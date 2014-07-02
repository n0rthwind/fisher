using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace FISHER
{
    static public class Communicators
    {
        /// <summary>
        /// COM-порт для работы с устройствами
        /// </summary>
        public static SerialPort comPort;
        /// <summary>
        /// Инициализирует или изменят настройки COM-порта
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="paryty"></param>
        /// <param name="stopBits"></param>
        /// <param name="readtimeOut"></param>
        public static bool Initialization(string portName = "COM1", int baudRate = 19200, Parity parity = Parity.None,
            StopBits stopBits = StopBits.One, int readtimeOut = 900)
        {
            // Хранит состояние был ли открыт порт
            bool wasOpen = false;

            if (comPort == null)
            {
                if ((portName == null) || (portName == "")) return false;//portName = "COM1";
                comPort = new SerialPort(portName, baudRate, parity, 8, stopBits);
            }
            else
            {
                // Закрыть порт при необходимости
                if (comPort.IsOpen)
                {
                    wasOpen = true;
                    comPort.Close();
                    Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.TcpServer,
                        Communicators.comPort.PortName + " закрыт...");
                }
                // Изменить настройки
                comPort.PortName = portName;
                comPort.BaudRate = baudRate;
                comPort.Parity = parity;
                comPort.DataBits = 8;
                comPort.StopBits = stopBits;
            }
            // Задать срок ожидания ответа от устройства
            comPort.ReadTimeout = readtimeOut;

            // Открыть порт, если необходимо
            if (wasOpen)
            {
                try
                {
                    comPort.Open();
                    Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.TcpServer,
                            Communicators.comPort.PortName + " открыт...");
                }
                catch 
                { }
            }

            return true;
        }
        /// <summary>
        /// Открывает выбранный СОМ-порт
        /// </summary>
        public static void CommunicationOpen()
        {
            if (comPort == null) Initialization();
            comPort.Open();
        }
        /// <summary>
        /// Закрывает сом-порт
        /// </summary>
        public static void CommunicationClose()
        {
            comPort.Close();
        }
        /// <summary>
        /// Очищает буфер от ложных данных 
        /// </summary>
        public static void ClearPortBuffer(ModulNevod modulNevod)
        {
            if (comPort == null) return;
            if (!comPort.IsOpen) return;
            if (comPort.BytesToRead > 0)
            {
                byte[] buffer = new byte[254];
                comPort.Read(buffer, 0, buffer.Length);
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.TcpServer,
                        "Очистка буфера...");
                // Увеличиваем счетчик ложных пакетов модуля
                modulNevod.WrongPackets++;
                // Увеличиваем счетчик ложных пакетов всех модулей
                WrongPackets++;
            }
            else
            {
                // Увеличиваем счетчик потерянных пкетов модуля
                modulNevod.LastPackets++;
                // Увеличиваем счетчик потерянных пкетов всех модулей
                LastPackets++;
            }
        }
        /// <summary>
        /// Количество ошибок в приеме/передаче данных
        /// </summary>
        public static long WrongPackets = 0;
        /// <summary>
        /// Количество потерянных пакетов
        /// </summary>
        public static long LastPackets = 0;
        /// <summary>
        /// Переводит значение бита четности в рускоязычный аналог 
        /// </summary>
        /// <param name="parity">Значение бита четности</param>
        /// <returns>Рускоязычное значение бита четности</returns>
        public static string ParityToStr(Parity parity)
        {
            switch (parity)
            { 
                case Parity.Even:
                    return "нет";
                case Parity.Odd:
                    return "нечетный";
                case Parity.None:
                    return "четный";
                default:
                    return parity.ToString();
            }
        }
        /// <summary>
        /// Переводит количество стоповых битов в строку
        /// </summary>
        /// <param name="stopBits">Число стоповых битов</param>
        /// <returns>Значение количества стоповых бит</returns>
        public static string StopBitsToStr(StopBits stopBits)
        {
            switch (stopBits)
            { 
                case StopBits.None:
                    return "0";
                case StopBits.One:
                    return "1";
                case StopBits.OnePointFive:
                    return "1.5";
                case StopBits.Two:
                    return "2";
                default:
                    return stopBits.ToString();
            }
        }
    }
}
