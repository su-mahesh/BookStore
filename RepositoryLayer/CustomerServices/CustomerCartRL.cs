using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;

namespace RepositoryLayer.CustomerServices
{
    public class CustomerCartRL : ICustomerCartRL
    {
        private readonly IConfiguration config;
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        readonly string InsertBookToCartSQL = "InsertBookToCart";
        readonly string RemoveBookFromCartSQL = "RemoveBookFromCart";

        public CustomerCartRL(IConfiguration config)
        {
            this.config = config;
            this.config = config;
            sqlConnectString = this.config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString + "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }

        public ICollection<CustomerCart> AddBookToCart(string CustomerID, long BookID) 
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(InsertBookToCartSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("BookID", BookID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                ICollection<CustomerCart> cart = new List<CustomerCart>();
                CustomerCart Book;
                while (rd.Read())
                {
                    Book = new CustomerCart();
                    Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                    Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                    Book.CartID = rd["CartID"] == DBNull.Value ? default : rd.GetInt64("CartID");
                    Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                    Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    cart.Add(Book);
                }
                return cart;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ICollection<CustomerCart> RemoveBookFromCart(string CustomerID, long BookID)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(RemoveBookFromCartSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                cmd.Parameters.AddWithValue("BookID", BookID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                ICollection<CustomerCart> cart = new List<CustomerCart>();
                CustomerCart Book;
                while (rd.Read())
                {
                    Book = new CustomerCart();
                    Book.BookID = rd["BookID"] == DBNull.Value ? default : rd.GetInt64("BookID");
                    Book.BookPrice = rd["BookPrice"] == DBNull.Value ? default : rd.GetInt32("BookPrice");
                    Book.CartID = rd["CartID"] == DBNull.Value ? default : rd.GetInt64("CartID");
                    Book.BookName = rd["BookName"] == DBNull.Value ? default : rd.GetString("BookName");
                    Book.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    cart.Add(Book);
                }
                return cart;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
