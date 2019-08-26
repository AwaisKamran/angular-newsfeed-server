using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AngularNewsFeed.Manager
{
    public class PostManager
    {
        internal static IEnumerable<object> getPosts()
        {
            string queryString = @"select * from posts";
            string connectionString = ConnectionStringManager.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    List<Post> posts = new List<Post>();
                    while (reader.Read())
                    {
                        Post post = new Post();
                        post.postId = int.Parse(reader["postId"].ToString());
                        post.postTitle = reader["postTitle"].ToString();
                        post.postContent = reader["postContent"].ToString();
                        post.postCreated = DateTime.Parse(reader["postCreated"].ToString());
                        post.postCategory = int.Parse(reader["postCategory"].ToString());
                        post.postApproved = bool.Parse(reader["postApproved"].ToString());
                        post.postedBy = int.Parse(reader["postedBy"].ToString());
                        post.MetaTitle = reader["MetaTitle"].ToString();
                        post.MetaDescription = reader["MetaDescription"].ToString();

                        if (
                                    reader["ModifiedDate"].ToString() != null &&
                                    reader["ModifiedDate"].ToString() != ""
                               )
                        {
                            post.ModifiedDate = DateTime.Parse(reader["ModifiedDate"].ToString());
                        }

                        if (
                                reader["ModifiedBy"].ToString() != null &&
                                reader["ModifiedBy"].ToString() != ""
                           )
                        {
                            post.ModifiedBy = int.Parse(reader["ModifiedBy"].ToString());
                        }

                        post.Tags = reader["Tags"].ToString();
                        post.MetaKeywords = reader["MetaKeywords"].ToString();
                        post.postSource = reader["postSource"].ToString();
                        post.OwnerOfSource = reader["OwnerOfSource"].ToString();
                        posts.Add(post);
                    }
                    return posts.ToList<Post>();
                }

                finally
                {
                    reader.Close();
                }
            }
        }

        internal static bool updatePost(int id, Post post)
        {
            string queryString = @"
                Update Posts
                    set 
                    MetaTitle = @MetaTitle,
                    MetaDescription = @MetaDescription,
                    MetaKeywords = @MetaKeywords,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy,
                    postSource = @postSource,
                    OwnerOfSource = @OwnerOfSource
                where postId= @id";

            string connectionString = ConnectionStringManager.getConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@MetaTitle", post.MetaTitle);
                    command.Parameters.AddWithValue("@MetaDescription", post.MetaDescription);
                    command.Parameters.AddWithValue("@MetaKeywords", post.MetaKeywords);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ModifiedBy", post.ModifiedBy);
                    command.Parameters.AddWithValue("@postSource", post.postSource);
                    command.Parameters.AddWithValue("@OwnerOfSource", post.OwnerOfSource);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    reader = command.ExecuteReader();
                    reader.Close();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    return false;
                }
            }
        }

        internal static bool approvePost(int id)
        {
            string queryString = @"Update Posts set postApproved = 1 where postId= @id";
            string connectionString = ConnectionStringManager.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    reader = command.ExecuteReader();
                    reader.Close();
                    return true;
                }
                catch (SqlException ex)
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    return false;
                }
            }
        }

        internal static IEnumerable<object> getUnApprovedPosts()
        {
            string queryString = @"select * from posts where postApproved = 0";
            string connectionString = ConnectionStringManager.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    List<Post> posts = new List<Post>();
                    while (reader.Read())
                    {
                        Post post = new Post();
                        post.postId = int.Parse(reader["postId"].ToString());
                        post.postTitle = reader["postTitle"].ToString();
                        post.postContent = reader["postContent"].ToString();
                        post.postCreated = DateTime.Parse(reader["postCreated"].ToString());
                        post.postCategory = int.Parse(reader["postCategory"].ToString());
                        post.postApproved = bool.Parse(reader["postApproved"].ToString());
                        post.postedBy = int.Parse(reader["postedBy"].ToString());
                        post.MetaTitle = reader["MetaTitle"].ToString();
                        post.MetaDescription = reader["MetaDescription"].ToString();

                        if (
                                    reader["ModifiedDate"].ToString() != null &&
                                    reader["ModifiedDate"].ToString() != ""
                               )
                        {
                            post.ModifiedDate = DateTime.Parse(reader["ModifiedDate"].ToString());
                        }

                        if (
                                reader["ModifiedBy"].ToString() != null &&
                                reader["ModifiedBy"].ToString() != ""
                           )
                        {
                            post.ModifiedBy = int.Parse(reader["ModifiedBy"].ToString());
                        }

                        post.Tags = reader["Tags"].ToString();
                        post.MetaKeywords = reader["MetaKeywords"].ToString();
                        post.postSource = reader["postSource"].ToString();
                        post.OwnerOfSource = reader["OwnerOfSource"].ToString();
                        posts.Add(post);
                    }
                    return posts.ToList<Post>();
                }

                finally
                {
                    reader.Close();
                }
            }
        }

        internal static IEnumerable<object> getCategoryPosts()
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

                        IEnumerable<Post> categoryPostList;
                        if ( (categoryPostList = getPostByCategory(category.categoryId)) != null)
                        {
                            category.Posts = categoryPostList.ToArray<Post>();
                        }
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

        internal static int createPost(Post post)
        {
            string queryString = @"           
                insert into Posts(
	                postTitle, 
	                postContent, 
	                postCreated, 
	                postCategory, 
	                postedBy, 
	                Tags,
	                postSource, 
	                OwnerOfSource
                ) 
                values(
	                @postTitle, 
	                @postContent, 
	                SYSDATETIME(), 
	                @postCategory, 
	                @postedBy,
	                @tags,
                    @postSource,
                    @ownerOfSource
                );
                SELECT SCOPE_IDENTITY();
            ";
            string connectionString = ConnectionStringManager.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader reader = null;
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@postTitle", post.postTitle);
                    command.Parameters.AddWithValue("@postContent", post.postContent);
                    command.Parameters.AddWithValue("@postCategory", post.postCategory);
                    command.Parameters.AddWithValue("@postedBy", post.postedBy);
                    command.Parameters.AddWithValue("@tags", post.Tags);
                    command.Parameters.AddWithValue("@postSource", post.postSource);
                    command.Parameters.AddWithValue("@ownerOfSource", post.OwnerOfSource);
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

        internal static object getPost(int id)
        {
            string queryString = @"select * from posts where postId = @postId";
            string connectionString = ConnectionStringManager.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@postId", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows)
                    {
                        Post post = new Post();
                        while (reader.Read())
                        {
                            post.postId = int.Parse(reader["postId"].ToString());
                            post.postTitle = reader["postTitle"].ToString();
                            post.postContent = reader["postContent"].ToString();
                            post.postCreated = DateTime.Parse(reader["postCreated"].ToString());
                            post.postCategory = int.Parse(reader["postCategory"].ToString());
                            post.postApproved = bool.Parse(reader["postApproved"].ToString());
                            post.postedBy = int.Parse(reader["postedBy"].ToString());
                            post.MetaTitle = reader["MetaTitle"].ToString();
                            post.MetaDescription = reader["MetaDescription"].ToString();
                            post.ModifiedDate = DateTime.Parse(reader["ModifiedDate"].ToString());
                            post.ModifiedBy = int.Parse(reader["ModifiedBy"].ToString());
                            post.Tags = reader["Tags"].ToString();
                            post.MetaKeywords = reader["MetaKeywords"].ToString();
                            post.postSource = reader["postSource"].ToString();
                            post.OwnerOfSource = reader["OwnerOfSource"].ToString();
                        }
                        return post;
                    }
                    return null;
                }

                catch(SqlException exception)
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    return null;
                }

                finally
                {
                    reader.Close();
                }
            }
        }

        internal static IEnumerable<Post> getPostByCategory(int categoryId)
        {
            string queryString = @"select * from posts where postCategory = @categoryId";
            string connectionString = ConnectionStringManager.getConnectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    if (reader.HasRows)
                    {
                        List<Post> posts = new List<Post>();
                        while (reader.Read())
                        {
                            Post post = new Post();
                            post.postId = int.Parse(reader["postId"].ToString());
                            post.postTitle = reader["postTitle"].ToString();
                            post.postContent = reader["postContent"].ToString();
                            post.postCreated = DateTime.Parse(reader["postCreated"].ToString());
                            post.postCategory = int.Parse(reader["postCategory"].ToString());
                            post.postApproved = bool.Parse(reader["postApproved"].ToString());
                            post.postedBy = int.Parse(reader["postedBy"].ToString());
                            post.MetaTitle = reader["MetaTitle"].ToString();
                            post.MetaDescription = reader["MetaDescription"].ToString();

                            if (
                                    reader["ModifiedDate"].ToString() != null &&
                                    reader["ModifiedDate"].ToString() != ""
                               )
                            {
                                post.ModifiedDate = DateTime.Parse(reader["ModifiedDate"].ToString());
                            }

                            if (
                                    reader["ModifiedBy"].ToString() != null &&
                                    reader["ModifiedBy"].ToString() != ""
                               )
                            {
                                post.ModifiedBy = int.Parse(reader["ModifiedBy"].ToString());
                            }
                        
                            post.Tags = reader["Tags"].ToString();
                            post.MetaKeywords = reader["MetaKeywords"].ToString();
                            post.postSource = reader["postSource"].ToString();
                            post.OwnerOfSource = reader["OwnerOfSource"].ToString();
                            posts.Add(post);
                        }
                        return posts;
                    }
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

                finally
                {
                    reader.Close();
                }
            }
        }
    }
}