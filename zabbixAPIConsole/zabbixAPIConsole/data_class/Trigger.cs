using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zabbixAPIConsole.data_class
{
    class Trigger
    {
        public class Request
        {
            public string jsonrpc { get; set; }
            public string method { get; set; }
            public RequestParams @params { get; set; }
            public string auth { get; set; }
            public int id { get; set; }
        }

        public class RequestParams
        {
            public string hostids { get; set; }
        }

        public class Result
        {
            public string jsonrpc { get; set; }
            public List<ResultResult> result { get; set; }
            public int id { get; set; }
        }

        public class ResultResult
        {
            public int triggerid { get; set; }
            public string expression { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string status { get; set; }
            public string value { get; set; }
            public string priority { get; set; }
            public string lastchange { get; set; }
            public string comments { get; set; }
            public string error { get; set; }
            public string templateid { get; set; }
            public string type { get; set; }
            public string state { get; set; }
            public string flags { get; set; }
            public string recovery_mode { get; set; }
            public string recovery_expression { get; set; }
            public string correlation_mode { get; set; }
            public string correlation_tag { get; set; }
            public string manual_close { get; set; }
        }
    }
}
