using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class BookTypeAuthor
    {
        public List<Books> books { get; set; }
        public List<Authors> auth { get; set; }
        public List<Types> types { get; set; }
       
    }
}