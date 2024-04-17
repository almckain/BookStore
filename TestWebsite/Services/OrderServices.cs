using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Data;

namespace TestWebsite
{
	public class OrderServices : DatabaseService, IOrderService
	{
        public List<Order> GetOrdersForCustomer(int customerId)
        {
            /*
            * Eventually will be a query that returns all orders
            */
            return new List<Order>
            {
                new Order
                {
                    OrderNumber = 1,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2002, 4, 16),
                    Total = 36.40m
                },
                new Order
                {
                    OrderNumber = 2,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2003, 7, 21),
                    Total = 52.25m
                },
                new Order
                {
                    OrderNumber = 3,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2005, 11, 2),
                    Total = 19.99m
                },
                new Order
                {
                    OrderNumber = 4,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2007, 5, 30),
                    Total = 99.95m
                },
                new Order
                {
                    OrderNumber = 5,
                    CustomerID = customerId,
                    OrderDate = new DateTime(2010, 3, 15),
                    Total = 75.50m
                },
            };
        }

        public void PlaceOrder(int customerId, Dictionary<int, int> cart)
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
                        newOrderLineId++;
                    }




                }

                }
                catch (SqlException e)
                {
                    Console.WriteLine("\n\nError Connecting to database: " + e.ToString() + "\n\n");
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }


            
        }

        public OrderServices()
		{
		}
	}
}

