using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace zabbixAPIConsole
{
    public class http_get
    {
        public static string httpReq(string JSON)
        {
            byte[] body = Encoding.UTF8.GetBytes(JSON);

            // Отправка запроса
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://172.28.0.250/zabbix/api_jsonrpc.php");

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
                stream.Close();
            }


            // Получение ответа
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                response.Close();
                return responseFromServer;
            }
        }
    }
}
