using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestWebsite
{
	public class CustomerService : DatabaseService, ICustomerService
	{

        public List<Order> ReturnOrdersByCustomers(int customerID)
        {
            List<Order> orders = new List<Order>();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String procedureToCall = "ReturnCustOrders";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.Add(new SqlParameter("@CustomerID", customerID));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Order order = new Order
                                {
                                    OrderNumber = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : 0,
                                    Total = reader["OrderTotal"] != DBNull.Value ? Convert.ToInt32(reader["OrderTotal"]) : 0,
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"])
                                  
                                };
                                orders.Add(order);
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
            return orders;

        }

        public Customer GetCustomerByEmail(string email)
        {
            Customer currentCustomer = new Customer();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String procedureToCall = "GetCustomerByEmail";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        command.Parameters.Add(new SqlParameter("@Email", email));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer
                                {
                                    CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
                                    Name = reader["Name"]?.ToString() ?? "Unknown Name",
                                    Email = reader["Email"]?.ToString() ?? "Email"
                                };
                                currentCustomer = customer;
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

            return currentCustomer;
        }

        public bool CreateNewCustomer(string name, string email)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String procedureToCall = "CreateNewAccount";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Name", name);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\nError Connecting to database: " + e.ToString() + "\n\n");
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return false;
            }

            return true;
        }
    }
}

