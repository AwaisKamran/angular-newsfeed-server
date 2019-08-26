using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AngularNewsFeed.Manager
{
    public class CategoryManager
    {
        internal static IEnumerable<Category> getCategories()
        {
            string queryString = @"select * from category";

            string connectionString = ConnectionStringManager.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    List<Category> categories = new List<Category>();
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.categoryId = int.Parse(reader["categoryId"].ToString());
                        category.categoryName = reader["categoryName"].ToString();
                        categories.Add(category);
                    }
                    return categories.ToList<Category>();
                }

                finally
                {
                    reader.Close();
                }
            }
        }

        internal static int postCategory(Category category)
        {
            string queryString = @"           
                insert into Category(
                    categoryName
                ) 
                values(@categoryName);
                SELECT SCOPE_IDENTITY()
            ";
            string connectionString = ConnectionStringManager.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@categoryName", category.categoryName);
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
    }
}