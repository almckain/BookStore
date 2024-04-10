using System;
using Microsoft.Data.SqlClient;

namespace TestWebsite
{
	public abstract class DatabaseService
	{
        protected SqlConnectionStringBuilder builder;

        public DatabaseService()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "mssql.cs.ksu.edu";
            builder.UserID = "almckain";
            builder.Password = "Septembuary1793*";
            builder.InitialCatalog = "almckain";
            builder.Encrypt = false;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(builder.ConnectionString);
        }
    }
}

