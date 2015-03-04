using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

        public static List<ClassLinks> LoadLinksListFromFile(string path)
        {
            if (!File.Exists(path))
            {
                using (var stream = File.CreateText(path))
                {
                    stream.WriteLine("0;name;host;port;servicename;user;pass;;;;;");
                }
            }
            var users = new List<ClassLinks>();

            foreach (var line in File.ReadAllLines(path))
            {
                var columns = line.Split(';');
                users.Add(new ClassLinks
                {
                    select = false,
                    name = columns[1],
                    host = columns[2],
                    port = columns[3],
                    servicename = columns[4],
                    user = columns[5],
                    pass = columns[6],
                    result = columns[7],
                    object_type = columns[8],
                    object_status = columns[9],
                    last_ddl_time = columns[10]
                });
            }

            return users;
        }
    }
}
