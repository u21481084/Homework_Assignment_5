using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Books
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public int PageCount { get; set; }
        public int Points { get; set; }
        public string Status { get; set; }


       
    }
}