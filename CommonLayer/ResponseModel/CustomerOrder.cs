using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;

namespace CommonLayer.ResponseModel
{
    public class CustomerOrder
    {
        public long OrderID { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerID { get; set; }
        public int TotalCost { get; set; }
        public CustomerAddress Address { get; set; }
    }
}
