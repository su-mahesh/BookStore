using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class CustomerCart
    {
        public long CartID { get; set; }
        public string BookName { get; set; }
        public string CustomerID { get; set; }
        public long BookID { get; set; }
        public int BookPrice { get; set; }
    }
}
