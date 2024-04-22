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
                                tempBook.PublisherID = reader["Publisher_ID"] != DBNull.Value ? Convert.ToInt32(reader["Publisher_ID"]) : 0;
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

        public List<RecentStockRefillViewModel> GetRecentStockRefills()
        {
            List<RecentStockRefillViewModel> recentRefills = new List<RecentStockRefillViewModel>();

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Stored Procedure
                    string procedureToCall = "GetRecentPurchaseOrders";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RecentStockRefillViewModel tempRecentRefill = new RecentStockRefillViewModel();
                                int quantity = reader["Quanity"] != DBNull.Value ? Convert.ToInt32(reader["Quanity"]) : 0;
                                string title = reader["Booktitle"]?.ToString() ?? "Default Title";
                                string publisherName = reader["PublisherName"]?.ToString() ?? "Default Publisher";
                                DateTime orderDate = Convert.ToDateTime(reader["OrderDate"]);

                                tempRecentRefill.BookTitle = title;
                                tempRecentRefill.QuantityOrdered = quantity;
                                tempRecentRefill.OrderDate = orderDate;
                                tempRecentRefill.PublisherName = publisherName;

                                recentRefills.Add(tempRecentRefill);
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

            return recentRefills;
        }

        public List<Customer> GetTopCustomers()
        {
            List<Customer> topCustomers = new List<Customer>();

            try
            {
                using(var connection = GetConnection())
                {
                    connection.Open();

                    string procedureToCall = "GetTopCustomers";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string customerName = reader["Name"]?.ToString() ?? "Unknown Customer";
                                int customerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0;
                                string customerEmail = reader["Email"]?.ToString() ?? "Unknown Email";
                                int orderCount = reader["OrderCount"] != DBNull.Value ? Convert.ToInt32(reader["OrderCount"]) : 0;
                                decimal totalSpent = reader["TotalMoneySpent"] != DBNull.Value ? Convert.ToDecimal(reader["TotalMoneySpent"]) : 0m;
                                Customer temp = new Customer();
                                temp.Name = customerName;
                                temp.Email = customerEmail;
                                temp.CustomerID = customerID;
                                temp.OrderCount = orderCount;
                                temp.TotalMoneySpent = totalSpent;

                                topCustomers.Add(temp);
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
            return topCustomers;
        }
    }
}

