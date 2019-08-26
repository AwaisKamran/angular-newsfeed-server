using System;
using System.Configuration; 
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AngularNewsFeed.Manager
{
    public class UserManager
    {
        internal static int registerUser(User user)
        {
            string queryString = @"           
                insert into Users(
                    name, 
                    email, 
                    password, 
                    type, 
                    ipAddress, 
                    active
                ) 
                values(@name, @email, @password, @type, @ipAddress, @active);
                SELECT SCOPE_IDENTITY()
            ";
            string connectionString = ConnectionStringManager.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@name", user.name);
                    command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@password", user.password);
                    command.Parameters.AddWithValue("@type", "user");
                    command.Parameters.AddWithValue("@ipAddress", user.ipAddress);
                    command.Parameters.AddWithValue("@active", 1);
                    connection.Open();
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        return int.Parse(reader[0].ToString());
                    }
                    reader.Close();
                    return -1;
                }

                catch (SqlException exception)
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    return -1;
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        internal static string loginUser(User user)
        {
            string queryString = @"    
                select * from Users 
                where 
                email=@email and 
                password=@password
            ";
            string connectionString = ConnectionStringManager.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@password", user.password);
                    connection.Open();
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return reader["type"].ToString();
                        }
                        reader.Close();
                    }
                    reader.Close();
                    return null;
                }

                catch (SqlException exception)
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    return null;
                }

                finally {
                    reader.Close();
                }
            }
        }
    }
}