using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TestWebsite
{
	public class GenreService: DatabaseService, IGenreService
	{
		public GenreService()
		{
		}

        public Genre GetGenreByID(int id)
        {
            Genre currentGenre = new Genre();
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    String procedureToCall = "GetGenreByID";

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
                                Genre genre = new Genre
                                {
                                    GenreName = reader["Name"]?.ToString() ?? "Unknown Genre",
                                    GenreID = reader["GenreID"] != DBNull.Value ? Convert.ToInt32(reader["GenreID"]) : 0,
                                };
                                currentGenre = genre;

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

            return currentGenre;
        }
    }
}

