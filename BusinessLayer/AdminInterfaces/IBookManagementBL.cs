using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace BusinessLayer.AdminInterfaces
{
    public interface IBookManagementBL
    {
        bool AddBook(RequestBook book);
        ICollection<ResponseBook> GetBooks();
    }
}
