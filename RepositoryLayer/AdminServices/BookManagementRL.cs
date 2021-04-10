using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.RequestModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AdminInterfaces;

namespace RepositoryLayer.AdminServices
{
    public class BookManagementRL : IBookManagementRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        private readonly IConfiguration config;
        readonly string InserBookRecordSQL = "InserBookRecord";

        public BookManagementRL(IConfiguration config)
        {
            this.config = config;
            sqlConnectString = config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString + "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }
        public bool AddBook(RequestBook book)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(InserBookRecordSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("BookName", book.BookName);
                cmd.Parameters.AddWithValue("BookDiscription", book.BookDiscription);
                cmd.Parameters.AddWithValue("BookImage", book.BookImage);
                cmd.Parameters.AddWithValue("BookPrice", book.BookPrice);
                cmd.Parameters.AddWithValue("AuthorName", book.AuthorName);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (result != null && result.Equals(1))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
