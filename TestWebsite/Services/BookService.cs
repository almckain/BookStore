using System;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace TestWebsite
{
	public class BookService : DatabaseService, IBookService
	{
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Book> GetBooks()
        {
            List<Book> AllBooks = new List<Book>();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Stored Procedure
                    string procedureToCall = "GetAllBooks";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader["BookID"] != DBNull.Value ? Convert.ToInt32(reader["BookID"]) : 0;

                                string title = reader["Title"]?.ToString() ?? "Default Title";
                                string authorName = reader["AuthorName"]?.ToString() ?? "Unknown Author";
                                decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;
                                string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";

                                Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                                AllBooks.Add(new Book(title, authorName, price, coverImagePath, id));
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Book> GetBestSellers()
        {
            List<Book> BestSellers = new List<Book>();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    //Stored Procedure
                    string procedureToCall = "GetTopSellingBooks";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader["BookID"] != DBNull.Value ? Convert.ToInt32(reader["BookID"]) : 0;
                                string title = reader["Title"]?.ToString() ?? "Default Title";
                                string authorName = reader["Author"]?.ToString() ?? "Unknown Author";
                                string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";
                                decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;

                                Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                                BestSellers.Add(new Book(title, authorName, price, coverImagePath, id));
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Genre> GetAllGenres()
        {
            List<Genre> genres = new List<Genre>();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    //Stored Procedure
                    String procedureToCall = "GetAllGenres";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<Book> GetBooksByGenres(List<int> ids)
        {
            
            List<Book> books = new List<Book>();
            
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    foreach (var id in ids)
                    {
                        string procedureToCall = "GetBooksByGenreID";

                        using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            command.Parameters.Add(new SqlParameter("@GenreID", id));
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int idd = reader["BookID"] != DBNull.Value ? Convert.ToInt32(reader["BookID"]) : 0;

                                    string title = reader["Title"]?.ToString() ?? "Default Title";
                                    string authorName = reader["AuthorName"]?.ToString() ?? "Unknown Author";
                                    string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";
                                    decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;

                                    Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                                    books.Add(new Book(title, authorName, price, coverImagePath, idd));
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



        /// <summary>
        /// Returns details of the request book
        /// </summary>
        /// <param name="id">Book id</param>
        /// <returns>Book with selected details</returns>
        public Book GetBookDetails(int id)
        {
            Book currentBook = new Book();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String procedureToCall = "GetBookDetails";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.Add(new SqlParameter("@BookID", id));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idd = reader["BookID"] != DBNull.Value ? Convert.ToInt32(reader["BookID"]) : 0;
                                int publisherID = reader["PublisherID"] != DBNull.Value ? Convert.ToInt32(reader["PublisherID"]) : 0;
                                int genreID = reader["GenreID"] != DBNull.Value ? Convert.ToInt32(reader["GenreID"]) : 0;
                                string title = reader["Title"]?.ToString() ?? "Default Title";
                                string authorName = reader["Author"]?.ToString() ?? "Unknown Author";
                                string isbn = reader["ISBN"]?.ToString() ?? "Unknown ISBN";

                                string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";
                                decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;
                                int stockQuantity = reader["Available"] != DBNull.Value ? Convert.ToInt32(reader["Available"]) : 0;

                                Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                                currentBook = new Book(title, authorName, price, coverImagePath, idd, isbn, publisherID, genreID, stockQuantity);
                                currentBook.PublisherID = publisherID;
                                currentBook.GenreID = genreID;
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

            return currentBook;

        }

        public List<Book> GetBooksByPublishers(int id)
        {

            List<Book> books = new List<Book>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();


                    string procedureToCall = "GetBooksByPublisherID";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.Add(new SqlParameter("@PublisherID", id));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idd = reader["BookID"] != DBNull.Value ? Convert.ToInt32(reader["BookID"]) : 0;

                                string title = reader["Title"]?.ToString() ?? "Default Title";
                                string authorName = reader["AuthorName"]?.ToString() ?? "Unknown Author";
                                string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";
                                decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;

                                Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                                books.Add(new Book(title, authorName, price, coverImagePath, idd));
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

