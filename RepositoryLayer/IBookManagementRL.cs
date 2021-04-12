using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;

namespace RepositoryLayer
{
    public interface IBookManagementRL
    {
        bool AddBook(RequestBook book);
        ICollection<ResponseBook> GetBooks();
        bool DeleteBook(long bookID);
    }
}
