using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class StudentClassVM
    {
        public List<Students> stds { get; set; }
        public List<Class> cClass { get; set; }
        public int bID { get; set; }

        public int borrowid { get; set; }
    }
}