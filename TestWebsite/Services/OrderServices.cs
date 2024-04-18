using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace TestWebsite
{
	public class OrderServices : DatabaseService, IOrderService
	{
        public bool PlaceOrder(int customerId, Dictionary<int, int> cart)
        {
            int newOrderId=0;
            int newOrderLineId=0;
                try
                {
                    using (var connection = GetConnection())
                    {
                        connection.Open();

                        String procedureToCall = "GetBiggestOrderId";

                        using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                int oldOrderId = reader["max_orderid"] != DBNull.Value ? Convert.ToInt32(reader["max_orderid"]) : 0;
                                newOrderId = oldOrderId + 1;
                                }
                            }
                        }

                    procedureToCall = "GetBiggestOrderLineId";

                    using (SqlCommand command = new SqlCommand(procedureToCall, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int oldOrderLineId = reader["max_orderlineid"] != DBNull.Value ? Convert.ToInt32(reader["max_orderlineid"]) : 0;
                                newOrderLineId = oldOrderLineId + 1;
                            }
                        }
                    }

                    procedureToCall = "PlaceOrder";
                    using (SqlCommand command = new SqlCommand(procedureToCall, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrderId", newOrderId);
                        command.Parameters.AddWithValue("@CustomerId", customerId);
                        command.ExecuteNonQuery(); 
                    }

                    foreach (KeyValuePair<int, int> pair in cart)
                    {
                        int bookId = pair.Key;
                        int quantity = pair.Value;
                        procedureToCall = "CreateOrderLines";
                        using (SqlCommand command = new SqlCommand(procedureToCall, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@OrderLineId", newOrderLineId);
                            command.Parameters.AddWithValue("@OrderId", newOrderId);
                            command.Parameters.AddWithValue("@Quantity", quantity);
                            command.Parameters.AddWithValue("@BookId", bookId);

                            command.ExecuteNonQuery();
                        }
                        procedureToCall = "DecrementBookIventory";
                        using (SqlCommand command = new SqlCommand(procedureToCall, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@BookId", bookId);
                            command.Parameters.AddWithValue("@Quantity", quantity);

                            command.ExecuteNonQuery();
                        }
                        newOrderLineId++;
                    }
                    
                    return true;



                }

                }
                catch (SqlException e)
                {
                    Console.WriteLine("\n\nError Connecting to database: " + e.ToString() + "\n\n");
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
            return false;


            
        }

        public OrderServices()
		{
		}
	}
}

