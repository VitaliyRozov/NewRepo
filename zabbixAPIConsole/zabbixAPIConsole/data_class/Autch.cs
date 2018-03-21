using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zabbixAPIConsole.data_class
{
    class Autch
    {
        public class Request
        {
            public string jsonrpc { get; set; }
            public string method { get; set; }
            public RequestParams @params { get; set; }
            public int id { get; set; }
            public object auth { get; set; }
        }

        public class RequestParams
        {
            public string user { get; set; }
            public string password { get; set; }
        }

        public class Result
        {
            public string jsonrpc { get; set; }
            public string id { get; set; }
            public string result { get; set; }
        }

    }
}
