using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BookBorrowsVM
    {
        public List<BookBorrows> bb { get; set; }
        public string bbNameStatus { get; set; }
       public int ID { get; set; }
      
    }
}