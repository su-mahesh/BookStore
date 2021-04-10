using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class RequestBook
    {
        public string BookName { get; set; }
        public string BookDiscription { get; set; }
        public string BookImage { get; set; }
        public int BookPrice { get; set; }
        public string AuthorName { get; set; }
    }
}
