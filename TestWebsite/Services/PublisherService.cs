using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestWebsite
{
	public class PublisherService: DatabaseService, IPublisherService
	{
        public bool PlacePublisherOrder(Dictionary<int, int[]> cart)
        {
            int newOrderId = 0;
            int newOrderLineId = 0;
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String procedureToCall = "GetBiggestPurchaseOrderID";

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

                    procedureToCall = "GetBiggestPurchaseOrderLineID";

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
                    foreach (KeyValuePair<int, int[]> publisher in cart)
                    {
                        //index 0 is pub id
                        //index 1 is quantity
                        int bookId = publisher.Key;
                        int quantity = publisher.Value[1];
                        if (quantity > 0)
                        {
                            procedureToCall = "PlaceOrderPublishers";
                            using (SqlCommand command = new SqlCommand(procedureToCall, connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@PurchaseOrderID", newOrderId);
                                command.Parameters.AddWithValue("@PublisherID", publisher.Value[0]);
                                command.ExecuteNonQuery();
                            }


                            procedureToCall = "CreateProcedureOrderLines";
                            using (SqlCommand command = new SqlCommand(procedureToCall, connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@PurchaseOrderLineID", newOrderLineId);
                                command.Parameters.AddWithValue("@PurchaseOrderID", newOrderId);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@BookID", bookId);

                                command.ExecuteNonQuery();
                            }
                            procedureToCall = "IncrementBookInventory";
                            using (SqlCommand command = new SqlCommand(procedureToCall, connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@BookID", bookId);
                                command.Parameters.AddWithValue("@Quantity", quantity);

                                command.ExecuteNonQuery();
                            }
                            newOrderLineId++;
                        }
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


        public PublisherService()
		{
		}

		public Publisher GetPublisherByID(int id)
		{
            Publisher currentPublisher = new Publisher();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String procedureToCall = "GetPublisherByID";

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
                                Publisher publisher = new Publisher {
                                    Name = reader["Name"]?.ToString() ?? "Unknown Publisher",
                                    PublisherID = reader["PublisherID"] != DBNull.Value ? Convert.ToInt32(reader["PublisherID"]):0,
                                    PhoneNumber = reader["PhoneNumber"]?.ToString() ?? "Unknown Phone Number",
                                    StreetAddress = reader["StreetAddress"]?.ToString() ?? "Unknown Address"
                                };
                                currentPublisher = publisher;

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

            return currentPublisher;
        }
	}
}

