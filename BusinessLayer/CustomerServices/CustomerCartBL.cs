using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.ResponseModel;
using RepositoryLayer.CutomerInterfaces;

namespace BusinessLayer.CustomerServices
{
    public class CustomerCartBL : ICustomerCartBL
    {
        ICustomerCartRL customerCartRL;

        public CustomerCartBL(ICustomerCartRL customerCartRL)
        {
            this.customerCartRL = customerCartRL;
        }

        public ICollection<CustomerCart> AddBookToCart(string CustomerID, long BookID) 
        {
            try
            {
                var result =  customerCartRL.AddBookToCart(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
