using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Globalization;
using System.Windows.Forms;

namespace FISHER
{
    /// <summary>
    /// Класс-обработчик клиента
    /// </summary>
    class Client
    {
        /// <summary>
        /// Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        /// </summary>
        /// <param name="Client"></param>
        public Client(TcpClient Client)
        {
            try
            {
                bool disconnect = false;

                while ((!Loger.closeAllThreads) & (!disconnect))
                {
                    // Объявим строку, в которой будет хранится запрос клиента
                    string Request = "";
                    // Буфер для хранения принятых от клиента данных
                    byte[] Buffer = new byte[8];
                    // Переменная для хранения количества байт, принятых от клиента
                    int Count;

                    while ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0)
                    {
                        //stop_send = true;

                        Request += Encoding.UTF8.GetString(Buffer, 0, Count);

                        // Пакет пришел полностью
                        if (Request.IndexOf("<end>") >= 0)
                        {
                            break;
                        }

                        // Превышен разумный размер пакета
                        if (Request.Length > 4096)
                        {
                            break;
                        }
                    }

                    Request = Request.Replace("<end>", "");

                    if (Request == "") return;
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Packet));
                    Packet packet = (Packet)ser.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(Request)));

                    if (packet.cmd != null)
                    {
                        ClientInfo clientinfo;
                        switch (packet.cmd)
                        {
                            case "connection":
                                // Добавляем в список клиентов
                                TcpServer.ListOfClients.Add(new ClientInfo(ref Client));
                                // Получаем ссылку на экземпляр клиента
                                clientinfo = TcpServer.ListOfClients.Find(ci => ci.Client.Equals(Client));

                                // Сохраняем имя подключенного клиента
                                // Определяем каккая версия клиента подключена
                                if (packet.nameClient == null)
                                {
                                    // В старой версии протокола предпологалось, что клиентам 
                                    // присваевается идентификатор при регистрации
                                    byte[] number = Encoding.UTF8.GetBytes("385");
                                    Client.GetStream().Write(number, 0, number.Length);
                                    clientinfo.Name = "Старая версия клиента 'Хариус'";
                                }
                                else
                                {
                                    // В новой версии протокола можно передавать версию сервера
                                    // при подключении. А можно и не передавать :)
                                    byte[] number = Encoding.UTF8.GetBytes(Application.ProductVersion.ToString());
                                    Client.GetStream().Write(number, 0, number.Length);
                                    clientinfo.Name = packet.nameClient.ToString();
                                }
                                // Сохраняем IP-адресс подключенного клиента
                                clientinfo.IpAdress = ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString();
                                // Запись в лог-файл
                                Loger.WriteToFile("Подключен новый клиент '" +
                                    clientinfo.Name + "' (ip: " + clientinfo.IpAdress + ") ...");
                                break;

                            case "disconnect":
                                disconnect = true;
                                // Запись в лог-файл
                                Loger.WriteToFile("Отключился клиент " +
                                    Convert.ToString(packet.nameClient) + " " +
                                    Convert.ToString(((IPEndPoint)Client.Client.LocalEndPoint).Address) + " ...");
                                break;

                            case "refresh":
                                NevodCommand refreshCmd = new NevodCommand("refresh", packet.numberKP, packet.parametr);
                                // Получаем ссылку на экземпляр клиента
                                clientinfo = TcpServer.ListOfClients.Find(ci => ci.Client.Equals(Client));
                                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.TcpServer,
                                    "Клиент '" + clientinfo.Name + "' (ip: " + clientinfo.IpAdress +
                                    ") принудительно запросил обновить данные для КП-" +
                                    Convert.ToString(packet.numberKP));
                                DataServer.hendCommmands.Add(refreshCmd);
                                DataServer.WaitHandler.Set();
                                break;

                            case "switch":
                                // Получаем ссылку на экземпляр клиента
                                clientinfo = TcpServer.ListOfClients.Find(ci => ci.Client.Equals(Client));
                                // Проверяем есть ли у пользователя права на телеуправление
                                if (TcpServer.ListOfUsers.FindIndex(
                                    name => name == Convert.ToString(packet.nameClient)) == -1) 
                                {
                                    // Запись в лог-файл
                                    Loger.WriteToFile("Клиент '" + clientinfo.Name + "' (ip: " + clientinfo.IpAdress +
                                        ") заблокирована операция кратковременной подачи напряжения.  КП №" +
                                        Convert.ToString(packet.numberKP) + " выход №" + Convert.ToString(packet.parametr) +
                                        "Причина: нет прав на телеуправление.");
                                    break;
                                }
                                // Добавить операцию телеуправления в список команд
                                NevodCommand tempCmd = new NevodCommand("switch", packet.numberKP, packet.parametr);
                                DataServer.hendCommmands.Add(tempCmd);
                                DataServer.WaitHandler.Set();
                                // Запись в лог-файл
                                Loger.WriteToFile("Клиент '" + clientinfo.Name + "' (ip: " + clientinfo.IpAdress +
                                    ") запустил операцию кратковременной подачи напряжения.  КП №" +
                                    Convert.ToString(packet.numberKP) + " выход №" + Convert.ToString(packet.parametr));
                                break;

                            case "on":
                                // Получаем ссылку на экземпляр клиента
                                clientinfo = TcpServer.ListOfClients.Find(ci => ci.Client.Equals(Client));
                                // Проверяем есть ли у пользователя права на телеуправление
                                if (TcpServer.ListOfUsers.FindIndex(
                                    name => name == Convert.ToString(packet.nameClient)) == -1)
                                {
                                    // Запись в лог-файл
                                    Loger.WriteToFile("Клиент '" + clientinfo.Name + "' (ip: " + clientinfo.IpAdress +
                                        ") заблокирована операция ТУ. Подать напряжение на КП №" +
                                        Convert.ToString(packet.numberKP) + " выход №" + Convert.ToString(packet.parametr) +
                                        "Причина: нет прав на телеуправление.");
                                    break;
                                }
                                // Добавить операцию телеуправления в список команд
                                NevodCommand onCmd = new NevodCommand("on", packet.numberKP, packet.parametr);
                                DataServer.hendCommmands.Add(onCmd);
                                DataServer.WaitHandler.Set();
                                // Запись в лог-файл
                                Loger.WriteToFile("Клиент '" + clientinfo.Name + "' (ip: " + clientinfo.IpAdress +
                                    ") запустил операцию ТУ. Подать напряжение на КП №" +
                                    Convert.ToString(packet.numberKP) + " выход №" + Convert.ToString(packet.parametr));
                                break;

                            case "off":
                                // Получаем ссылку на экземпляр клиента
                                clientinfo = TcpServer.ListOfClients.Find(ci => ci.Client.Equals(Client));
                                // Проверяем есть ли у пользователя права на телеуправление
                                if (TcpServer.ListOfUsers.FindIndex(
                                    name => name == Convert.ToString(packet.nameClient)) == -1)
                                {
                                    // Запись в лог-файл
                                    Loger.WriteToFile("Клиент '" + clientinfo.Name + "' (ip: " + clientinfo.IpAdress +
                                        ") заблокирована операция ТУ. Снять напряжение c КП №" +
                                        Convert.ToString(packet.numberKP) + " выход №" + Convert.ToString(packet.parametr) +
                                        "Причина: нет прав на телеуправление.");
                                    break;
                                }
                                // Добавить операцию телеуправления в список команд
                                NevodCommand offCmd = new NevodCommand("off", packet.numberKP, packet.parametr);
                                DataServer.hendCommmands.Add(offCmd);
                                DataServer.WaitHandler.Set();
                                // Запись в лог-файл
                                Loger.WriteToFile("Клиент '" + clientinfo.Name + "' (ip: " + clientinfo.IpAdress +
                                    ") запустил операцию ТУ. Снять напряжение c КП №" +
                                    Convert.ToString(packet.numberKP) + " выход №" + Convert.ToString(packet.parametr));
                                break;

                            default:
                                break;
                        }
                    }
                }

                // Получаем ссылку на экземпляр клиента
                ClientInfo cinfo = TcpServer.ListOfClients.Find(ci => ci.Client.Equals(Client));
                // Запись в лог-файл
                Loger.WriteToFile("Отключился клиент '" + cinfo.Name + "' (ip: " + cinfo.IpAdress + ") ...");
                // Удаляем клиента из списка подписчиков
                TcpServer.ListOfClients.Remove(TcpServer.ListOfClients.Find(ci => ci.Client.Equals(Client)));
                // Закрываем соединение
                Client.Close();
            }
            catch (Exception exp)
            {
                Console.WriteLine(TcpServer.ListOfClients.Count);

                //for (int i = 0; i < TcpServer.ListOfClients.Count; i++)
                //    if (TcpServer.ListOfClients[i].Client == Client)
                //    {
                //        // Получаем ссылку на экземпляр клиента
                //        ClientInfo cinfo = TcpServer.ListOfClients.Find(ci => ci.Client.Equals(Client));
                //        // Запись в лог-файл
                //        Loger.WriteToFile("Потеряно соединение с клиентом '" + cinfo.Name +
                //            "' (ip: " + cinfo.IpAdress + ") ...");
                //        // Удаляем клиента из списка
                //        TcpServer.ListOfClients.RemoveAt(i);
                //    }

                // Получаем ссылку на экземпляр клиента
                ClientInfo cinfo = TcpServer.ListOfClients.Find(ci => ci.Client.Equals(Client));
                // Запись в лог-файл
                Loger.WriteToFile("Потеряно соединение с клиентом '" + cinfo.Name +
                    "' (ip: " + cinfo.IpAdress + ") ...");
                // Удаляем экземпляр клиента из списка
                TcpServer.ListOfClients.Remove(cinfo);

                Console.WriteLine(TcpServer.ListOfClients.Count);

                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.TcpServer,
                    exp.Message);
            }
        }
    }

    class TcpServer
    {
        /// <summary>
        /// Список подключенных клиентов
        /// </summary>
        public static List<ClientInfo> ListOfClients = new List<ClientInfo>();
        /// <summary>
        /// Список пользователей с правами на телеуправление
        /// </summary>
        public static List<string> ListOfUsers = new List<string>();

        /// <summary>
        /// Объект, принимающий TCP-клиентов
        /// </summary>
        TcpListener Listener;

        /// <summary>
        /// Номер прослушиваемого порта
        /// </summary>
        public static int Port = 12345;

        /// <summary>
        /// Запуск сервера
        /// </summary>
        public TcpServer()
        {
            // Создаем "слушателя" для указанного порта
            Listener = new TcpListener(IPAddress.Any, Port);
            // Запускаем его
            Listener.Start();

            // В бесконечном цикле
            while (!Loger.closeAllThreads)
            {
                // Принимаем нового клиента
                TcpClient Client = Listener.AcceptTcpClient();
                // Создаем поток
                Thread thread = new Thread(new ParameterizedThreadStart(ClientThread));
                thread.IsBackground = true;
                thread.Name = "TcpClient Thread IP:" + ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString();
                Console.WriteLine(thread.Name);
                // И запускаем этот поток, передавая ему принятого клиента
                thread.Start(Client);
                Thread.Sleep(2000);
            }
        }

        static void ClientThread(Object StateInfo)
        {
            // Просто создаем новый экземпляр класса Client
            // и передаем ему приведенный к классу TcpClient объект StateInfo
            new Client((TcpClient)StateInfo);
        }

        /// <summary>
        /// Отправляет данные всем подключенным TCP-клиентам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SendDataClient(object sender, MsgArrivedEventArgs e)
        {
            if (TcpServer.ListOfClients.Count == 0) return;
            ModulNevod modul = sender as ModulNevod;

            // Отправка результата клиентам
            string data =
                "{\"NumberInt\":" + Convert.ToString(modul.inKpNum) + "," +
                "\"TI\":[" +
                modul.AnIn1.ToString(NumberFormatInfo.InvariantInfo) + "," +
                modul.AnIn2.ToString(NumberFormatInfo.InvariantInfo) + "," +
                modul.AnIn3.ToString(NumberFormatInfo.InvariantInfo) + "," +
                modul.AnIn4.ToString(NumberFormatInfo.InvariantInfo) + "," +
                modul.Temperature.ToString(NumberFormatInfo.InvariantInfo) + "],";

            // Дискретные переменные
            data +=
                "\"NumberDg\":" + Convert.ToString(modul.dgKpNum) + "," +
                "\"TS\":[" +
                Convert.ToString(modul.DgIn1) + "," +
                Convert.ToString(modul.DgIn2) + "," +
                Convert.ToString(modul.DgIn3) + "," +
                Convert.ToString(modul.DgIn4) + "," +
                Convert.ToString(modul.DgIn5) + "," +
                Convert.ToString(modul.DgIn6) + "," +
                Convert.ToString(modul.DgIn7) + "," +
                Convert.ToString(modul.DgIn8) + "],";

            // Время получения данных
            data +=
                "\"Time\":" +
                DateTime.Now.Ticks.ToString();

            data += "}";

            byte[] buffer = Encoding.UTF8.GetBytes(data);
            int size = 4 + buffer.Length;
            byte[] packet = new byte[size];

            // Структура пакета:
            // размер - время получения данных - данные

            // Указываем размер в начале пакета 

            BitConverter.GetBytes(size).CopyTo(packet, 0);
            // Копируем данные
            buffer.CopyTo(packet, 4);

            for (int i = 0; i < ListOfClients.Count; i++)
            {
                // Здесь возникает ошибка, когда аварийно отключается клиент хоть и редко
                // try блок нас спасет...
                try
                {
                    ListOfClients[i].Client.GetStream().Write(packet, 0, packet.Length);
                    int a = ListOfClients[i].GetHashCode();
                }
                catch (Exception exp)
                {
                    Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                        exp.Message);
                    // Запись в лог-файл
                    Loger.WriteToFile("Потерянно соединение с клиентом №" +
                        ListOfClients[i].ToString());
                    ListOfClients.RemoveAt(i);
                    i = 0;
                }
            }
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        ~TcpServer()
        {
            // Если "слушатель" был создан
            if (Listener != null)
            {
                // Остановим его
                Listener.Stop();
            }
        }
    }

    public class Packet
    {
        public object nameClient { get; set; }
        public string cmd { get; set; }
        public int numberKP { get; set; }
        public object parametr { get; set; }
    }

    public class NetData
    {
        public int numberKp;
        public double[] measurement;
    }

    /// <summary>
    /// Хранит ссылки на объекты Client
    /// и информацию о подключенных пользоватеях
    /// </summary>
    public class ClientInfo : object
    {
        /// <summary>
        /// Ссылка на объект клиента
        /// </summary>
        public TcpClient Client { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// IP-адресс
        /// </summary>
        public string IpAdress { get; set; }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="tcpClient"></param>
        public ClientInfo(ref TcpClient tcpClient)
        {
            Client = tcpClient;
            Name = "#";
            IpAdress = "#.#.#.#";
        }
    }
}
