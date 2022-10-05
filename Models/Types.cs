using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Types
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }

        public List<Types> typenames { get;set; }
    }
}