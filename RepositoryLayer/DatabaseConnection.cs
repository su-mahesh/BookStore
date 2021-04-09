using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer
{
    public class DatabaseConnection
    {
        private readonly IConfiguration config;
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;


        public DatabaseConnection(IConfiguration config)
        {
            this.config = config;
            sqlConnectString = config.GetConnectionString("BookStoreConnection");
        }

        public SqlConnection GetConnection() {

            // Set the connection string with pooling option    
            connection.ConnectionString = sqlConnectString + "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
            //Open connection    }
            connection.Open();
            return connection;
        }
    }
}
