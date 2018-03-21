using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zabbixAPIConsole.data_class
{
    class Event
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
            public string output { get; set; }
            public double time_from { get; set; }
            public string select_acknowledges { get; set; }
            public string hostids { get; set; }
            public List<string> sortfield { get; set; }
            public string sortorder { get; set; }
            public int objectids { get; set;}
        }



        public class Result
        {
            public string jsonrpc { get; set; }
            public List<ResultResult> result { get; set; }
            public int id { get; set; }
        }

        public class ResultResult
        {
            public string eventid { get; set; }
            public string source { get; set; }
            public string @object { get; set; }
            public string objectid { get; set; }
            public double clock { get; set; }
            public string value { get; set; }
            public string acknowledged { get; set; }
            public string ns { get; set; }
            public string r_eventid { get; set; }
            public string c_eventid { get; set; }
            public string correlationid { get; set; }
            public string userid { get; set; }
            public List<object> acknowledges { get; set; }
        }
    }
}
