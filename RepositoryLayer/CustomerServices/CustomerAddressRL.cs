using System;
using System.Data;
using System.Data.SqlClient;
using CommonLayer.RequestModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.CutomerInterfaces;

namespace RepositoryLayer.CustomerServices
{
    public class CustomerAddressRL : ICustomerAddressRL
    {
        private readonly IConfiguration config;
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        readonly string InsertCustomerAddressSQL = "InsertCustomerAddress";
        readonly string DeleteCustomerAddressSQL = "DeleteCustomerAddress";
        public CustomerAddressRL(IConfiguration config)
        {
            this.config = config;
            sqlConnectString = this.config.GetConnectionString("BookStoreConnection");
            connection.ConnectionString = sqlConnectString + "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
        }

        public bool AddCustomerAddress(CustomerAddress address)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(InsertCustomerAddressSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", address.CustomerID);
                cmd.Parameters.AddWithValue("Pincode", address.Pincode);
                cmd.Parameters.AddWithValue("Address", address.Address);
                cmd.Parameters.AddWithValue("City", address.City);
                cmd.Parameters.AddWithValue("AddressType", address.AddressType);
                cmd.Parameters.AddWithValue("Landmark", address.Landmark);
                cmd.Parameters.AddWithValue("Locality", address.Locality);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteReader();
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

        public bool DeleteCustomerAddress(string customerID, long addressID)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(DeleteCustomerAddressSQL, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("CustomerID", customerID);
                cmd.Parameters.AddWithValue("AddressID", addressID);
                var returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteReader();
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
