using System;
using Microsoft.Data.SqlClient; //Dependecies NuGet Package
using Microsoft.Data;
using System.Data;
using System.Data.SqlClient;

namespace TestWebsite
{
	public class ServerConnection
	{

		public ServerConnection()
		{
		}
		
		public void GetTopSellingBooks()
		{
			try
			{
				SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
				builder.DataSource = "mssql.cs.ksu.edu"; //IDK
				builder.UserID = "almckain"; //What to
				builder.Password = "Septembuary1793*"; //Put
				builder.InitialCatalog = "almckain"; //Here
				builder.Encrypt = false; //Apparently is vital to the connection working?????

				using(var connection = new SqlConnection(builder.ConnectionString))
				{
					connection.Open();

					//Stored Procedure
					String procedureToCall = "GetTopSellingBooks"; //I think

					using(SqlCommand command = new SqlCommand(procedureToCall, connection)
					{
						CommandType = CommandType.StoredProcedure
					})
					{
						command.CommandType = CommandType.StoredProcedure;
						//test
						using(SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								// HEAR YEE HEAR YEE ALL THE QUESTION MARKS ARE REQUIRED -- IF THE DB RETURNS NULL AND WE CALL TOSTRING ON IT WE WILL GET A NULL REFERENCE EXCEPTION
                                string title = reader["Title"]?.ToString() ?? "Default Title";
                                string authorName = reader["Author"]?.ToString() ?? "Unknown Author";
                                string coverImagePath = reader["CoverImagePath"]?.ToString() ?? "No image available";

                                // For price check for DBNull.Value instead of null
                                decimal price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0m;

                                Console.WriteLine($"Title: {title}, Author: {authorName}, Price: ${price}, Cover Image Path: {coverImagePath}");
                            }
                        }
					}
				}

			}
			catch(SqlException e)
			{
				Console.WriteLine("\n\nError Connecting to database: " + e.ToString() +"\n\n");
				System.Diagnostics.Debug.WriteLine(e.ToString());

            }
		}
		
	}
}

