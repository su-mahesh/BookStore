using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class RequestBook
    {
        public long BookID { get; set; }
        [Required]
        public string BookName { get; set; }
        public string BookDiscription { get; set; }
        public string BookImage { get; set; }
        [Range(1, 1000000000, ErrorMessage = "Book Price must be between 1 to 100000")]
        public int BookPrice { get; set; }
        public string AuthorName { get; set; }
        [Range(0, 1000000000, ErrorMessage = "Quantity must be between 0 to 10000000")]
        public int Quantity { get; set; }
    }
}
