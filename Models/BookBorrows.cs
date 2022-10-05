using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BookBorrows
    {
     
       public int bBorrowID { get; set; }
        public int bStudentID { get; set; }
        public string bTakenDate { get; set; }
        public string bBroughtDate { get; set; }
        public string bStudentName { get; set; }
        public string status { get; set; }
    }
}