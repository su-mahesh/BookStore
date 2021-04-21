using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace RepositoryLayer
{
    public interface IBookManagementRL
    {
        ResponseBook AddBook(RequestBook book);
        ICollection<ResponseBook> GetCustomerBooks(string CustomerID);
        bool DeleteBook(long bookID);
        ResponseBook UpdateBook(RequestBook book);
    }
}
