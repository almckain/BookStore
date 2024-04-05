using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestWebsite
{
	public class BookService : IBookService
	{
        /*
		 * Eventually will be a call to the database for the books
		 */
        public List<Book> GetBooks()
        {
            List<Book> AllBooks = new List<Book>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "mssql.cs.ksu.edu";
                builder.UserID = "almckain";
                builder.Password = "Septembuary1793*";
                builder.InitialCatalog = "almckain";
                builder.Encrypt = false;
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    //Stored Procedure
                    String procedureToCall = "GetAllBooks"; //I think

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string title = reader["Title"]?.ToString() ?? "Default Title";
                                string authorName = reader["AuthorName"]?.ToString() ?? "Unknown Author";
                                decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;
                                string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";

                                Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                                AllBooks.Add(new Book(title, authorName, price, coverImagePath));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("\n\nError Connecting to database: " + e.ToString() + "\n\n");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return AllBooks;
            
        }

        /*
         * Eventually will be a call to the database for the best sellers
         */
        public List<Book> GetBestSellers()
        {
            List<Book> BestSellers = new List<Book>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "mssql.cs.ksu.edu"; //IDK
                builder.UserID = "almckain"; //What to
                builder.Password = "Septembuary1793*"; //Put
                builder.InitialCatalog = "almckain"; //Here
                builder.Encrypt = false; //Apparently is vital to the connection working?????

                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    //Stored Procedure
                    String procedureToCall = "GetTopSellingBooks"; //I think

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // HEAR YEE HEAR YEE ALL THE QUESTION MARKS ARE REQUIRED -- IF THE DB RETURNS NULL AND WE CALL TOSTRING ON IT WE WILL GET A NULL REFERENCE EXCEPTION
                                string title = reader["Title"]?.ToString() ?? "Default Title";
                                string authorName = reader["Author"]?.ToString() ?? "Unknown Author";
                                string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";
                                // For price check for DBNull.Value instead of null
                                decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;

                                Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                                BestSellers.Add(new Book(title, authorName, price, coverImagePath));
                            }
                        }
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("\n\nError Connecting to database: " + e.ToString() + "\n\n");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return BestSellers;
        }

        
        /*
         *  Eventually there may be another query to return all the books with a given order id???
         */
        public List<Book> GetBooksByOrderNumber(int orderNumber)
        {
            // Hardcoded for demonstration; you'll replace this with actual logic to fetch data
            var allBooks = GetBooks();
            //return allBooks.Where(book => book.OrderNumber == orderNumber).ToList();
            return null;
        }

        public List<Genre> GetAllGenres()
        {
            List<Genre> genres = new List<Genre>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "mssql.cs.ksu.edu";
                builder.UserID = "almckain";
                builder.Password = "Septembuary1793*";
                builder.InitialCatalog = "almckain";
                builder.Encrypt = false;

                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    //Stored Procedure
                    String procedureToCall = "GetAllGenres"; //I think

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string genreName = reader["GenreName"]?.ToString() ?? "Genre Name";
                                int genreID = reader["GenreID"] != DBNull.Value ? Convert.ToInt32(reader["GenreID"]) : 0;

                                genres.Add(new Genre(genreName, genreID));
                            }
                        }
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("\n\nError Connecting to database: " + e.ToString() + "\n\n");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return genres;
        }

        public List<Book> GetBooksByGenres(List<int> ids)
        {
            List<Book> books = new List<Book>();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "mssql.cs.ksu.edu";
                builder.UserID = "almckain";
                builder.Password = "Septembuary1793*";
                builder.InitialCatalog = "almckain";
                builder.Encrypt = false;

                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    foreach (var id in ids)
                    {


                        //Stored Procedure
                        String procedureToCall = "GetBooksByGenreID"; //I think

                        using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@GenreID", id));
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string title = reader["Title"]?.ToString() ?? "Default Title";
                                    string authorName = reader["Author"]?.ToString() ?? "Unknown Author";
                                    string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";
                                    decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;

                                    Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                                    books.Add(new Book(title, authorName, price, coverImagePath));
                                }
                            }
                        }
                    }
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("\n\nError Connecting to database: " + e.ToString() + "\n\n");
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return books;
        }

        public BookService()
		{
		}
	}
}

