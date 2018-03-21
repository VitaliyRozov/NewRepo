using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zabbixAPIConsole.function;
using zabbixAPIConsole.data_class;

namespace zabbixAPIConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length == 0) {
                Console.Write("Не заданы параметры:\n" +
                    "-perod \"month или week\"\n" +
                    "-path \"путь до файла\"\n" +
                    "-groupids \"ID группа хостов скоторых необходимы данные\"\n" +
                    "-templateid \"ID триггера по которому необходимы данные\"\n");
                Console.ReadKey();
                return;
            }

            string period = "";

            for (int q = 0; q < args.Length; q++)
            {
                switch (args[q])
                {
                    case "-perod":
                        if (args[q + 1] == "month") period = "month";
                        if (args[q + 1] == "week") period = "week";
                        if (period == "")
                        {
                            Console.Write("Параметр -period не задан");
                            return;
                        }
                        break;
                    case "-path":
                        if (args[q + 1] == "month") period = "month";
                        if (args[q + 1] == "week") period = "week";
                        if (period == "")
                        {
                            Console.Write("Параметр -period не задан");
                            return;
                        }
                        break;
                }
            }
            


            ///////////////////Аутентификация

            // JSON запрос 
            Autch.Request ARequest = new Autch.Request
            {
                jsonrpc = "2.0",
                method = "user.login",
                id = 1,
                auth = null,
                @params = new Autch.RequestParams
                {
                    user = "Admin",
                    password = "zabbix"
                }
            };

            //Сериализация данных в json строку
            string json = JsonConvert.SerializeObject(ARequest);
            //Отправка запроса
            string result = http_get.httpReq(json);
            //Десериализация результата в объект
            Autch.Result AResult = JsonConvert.DeserializeObject<Autch.Result>(result);



            //Получаем узлы из нужной нам груупы

            Host.Request HRequest = new Host.Request
            {
                jsonrpc = "2.0",
                method = "host.get",
                id = 1,
                auth = AResult.result,
                @params = new Host.RequestParams
                {
                    groupids = "8",
                }
            };

            //Сериализация данных в json строку
            json = JsonConvert.SerializeObject(HRequest);
            //Отправка запроса
            result = http_get.httpReq(json);
            //Десериализация результата в объект
            Host.Result HResult = JsonConvert.DeserializeObject<Host.Result>(result);

            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Temp\event.csv");


            //Пробегаемся по всем хостам и вытягиваем все события с необходимыми тригерами

            for (int x=0; x<HResult.result.Count(); x++ )
            {


                Trigger.Request TRequest = new Trigger.Request
                {
                    jsonrpc = "2.0",
                    method = "trigger.get",
                    @params = new Trigger.RequestParams
                    {
                        hostids = HResult.result[x].hostid,
                    },
                    auth = AResult.result,
                    id = 1
                };

                //Сериализация данных в json строку
                json = JsonConvert.SerializeObject(TRequest);
                //Отправка запроса
                result = http_get.httpReq(json);
                //Десериализация результата в объект
                Trigger.Result TResult = JsonConvert.DeserializeObject<Trigger.Result>(result);


                //Пройдемся по всем тригерам и найдем ИД того, у которого ИД шаблона 13554

                for (int y = 0; y < TResult.result.Count(); y++)
                {

                    if (TResult.result[y].templateid == "14001")
                    {

                        //Для найденого тригера вытацим события

                        Event.Request ERequest = new Event.Request
                        {
                            jsonrpc = "2.0",
                            method = "event.get",
                            @params = new Event.RequestParams
                            {
                                output = "extend",
                                time_from = unixtime.ConvertToUnixTimestamp(DateTime.Now.AddMonths(-1)),
                                select_acknowledges = "extend",
                                hostids = HResult.result[x].hostid,
                                sortfield = new List<string>(new string[] { "clock", "eventid" }),
                                sortorder = "ACS",
                                objectids = TResult.result[y].triggerid
                            },
                            auth = AResult.result,
                            id = 1
                        };


                        //Сериализация данных в json строку
                        json = JsonConvert.SerializeObject(ERequest);
                        //Отправка запроса
                        result = http_get.httpReq(json);
                        //Десериализация результата в объект
                        Event.Result EResult = JsonConvert.DeserializeObject<Event.Result>(result);



                        //Перебираем события
                        for(int z = 0; z < EResult.result.Count(); z++)
                        {
                            //Пишем в файлик

                            file.Write(HResult.result[x].name);
                            file.Write(";");
                            file.Write(HResult.result[x].host);
                            file.Write(";");

                            if (EResult.result[z].value == "0")
                            {
                                if (z == 0) {
                                    file.Write(";");
                                    file.Write(";");
                                    file.Write(TimeZoneInfo.ConvertTimeFromUtc(unixtime.ConvertFromUnixTimestamp(EResult.result[z].clock), TimeZoneInfo.Local).ToString("dd.MM.yyyy hh:mm:ss"));
                                }
                            } else if (EResult.result[z].value == "1")
                            {
                                if (z == EResult.result.Count()-1)
                                {
                                    file.Write(TimeZoneInfo.ConvertTimeFromUtc(unixtime.ConvertFromUnixTimestamp(EResult.result[z].clock), TimeZoneInfo.Local).ToString("dd.MM.yyyy hh:mm:ss"));
                                    file.Write(";");
                                    file.Write(";");
                                    file.Write(";");
                                } else { 
                                    file.Write(TimeZoneInfo.ConvertTimeFromUtc(unixtime.ConvertFromUnixTimestamp(EResult.result[z].clock), TimeZoneInfo.Local).ToString("dd.MM.yyyy hh:mm:ss"));
                                    file.Write(";");
                                    file.Write(EResult.result[z + 1].clock - EResult.result[z].clock);
                                    file.Write(";");
                                    file.Write(TimeZoneInfo.ConvertTimeFromUtc(unixtime.ConvertFromUnixTimestamp(EResult.result[z+1].clock), TimeZoneInfo.Local).ToString("dd.MM.yyyy hh:mm:ss"));
                                    file.Write(";");
                                    z = z + 1;
                                }
                            }
                            

                            file.Write("\n");
                        }

                        
                        


                    }
                }

           



            }

            file.Close();
            Console.WriteLine(HResult.result.Count());
            Console.ReadKey();
        }
    }
}
