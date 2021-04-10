using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;

namespace RepositoryLayer.AdminInterfaces
{
    public interface IBookManagementRL
    {
        bool AddBook(RequestBook book);
    }
}
