using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.ResponseModel;
using RepositoryLayer.CutomerInterfaces;

namespace BusinessLayer.CustomerServices
{
    public class CustomerOrderBL : ICustomerOrderBL
    {
        ICustomerOrderRL CustomerOrderRL;

        public CustomerOrderBL(ICustomerOrderRL customerOrderRL)
        {
            CustomerOrderRL = customerOrderRL;
        }
        public CustomerOrder PlaceOrder(string CustomerID)
        {
            try
            {
                return CustomerOrderRL.PlaceOrder(CustomerID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
