using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace BusinessLayer
{
    public interface IBookManagementBL
    {
        ResponseBook AddBook(RequestBook book);
        ICollection<ResponseBook> GetBooks();
        bool DeleteBook(long bookID);
        ResponseBook UpdateBook(RequestBook book);
    }
}
