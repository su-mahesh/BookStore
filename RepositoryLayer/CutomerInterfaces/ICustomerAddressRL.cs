using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;

namespace RepositoryLayer.CutomerInterfaces
{
    public interface ICustomerAddressRL
    {
        bool AddCustomerAddress(CustomerAddress address);
    }
}
