using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zabbixAPIConsole.data_class
{
    class Host
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
            public string groupids { get; set; }
        }


        public class Result
        {
            public string jsonrpc { get; set; }
            public List<ResultResult> result { get; set; }
            public int id { get; set; }
        }
        public class ResultResult
        {
            public string hostid { get; set; }
            public string proxy_hostid { get; set; }
            public string host { get; set; }
            public string status { get; set; }
            public string disable_until { get; set; }
            public string error { get; set; }
            public string available { get; set; }
            public string errors_from { get; set; }
            public string lastaccess { get; set; }
            public string ipmi_authtype { get; set; }
            public string ipmi_privilege { get; set; }
            public string ipmi_username { get; set; }
            public string ipmi_password { get; set; }
            public string ipmi_disable_until { get; set; }
            public string ipmi_available { get; set; }
            public string snmp_disable_until { get; set; }
            public string snmp_available { get; set; }
            public string maintenanceid { get; set; }
            public string maintenance_status { get; set; }
            public string maintenance_type { get; set; }
            public string maintenance_from { get; set; }
            public string ipmi_errors_from { get; set; }
            public string snmp_errors_from { get; set; }
            public string ipmi_error { get; set; }
            public string snmp_error { get; set; }
            public string jmx_disable_until { get; set; }
            public string jmx_available { get; set; }
            public string jmx_errors_from { get; set; }
            public string jmx_error { get; set; }
            public string name { get; set; }
            public string flags { get; set; }
            public string templateid { get; set; }
            public string description { get; set; }
            public string tls_connect { get; set; }
            public string tls_accept { get; set; }
            public string tls_issuer { get; set; }
            public string tls_subject { get; set; }
            public string tls_psk_identity { get; set; }
            public string tls_psk { get; set; }
        }


    }
}
