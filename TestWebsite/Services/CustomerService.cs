using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestWebsite
{
	public class CustomerService : DatabaseService, ICustomerService
	{
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
    }
}

