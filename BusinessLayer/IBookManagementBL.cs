using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace BusinessLayer
{
    public interface IBookManagementBL
    {
        bool AddBook(RequestBook book);
        ICollection<ResponseBook> GetBooks();
    }
}
