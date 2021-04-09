using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;

namespace RepositoryLayer.CustomerServices
{
    public class CustomerAccountRL : ICustomerAccountRL
    {
        private readonly IConfiguration config;
        readonly DatabaseConnection dbConnection;
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        readonly string SQL = "InsertCustomerRecord";

        public CustomerAccountRL(IConfiguration config)
        {
            this.config = config;
            dbConnection = new DatabaseConnection(this.config);
            sqlConnectString = config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString + "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";

        }

        public CustomerAccount RegisterCustomer(RegisterCustomerAccount registerCustomerAccount)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("FirstName", registerCustomerAccount.FirstName);
                cmd.Parameters.AddWithValue("LastName", registerCustomerAccount.LastName);
                cmd.Parameters.AddWithValue("Email", registerCustomerAccount.Email);
                cmd.Parameters.AddWithValue("PhoneNumber", registerCustomerAccount.PhoneNumber);
                cmd.Parameters.AddWithValue("Password", registerCustomerAccount.Password);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                CustomerAccount customer = new CustomerAccount();
                SqlDataReader rd = cmd.ExecuteReader();
                var result = returnParameter.Value;
                if (rd.Read())
                {
                    customer.CustomerID = rd["CustomerID"] == DBNull.Value ? default : rd.GetString("CustomerID");
                    customer.FirstName = rd["CustomerFirstName"] == DBNull.Value ? default : rd.GetString("CustomerFirstName");
                    customer.LastName = rd["CustomerLastName"] == DBNull.Value ? default : rd.GetString("CustomerLastName");
                    customer.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                    customer.PhoneNumber = rd["PhoneNumber"] == DBNull.Value ? default : rd.GetInt64("PhoneNumber");
                }
                if (result!= null && result.Equals(2))
                {
                    throw new Exception("Email already registered");
                }
                return customer;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
