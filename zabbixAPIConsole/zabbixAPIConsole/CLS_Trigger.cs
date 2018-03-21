using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zabbixAPIConsole
{
    class CLS_Trigger
    {

        public class RTrigger
        {
            public string jsonrpc { get; set; }
            public string method { get; set; }
            public PTrigger @params { get; set; }
            public string auth { get; set; }
            public int id { get; set; }
        }

        public class PTrigger
        {
            public string group { get; set; }
//            public PTriggerOutput @output { get; set; }
            public PTriggerFilter @filter { get; set; }
            public string output { get; set; }
//            public int selectLastEvent { get; set; }
            public int expandDescription { get; set; }
            public List<string> templateids { get; set; }
        }

        public class PTriggerOutput
        {
            public int triggerid { get; set; }
            public int description { get; set; }
        }

        public class PTriggerFilter
        {
            public int value { get; set; }
        }

        


        public class TResult
        {
            public string jsonrpc { get; set; }
            public List<TResults> result { get; set; }
            public int id { get; set; }
        }

        public class TResults
        {
            public string triggerid { get; set; }
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
