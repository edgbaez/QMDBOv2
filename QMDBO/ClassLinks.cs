using System;
using System.Collections.Generic;


namespace QMDBO
{
    public class ClassLinks
    {
        public bool select { get; set; }
        public string name { get; set; }
        public string host { get; set; }
        public string port { get; set; }
        public string servicename { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string result { get; set; }
        public string object_type { get; set; }
        public string object_status { get; set; }
        public string last_ddl_time { get; set; }
        public string hide_link_id { get; set; }

    }
}
