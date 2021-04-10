using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.ResponseModel;

namespace RepositoryLayer.CutomerInterfaces
{
    public interface ICustomerWishListRL
    {
        public ICollection<CustomerWishList> AddBookToCart(string CustomerID, long BookID);
    }
}
