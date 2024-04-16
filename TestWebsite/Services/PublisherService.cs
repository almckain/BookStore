using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestWebsite
{
	public class PublisherService: DatabaseService, IPublisherService
	{
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

