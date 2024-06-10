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

        // Method executes the query and returns a collection of camping spots
        public IEnumerable<CampingSpot> ExtractQuery(string query)
        {
            List<CampingSpot> spots = new List<CampingSpot>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               /* string imageName = reader.GetString("Image");
                                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageName);
*/
                                CampingSpot spot = new CampingSpot
                                {
                                    Id = reader.GetInt32("CampingSpot_id"),
                                    Name = reader.GetString("Name"),
                                    City = reader.GetString("City"),
                                    PostalNum = reader.GetString("PostalNum"),
                                    StreetNum = reader.GetInt32("StreetNum"),
                                    HouseNum = reader.GetInt32("HouseNum"),
                                    Description = reader.GetString("Description"),
                                    Facilities = reader.GetString("Facilities"),
                                    Availability = reader.GetString("Availability"),
                                    Image = reader.GetString("Image"),
                                    Owner_Id = reader.GetInt32("Owner_id"),
                                    //ImageBase64 = ConvertImageToBase64(imagePath)  // Convert image to base64
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
                    connection.Close();
                }
            }
            return spots;
        }

        private string ConvertImageToBase64(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                return Convert.ToBase64String(imageBytes);
            }
            return null;
        }


        public IEnumerable<CampingSpot> ExtractCampingFilter(string query)
        {
            // Filters camping spots based on the search query string value and returns
            // values 'CampingSpot' model.
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
                                    Id = reader.GetInt32("CampingSpot_id"),
                                    Name = reader.GetString("Name"),
                                    City = reader.GetString("City"),
                                    PostalNum = reader.GetString("PostalNum"),
                                    StreetNum = reader.GetInt32("StreetNum"),
                                    HouseNum = reader.GetInt32("HouseNum"),
                                    Description = reader.GetString("Description"),
                                    Facilities = reader.GetString("Facilities"),
                                    Availability = reader.GetString("Availability"),
                                    Image = reader.GetString("Image"),
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
        // get user information
        public IEnumerable<User> ExtractUserInfo(string query, Dictionary<string, object> parameters)
        {
            List<User> info = new List<User>();

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

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = new User
                                {
                                    FirstName = reader.GetString("FirstName"),
                                    LastName = reader.GetString("LastName"),
                                    UserName = reader.GetString("UserName"),
                                    Email = reader.GetString("Email"),
                                    // Assuming PhoneNum is stored as string in the database
                                    PhoneNum = (int?)reader.GetInt64("PhoneNum"),
                                    Password = reader.GetString("PASSWORD")
                                };
                                info.Add(user);
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    // Consider logging the exception instead of writing to console
                    return null;
                }
                // No need for finally block to close connection; using statement handles it
            }

            return info;
        }

        // Get user first and last name
        public string ExtractUserPictureUrl(string query, Dictionary<string, object> parameters)
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

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string imageUrl = reader.GetString("profilePictureUrl");

                                return imageUrl;
                                
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    return null;
                }
            }

            return null;
        }
        //Get user Name
        public string ExtractUser(string query, Dictionary<string, object> parameters)
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

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstName = reader.GetString("FirstName");
                                string lastName = reader.GetString("LastName");


                                return firstName + " " + lastName;

                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    return null;
                }
            }

            return null;
        }
        // Get owner spots
        public IEnumerable<CampingSpot> ExtractOwnerSpots(string query, Dictionary<string, object> parameters)
        {
            List<CampingSpot> spots = new List<CampingSpot>();

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

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CampingSpot spot = new CampingSpot
                                {
                                    Name = reader.GetString("Name"),
                                    City = reader.GetString("City"),
                                    PostalNum = reader.GetString("PostalNum"),
                                    StreetNum = reader.GetInt32("StreetNum"),
                                    HouseNum = reader.GetInt32("HouseNum"),
                                    Description = reader.GetString("Description"),
                                    Facilities = reader.GetString("Facilities"),
                                    Availability = reader.GetString("Availability"),
                                    Image = reader.GetString("Image"),
                                    Latitude = reader.GetDouble("Latitude"),
                                    Longitude = reader.GetDouble("Longitude")
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
                    connection.Close();
                }
            }

            return spots;
        }




        // Verify user logging information
        public int? VerifyUserLoggin (string query, Dictionary<string, object> parameters)
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

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetInt32("User_id");
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    return null;
                }
            }

            return null;
        }

        // Verify owner logging information
        public int? VerifyOwnerLogin(string query, Dictionary<string, object> parameters)
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

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetInt32("Owner_id");
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    return null;
                }
            }

            return null;
        }
        public object ExecuteScalar(string query, Dictionary<string, object> parameters)
        {
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

                        return cmd.ExecuteScalar();
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Query execution failed: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public IEnumerable<BookingInfo> ExtractUserBookings(string query, Dictionary<string, object> parameters)
        {
            List<BookingInfo> booked_spots = new List<BookingInfo>();

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

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookingInfo booked = new BookingInfo
                                {
                                    BookingDate = reader.GetDateTime("BookingDate"),
                                    StartDate = reader.GetDateTime("StartDate"),
                                    EndDate = reader.GetDateTime("EndDate"),
                                    NumOfPeople = reader.GetInt32("NumOfPeople"),
                                    Price = reader.GetDecimal("Price"),
                                    CampingSpotName = reader.GetString("Name"),
                                    CampingSpotCity = reader.GetString("City"),
                                    PostalNum = reader.GetString("PostalNum"),
                                    StreetNum = reader.GetInt32("StreetNum"),
                                    HouseNum = reader.GetInt32("HouseNum"),
                                    CampingSpotDescription = reader.GetString("Description"),
                                    CampingSpotFacilities = reader.GetString("Facilities")
                                };
                                booked_spots.Add(booked);

                            };
                               
                            
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    // Consider logging the exception instead of writing to console
                    return null;
                }
                // No need for finally block to close connection; using statement handles it
            }

            return booked_spots;
        }

        public IEnumerable<RateAndCommentInfo> GetRatingsAndComments(string query, Dictionary<string, object> parameters)
        {
            // return object RateAndCommentInfor, rates and comments on a camping spot with the user name
            List<RateAndCommentInfo> info = new List<RateAndCommentInfo>();
            
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

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RateAndCommentInfo details = new RateAndCommentInfo
                                {
                                    Rating = reader.GetInt32("Rating"),
                                    Comment = reader.GetString("Comment"),
                                    FirstName = reader.GetString("FirstName"),
                                    CreatedAt = reader.GetDateTime("CreatedAt"),
                                    
                                };
                                info.Add(details);

                            };


                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Query execution failed: " + ex.Message);
                    // Consider logging the exception instead of writing to console
                    return null;
                }
                // No need for finally block to close connection; using statement handles it
            }

            return info;
        }

    }

}
