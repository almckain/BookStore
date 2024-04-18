using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestWebsite
{
	public class AdminService : DatabaseService ,IAdminService
	{
		public AdminService()
		{
		}

        public List<Book> GetLowStockBooks()
        {
            List<Book> lowStockBooks = new List<Book>();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Stored Procedure
                    string procedureToCall = "GetLowStockBooks";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book tempBook = new Book();
                                int id = reader["BookID"] != DBNull.Value ? Convert.ToInt32(reader["BookID"]) : 0;
                                int quantity = reader["Available"] != DBNull.Value ? Convert.ToInt32(reader["Available"]) : 0;
                                string title = reader["Title"]?.ToString() ?? "Default Title";
                                tempBook.BookID = id;
                                tempBook.StockQuantity = quantity;
                                tempBook.Title = title;

                                lowStockBooks.Add(tempBook);
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
            return lowStockBooks;
        }
    }
}

