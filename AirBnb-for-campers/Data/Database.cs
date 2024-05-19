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
        public bool ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            // Method executes a parameterized query containing a camping spot to the database
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
                    Console.WriteLine("Query execution failed: " + "false" + " " + ex.Message);
                    throw;

                }
                finally
                {
                    // Close the connection
                    connection.Close();
                }

            }
        }

        public IEnumerable<CampingSpot> ExtractQuery(string query)
        {
            // Method executes the query and returns a collection of camping spots

            List<CampingSpot> spots = new List<CampingSpot>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    //OpenConnection();
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CampingSpot spot = new CampingSpot
                                {
                                    //Id = reader.GetInt32("CampingSpot_id"),
                                    Name = reader.GetString("Name"),
                                    Location = reader.GetString("Location"),
                                    Description = reader.GetString("Description"),
                                    Facilities = reader.GetString("Facilities"),
                                    Availability = reader.GetString("Availability"),
                                    Owner_Id = reader.GetInt32("Owner_id"),
                                };
                                spots.Add(spot);
                            }
                        }
                    }

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    return null;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return spots;
        }
        public IEnumerable<CampingSpot> ExtractQueryByName(string query)
        {
            List<CampingSpot> spots = new List<CampingSpot>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    //OpenConnection();
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CampingSpot spot = new CampingSpot
                                {
                                    //Id = reader.GetInt32("CampingSpot_id"),
                                    Name = reader.GetString("Name"),
                                    Location = reader.GetString("Location"),
                                    Description = reader.GetString("Description"),
                                    Facilities = reader.GetString("Facilities"),
                                    Availability = reader.GetString("Availability"),
                                    Owner_Id = reader.GetInt32("Owner_id"),
                                };
                                spots.Add(spot);
                            }
                        }
                    }

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    return null;
                }
                finally
                {
                    CloseConnection();
                }
            }
            return spots;

        }

        // Verify user logging information
        public T VerifyLoggingInfor<T>(string query, Dictionary<string, object> parameters)
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }

                        object result = cmd.ExecuteScalar();
                        return (T)Convert.ChangeType(result, typeof(T));
                    }

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    throw;
                }
                finally
                {
                    CloseConnection();
                }

            }


        }

       
    }

}
