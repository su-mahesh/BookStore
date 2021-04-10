﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.ResponseModel;

namespace BusinessLayer.CustomerIntrfaces
{
    public interface ICustomerCartBL
    {
        public ICollection<CustomerCart> AddBookToCart(string CustomerID, long BookID);
        ICollection<CustomerCart> RemoveBookFromCart(string customerID, long bookID);
    }
}
