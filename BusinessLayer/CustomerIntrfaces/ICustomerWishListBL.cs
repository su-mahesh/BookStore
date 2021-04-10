using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.ResponseModel;

namespace BusinessLayer.CustomerIntrfaces
{
    public interface ICustomerWishListBL
    {
        public ICollection<CustomerWishList> AddBookToCart(string CustomerID, long BookID);
    }
}
