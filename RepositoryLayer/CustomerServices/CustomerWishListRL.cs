using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;

namespace RepositoryLayer.CustomerServices
{
    public class CustomerWishListRL : ICustomerWishListRL
    {
        private readonly IConfiguration config;
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        readonly string InsertBookToWishListSQL = "InsertBookToWishList";

        public CustomerWishListRL(IConfiguration config)
        {
            this.config = config;
            sqlConnectString = this.config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString + "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }

        public ICollection<CustomerWishList> AddBookToCart(string CustomerID, long BookID)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(InsertBookToWishListSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("BookID", BookID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                ICollection<CustomerWishList> WishList = new List<CustomerWishList>();
                CustomerWishList Book;
                while (rd.Read())
                {
                    Book = new CustomerWishList();
                    Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                    Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                    Book.WishListID = rd["WishListID"] == DBNull.Value ? default : rd.GetInt64("WishListID");
                    Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                    Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    WishList.Add(Book);
                }
                return WishList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
