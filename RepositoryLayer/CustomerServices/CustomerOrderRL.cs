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
    public class CustomerOrderRL : ICustomerOrderRL
    {
        private readonly IConfiguration config;
        readonly string PlaceOrderSQL = "PlaceOrder";

        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        public CustomerOrderRL(IConfiguration config)
        {
            this.config = config;
            sqlConnectString = this.config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString + "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }

        public CustomerOrder PlaceOrder(string CustomerID) 
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(PlaceOrderSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", CustomerID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (result != null && result.Equals(0))
                {
                    throw new Exception("Order failed cart is empty");
                }
                CustomerOrder order = new CustomerOrder();
                if (rd.Read())
                {
                    order.OrderID = rd["OrderID"] == DBNull.Value ? default : rd.GetInt64("OrderID");
                    order.OrderDate = rd["OrderDate"] == DBNull.Value ? default : rd.GetDateTime("OrderDate");
                    order.TotalCost = rd["TotalCost"] == DBNull.Value ? default : rd.GetInt32("TotalCost");
                    order.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                }
                return order;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
