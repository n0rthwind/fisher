using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace FISHER
{
    /// <summary>
    ///  Сисок id модулей
    /// </summary>
    public enum MessagerId
    {
        Log = 1,
        DataServer = 2,
        TcpServer = 3,
        Application = 4
    }

    /// <summary>
    ///  Структура для "общения" модулей
    /// </summary>
    public struct TMessager
    {
        public int tIdAuth;
        public int tIdReceiv;
        public string tMessage;
    }

    /// <summary>
    ///  Класс для ведения логов и записи сообщений в лог-файл
    /// </summary>
    static class Loger
    {
        /// <summary>
        /// Завершить работу всех потоков
        /// </summary>
        static volatile bool _closeAllThreads;

        public static bool closeAllThreads
        {
            get { return _closeAllThreads; }
            set { _closeAllThreads = value; }
        }

        /// <summary>
        /// Отправляет сообщение другому модулю программы
        /// </summary>
        /// <param name="idReciev">идентификатор получателя</param>
        /// <param name="idAuth">идентификатор автора</param>
        /// <param name="msg">сообщение</param>
        public static void SendMsg(int idReciev, int idAuth, string msg)
        {
            TMessager message = new TMessager();
            message.tIdReceiv = idReciev;
            message.tIdAuth = idAuth;
            message.tMessage = msg;

            // Если есть подписчики, сообщить, что данные обновились
            if (DataReceived != null) DataReceived(new LogerEventArgs(message));
        }
        /// <summary>
        /// Предоставляет метод, обрабатывающий событие DataReceived
        /// объекта Loger.
        /// </summary>
        /// <param name="e"> Cведения о событии </param>
        public delegate void DataReceivedEventHandler(LogerEventArgs e);
        /// <summary>
        /// Событие возникающее при получении нового сообщения.
        /// </summary>
        public static event DataReceivedEventHandler DataReceived;
        /// <summary>
        /// Записывает сообщение в лог-файл
        /// </summary>
        /// <param name="msg">Сообщениен для записи</param>
        /// <returns>Расшифровка ошибки</returns>
        public static string WriteToFile(string msg)
        {
            // Выводим сообщение в textBox
            Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.TcpServer, msg);

            // Записываем сообщение в лог-файл
            try
            {
                // Создать файл, если не создан
                if (!File.Exists("logs.txt")) File.Create("logs.txt");

                // Записать сообщение
                StringBuilder text = new StringBuilder("[" + DateTime.Now.ToString() + "] " + msg + Environment.NewLine);

                // Загрузить последние записи из файла
                using (StreamReader sr = File.OpenText("logs.txt"))
                {
                    // Ограничение по количеству символов в файле
                    const int size = 4*100000;

                    while ((sr.Peek() >= 0) & (text.Length < size))
                    {
                        text.Append(sr.ReadLine() + Environment.NewLine);
                    }
                }

                // Записать 
                using (StreamWriter sw = new StreamWriter(File.OpenWrite("logs.txt")))
                {
                    sw.Write(text);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return null;
        }
    }

    /// <summary>
    /// Содержит сообщение при добавлении логов
    /// </summary>
    public class LogerEventArgs : EventArgs
    {
        readonly TMessager message;

        public LogerEventArgs(TMessager message)
        {
            this.message = message;
        }

        public TMessager Message
        {
            get { return message; }
        }
    }

}
