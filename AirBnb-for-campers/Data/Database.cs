using AirBnb_for_campers.Models;
using MySqlConnector;
namespace AirBnb_for_campers.Data
{
    public class Database
    {
        private string connectionString =
            "server=127.0.0.1;" +
            "port=3306;" +
            "username=root;" +
            "password=;" +
            "database=airbnb_for_campers;";
        //string connectionString = "Server=localhost;Database=airbnb_for_campers;Uid=root;Pwd=";
        private bool OpenConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message + " opening connection status: " + false);
            }
        }
        private bool CloseConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message + " closing connection status: " + false);
            }
        }
        public bool TestConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                OpenConnection();
                Console.WriteLine("Connection successful!");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message); 
                return false;
            }
            finally { connection.Close(); }
        }
        public bool ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            // Method executes a parameterized query to the database
            // Returns true if successful, false if it fails.

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Query execution failed: " + "false" + " " +  ex.Message);
                    return false;
                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }
            }
        }

        /*  public bool ExecuteQuery(string query)
          {
              // Method executes all queries to the tables in the database
              // return a true if success and false in it failes.
              using (MySqlConnection connection = new MySqlConnection(connectionString))
              {
                  try
                  {
                      //OpenConnection();
                      connection.Open();
                      using (MySqlCommand cmd = new MySqlCommand(query, connection))
                      {
                          cmd.ExecuteNonQuery();
                      }
                      return true;
                  }
                  catch (MySqlException ex)
                  {
                      Console.WriteLine("Query execution failed: " + ex.Message);
                      return false;
                  }
                  finally
                  {
                      CloseConnection();
                  }

              }
          }*/

    }

}
