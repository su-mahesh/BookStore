using System;
using System.Collections.Generic;
using BusinessLayer.CustomerIntrfaces;
using CommonLayer.ResponseModel;
using RepositoryLayer.CutomerInterfaces;

namespace BusinessLayer.CustomerServices
{
    public class CustomerWishListBL : ICustomerWishListBL
    {
        ICustomerWishListRL customerWishListRL;

        public CustomerWishListBL(ICustomerWishListRL customerWishListRL)
        {
            this.customerWishListRL = customerWishListRL;
        }
        public ICollection<CustomerWishList> AddBookToCart(string CustomerID, long BookID)
        {
            try
            {
                var result = customerWishListRL.AddBookToCart(CustomerID, BookID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
