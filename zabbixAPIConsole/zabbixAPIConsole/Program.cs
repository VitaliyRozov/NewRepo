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

            // Работа с параметрами командной строки

            if (args.Length == 0 || args[0] == "-help") {
                Console.Write("Не заданы параметры:\n" +
                    "-login \t\t логин для досупа к Zabbix API\n" +
                    "-pass \t\t пароль для досупа к Zabbix API\n" +
                    "-url \t\t URL адрес Zabbix сервера\n" +
                    "-period \t month или week\n" +
                    "-file_path \t путь до файла\n" +
                    "-group_id \t ID группы хостов скоторых необходимы данные\n" +
                    "-template_id \t ID триггера по которому необходимы данные\n");
                Console.ReadKey();
                return;
            }

            string login = "";
            string pass = "";
            string url = "";
            DateTime period = DateTime.Today;
            string path = "";
            int groupids = -1;
            int templateid = -1;

            for (int q = 0; q < args.Length; q++)
            {
                switch (args[q])
                {

                    case "-login":
                        if (args.Length == (q + 1))
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        try
                        {
                            login = args[q + 1];
                            q += 1;
                        }
                        catch
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        break;

                    case "-pass":
                        if (args.Length == (q + 1))
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        try
                        {
                            pass = args[q + 1];
                            q += 1;
                        }
                        catch
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        break;

                    case "-url":
                        if (args.Length == (q + 1))
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        try
                            {
                                url = args[q + 1];
                                q += 1;
                            }
                            catch
                            {
                                Console.Write("Неправильное значение параметра {0} \n" +
                                        "Для получения справки запустите программу с параметром -help", args[q]);
                                Console.ReadKey();
                                return;
                            }
                        break;


                    case "-period":
                        if (args.Length == (q + 1))
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        switch (args[q + 1])
                        {
                            case "month":
                                period = DateTime.Today.AddMonths(-1);
                                break;
                            case "week":
                                period = DateTime.Today.AddDays(-7);
                                break;
                            default:
                                Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                                Console.ReadKey();
                                return;
                        }
                        q += 1;
                        break;


                    case "-file_path":
                        if (args.Length == (q + 1))
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        if (
                            (args[q + 1] == null) ||
                            (args[q + 1].IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1)
                           )
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        try
                        {
                            var tempFileInfo = new System.IO.FileInfo(args[q + 1]);
                            if(tempFileInfo.Attributes.ToString() == "Directory")
                                path = path = args[q + 1]+ "\\event.csv";
                            else
                                path = args[q + 1];
                        }
                        catch (NotSupportedException)
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        q += 1;
                        break;


                    case "-group_id":
                        if (args.Length == (q + 1))
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        try
                        {
                            groupids = Convert.ToInt32(args[q + 1]);
                            q += 1;
                        }
                        catch
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        break;


                    case "-template_id":
                        if (args.Length == (q + 1))
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        try
                        {
                            templateid = Convert.ToInt32(args[q + 1]);
                            q += 1;
                        }
                        catch
                        {
                            Console.Write("Неправильное значение параметра {0} \n" +
                                    "Для получения справки запустите программу с параметром -help", args[q]);
                            Console.ReadKey();
                            return;
                        }
                        break;


                    default:
                        Console.Write("Неизвестный параметр: " + args[q]);
                        Console.ReadKey();
                        return;
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
                    groupids = groupids,
                }
            };

            //Сериализация данных в json строку
            json = JsonConvert.SerializeObject(HRequest);
            //Отправка запроса
            result = http_get.httpReq(json);
            //Десериализация результата в объект
            Host.Result HResult = JsonConvert.DeserializeObject<Host.Result>(result);

            System.IO.StreamWriter file = null;
            try
            {
                file = new System.IO.StreamWriter(path);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(
                    "{0}: {1}",
                    e.GetType().Name, e.Message);
                Console.ReadKey();
                return;
            }


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

                    if (TResult.result[y].templateid == templateid.ToString())
                    {

                        //Для найденого тригера вытацим события

                        Event.Request ERequest = new Event.Request
                        {
                            jsonrpc = "2.0",
                            method = "event.get",
                            @params = new Event.RequestParams
                            {
                                output = "extend",
                                time_from = unixtime.ConvertToUnixTimestamp(period),
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
                                    file.Write(period);
                                    file.Write(";");
                                    file.Write(EResult.result[z].clock - unixtime.ConvertToUnixTimestamp(TimeZoneInfo.ConvertTimeToUtc(period)));
                                    file.Write(";");
                                    file.Write(TimeZoneInfo.ConvertTimeFromUtc(unixtime.ConvertFromUnixTimestamp(EResult.result[z].clock), TimeZoneInfo.Local).ToString());
                                }
                            } else if (EResult.result[z].value == "1")
                            {
                                if (z == EResult.result.Count()-1)
                                {
                                    file.Write(TimeZoneInfo.ConvertTimeFromUtc(unixtime.ConvertFromUnixTimestamp(EResult.result[z].clock), TimeZoneInfo.Local).ToString());
                                    file.Write(";");
                                    file.Write(unixtime.ConvertToUnixTimestamp(TimeZoneInfo.ConvertTimeToUtc(DateTime.Now)) - EResult.result[z].clock);
                                    file.Write(";");
                                    file.Write(DateTime.Now);
                                } else { 
                                    file.Write(TimeZoneInfo.ConvertTimeFromUtc(unixtime.ConvertFromUnixTimestamp(EResult.result[z].clock), TimeZoneInfo.Local).ToString());
                                    file.Write(";");
                                    file.Write(EResult.result[z + 1].clock - EResult.result[z].clock);
                                    file.Write(";");
                                    file.Write(TimeZoneInfo.ConvertTimeFromUtc(unixtime.ConvertFromUnixTimestamp(EResult.result[z+1].clock), TimeZoneInfo.Local).ToString());
                                    z = z + 1;
                                }
                            }
                            

                            file.Write("\n");
                        }

                        
                        


                    }
                }
            }

            file.Close();
//            Console.WriteLine(HResult.result.Count());
//            Console.ReadKey();
        }
    }
}
