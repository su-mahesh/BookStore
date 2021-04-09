using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace BusinessLayer.CustomerIntrfaces
{
    public interface ICustomerAddressBL
    {
        bool AddCustomerAddress(CustomerAddress address);
        bool DeleteCustomerAddress(string customerID, long addressID);
        ICollection<CustomerAddressResponse> GetAllCustomerAddress(string customerID);
    }
}
