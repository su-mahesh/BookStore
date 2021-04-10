using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;

namespace BusinessLayer.AdminInterfaces
{
    public interface IBookManagementBL
    {
        bool AddBook(RequestBook book);
    }
}
