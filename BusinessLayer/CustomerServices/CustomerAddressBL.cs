using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.RequestModel;
using RepositoryLayer.CutomerInterfaces;

namespace BusinessLayer.CustomerServices
{
    public class CustomerAddressBL : ICustomerAddressBL
    {
        ICustomerAddressRL customerAddressRL;

        public CustomerAddressBL(ICustomerAddressRL customerAddressRL)
        {
            this.customerAddressRL = customerAddressRL;
        }

        public bool AddCustomerAddress(CustomerAddress address)
        {
            try
            {
                return customerAddressRL.AddCustomerAddress(address); ;
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
                return customerAddressRL.DeleteCustomerAddress(customerID, addressID); ;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
