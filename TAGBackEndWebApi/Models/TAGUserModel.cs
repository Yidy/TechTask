using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAGBackEndWebApi
{
    public class TAGUserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public string color { get; set; }
        public string pantone_value { get; set; }
    }
}