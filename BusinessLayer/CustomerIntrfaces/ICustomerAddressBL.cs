using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;

namespace BusinessLayer.CustomerIntrfaces
{
    public interface ICustomerAddressBL
    {
        bool AddCustomerAddress(CustomerAddress address);
    }
}
