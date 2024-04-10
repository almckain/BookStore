using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestWebsite
{
	public class BookService : DatabaseService, IBookService
	{
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
                                string title = reader["Title"]?.ToString() ?? "Default Title";
                                string authorName = reader["Author"]?.ToString() ?? "Unknown Author";
                                string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";
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
                using (var connection = GetConnection())
                {
                    connection.Open();

                    //Stored Procedure
                    String procedureToCall = "GetAllGenres"; //I think

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
                        String procedureToCall = "GetBooksByGenreID";

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
                                    
                                    string title = reader["Title"]?.ToString() ?? "Default Title";
                                    string authorName = reader["AuthorName"]?.ToString() ?? "Unknown Author";
                                    string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";
                                    decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;

                                    Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                                    books.Add(new Book(title, authorName, price, coverImagePath));
                                    
                                    /*
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnName = reader.GetName(i);
                                        string value = reader.IsDBNull(i) ? "NULL" : reader.GetValue(i).ToString();
                                        Console.WriteLine($"{columnName}: {value}");
                                    }
                                    Console.WriteLine("----------"); // Separator for each row
                                    */
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

