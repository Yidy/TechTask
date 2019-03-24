using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TAGBackEndWebApi
{
    public class TAGUserResultModel
    {
        public int page { get; set; } = 0;
        public int per_page { get; set; } = 0;
        public int total { get; set; } = 0;
        public int total_pages { get; set; } = 0;
        public IEnumerable <TAGUserModel> data { get; set; }

    }
}