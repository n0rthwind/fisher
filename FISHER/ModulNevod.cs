using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FISHER
{
    public class ModulNevod
    {
        /// <summary>
        /// Задержка для операции ТУ, перед выключением агригата
        /// </summary>
        public static int TimeoutTU = 2000;
        /// <summary>
        /// Время ожидания после опреации ТУ 
        /// перед тем как обновить данные.
        /// </summary>
        static int TimeoutAfterTU = 120;

        // Номера КП
        private int _inKpNum;
        private int _dgKpNum;

        // Аналоговые входы
        private double _AnIn1;
        private double _AnIn2;
        private double _AnIn3;
        private double _AnIn4;
        private double _temperature;

        // Дискретные входы
        private bool _DgIn1;
        private bool _DgIn2;
        private bool _DgIn3;
        private bool _DgIn4;
        private bool _DgIn5;
        private bool _DgIn6;
        private bool _DgIn7;
        private bool _DgIn8;

        // Дискретные выходы
        private bool _DgOut1;
        private bool _DgOut2;
        private bool _DgOut3;
        private bool _DgOut4;
        private bool _DgOut5;
        private bool _DgOut6;
        private bool _DgOut7;
        private bool _DgOut8;

        // Название модуля
        public string ModulName { get; set; }

        // Номера КП
        public int inKpNum { get { return _inKpNum; } }
        public int dgKpNum { get { return _dgKpNum; } }

        // Аналоговые входы
        public double AnIn1 { get { return _AnIn1; } }
        public double AnIn2 { get { return _AnIn2; } }
        public double AnIn3 { get { return _AnIn3; } }
        public double AnIn4 { get { return _AnIn4; } }
        public double Temperature { get { return _temperature; } }

        // Дискретные входы
        public bool DgIn1 { get { return _DgIn1; } }
        public bool DgIn2 { get { return _DgIn2; } }
        public bool DgIn3 { get { return _DgIn3; } }
        public bool DgIn4 { get { return _DgIn4; } }
        public bool DgIn5 { get { return _DgIn5; } }
        public bool DgIn6 { get { return _DgIn6; } }
        public bool DgIn7 { get { return _DgIn7; } }
        public bool DgIn8 { get { return _DgIn8; } }

        // Дискретные входы
        public bool DgOut1 { get { return _DgOut1; } set { _DgOut1 = value; } }
        public bool DgOut2 { get { return _DgOut2; } set { _DgOut2 = value; } }
        public bool DgOut3 { get { return _DgOut3; } set { _DgOut3 = value; } }
        public bool DgOut4 { get { return _DgOut4; } set { _DgOut4 = value; } }
        public bool DgOut5 { get { return _DgOut5; } set { _DgOut5 = value; } }
        public bool DgOut6 { get { return _DgOut6; } set { _DgOut6 = value; } }
        public bool DgOut7 { get { return _DgOut7; } set { _DgOut7 = value; } }
        public bool DgOut8 { get { return _DgOut8; } set { _DgOut8 = value; } }

        /// <summary>
        /// Количество ошибок в приеме/передаче данных
        /// </summary>
        public long WrongPackets = 0;
        /// <summary>
        /// Количество потерянных пакетов
        /// </summary>
        public long LastPackets = 0;
        /// <summary>
        /// Показывает, что на данный момент есть незаконченные команды. 
        /// </summary>
        public static AutoResetEvent InWork = new AutoResetEvent(false);

        /// <summary>
        /// Делегат, для события получения данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DataReceivedEventHandler(object sender, MsgArrivedEventArgs e);

        /// <summary>
        /// Событие, которое возникает при получении данных от модуля 
        /// </summary>
        public event DataReceivedEventHandler DataReceived;

        /// <summary>
        /// Делегат, для события сообщающего о неудачной 
        /// попытке получения данных.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DataReceiveFailedEventHandler(object sender, MsgArrivedEventArgs e);

        /// <summary>
        /// Событие, которое возникает при получении данных от модуля 
        /// </summary>
        public event DataReceiveFailedEventHandler DataReceiveFailed;

        /// <summary>
        /// Тип устройства
        /// </summary>
        public string DeviceType { get { return GetDeviceType(); } }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="inKpNumber"></param>
        /// <param name="dgKpNumber"></param>
        /// <param name="name"></param>
        public ModulNevod(int inKpNumber, int dgKpNumber, string name = "")
        {
            _inKpNum = inKpNumber;
            _dgKpNum = dgKpNumber;

            ModulName = name;
        }

        /// <summary>
        /// Запрашивает данные дискретных входов КП и обновляет 
        /// переменные DgIn1-8 актуальными значениями
        /// </summary>
        public string DigitalVariablesRefresh()
        {
            // Указать, что запущенна новая команда
            InWork.Reset();

            try
            {
                string command = "$" + AddZero(Convert.ToString(dgKpNum)) + "6\r";
                byte[] query = Encoding.ASCII.GetBytes(command);
                Communicators.comPort.Write(query, 0, query.Length);

                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    "TX: " + ConvertByteMass(query, query.Length));
                Thread.Sleep(900);

                // читаем ответ
                byte[] buffer = new byte[256];
                Communicators.comPort.Read(buffer, 0, 256);

                // Выводим ответ 
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                     "RX: " + ConvertByteMass(buffer, 7));

                // дискретные выходы
                _DgOut1 = Convert.ToBoolean(buffer[1] & 254);
                _DgOut2 = Convert.ToBoolean(buffer[1] & 64);
                _DgOut3 = Convert.ToBoolean(buffer[1] & 32);
                _DgOut4 = Convert.ToBoolean(buffer[1] & 16);
                _DgOut5 = Convert.ToBoolean(buffer[1] & 8);
                _DgOut6 = Convert.ToBoolean(buffer[1] & 4);
                _DgOut7 = Convert.ToBoolean(buffer[1] & 2);
                _DgOut8 = Convert.ToBoolean(buffer[1] & 1);

                // дискретные входы
                _DgIn1 = Convert.ToBoolean(buffer[2] & 254);
                _DgIn2 = Convert.ToBoolean(buffer[2] & 64);
                _DgIn3 = Convert.ToBoolean(buffer[2] & 32);
                _DgIn4 = Convert.ToBoolean(buffer[2] & 16);
                _DgIn5 = Convert.ToBoolean(buffer[2] & 8);
                _DgIn6 = Convert.ToBoolean(buffer[2] & 4);
                _DgIn7 = Convert.ToBoolean(buffer[2] & 2);
                _DgIn8 = Convert.ToBoolean(buffer[2] & 1);

                // Если есть подписчики, сообщить, что данные обновились
                if (DataReceived != null)
                    DataReceived(this, new MsgArrivedEventArgs("DigitalVariablesRefresh"));
            }
            catch (Exception e)
            {
                // Указать, что команда закончена.
                InWork.Set();

                return e.Message;
            };

            // Указать, что команда закончена.
            InWork.Set();

            return null;
        }

        /// <summary>
        /// Запрашивает данные аналоговых входов КП и обновляет 
        /// переменные AnIn1-4 актуальными значениями
        /// </summary>
        public string AnalogInputsRefresh()
        {
            // Указать, что запущенна новая команда
            InWork.Reset();

            try
            {
                // отправляем команду на устройство
                Communicators.comPort.WriteLine("#" + AddZero(Convert.ToString(inKpNum)) + "\r");

                System.Threading.Thread.Sleep(900);

                // читаем ответ
                byte[] buffer = new byte[255];
                int count = Communicators.comPort.Read(buffer, 0, 255);

                // Выводим ответ 
                string data = "RX: " + Encoding.UTF8.GetString(buffer, 0, count);
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer, data);

                // первое измерение
                _AnIn1 = Convert.ToDouble(data.Substring(1, 7).Replace('.', ','));

                // второе измерение
                _AnIn2 = Convert.ToDouble(data.Substring(8, 7).Replace('.', ','));

                // третье измерение
                _AnIn3 = Convert.ToDouble(data.Substring(15, 7).Replace('.', ','));

                // четвертое измерение
                _AnIn4 = Convert.ToDouble(data.Substring(22, 7).Replace('.', ','));

                // значение температуры
                _temperature = Convert.ToDouble(data.Substring(29, 7).Replace('.', ','));

                // Если есть подписчики, сообщить, что данные обновились
                if (DataReceived != null)
                    DataReceived(this, new MsgArrivedEventArgs("AnalogInputsRefresh"));
            }
            catch (Exception e)
            {
                // Указать, что команда закончена.
                InWork.Set();

                return e.Message;
            }

            // Указать, что команда закончена.
            InWork.Set();

            return null;
        }

        /// <summary>
        /// Проверяет соответствие типа устройства КП
        /// </summary>
        /// <returns>
        /// сообщение о соответствии/несоответствии или мсообщение об ошбке
        /// </returns>
        public string GetDeviceType()
        {
            // Указать, что запущенна новая команда
            InWork.Reset();

            string result = Convert.ToString(inKpNum) + "-аналоговый, " +
                Convert.ToString(dgKpNum) + "-дискретный...";

            Communicators.comPort.NewLine = "\r";
            try
            {
                string buffer;

                Communicators.comPort.WriteLine("$" + inKpNum + "M\r");
                buffer = Communicators.comPort.ReadLine();
                if (buffer.Substring(3, 4) != "4017") result = "Неверно указан тип КП" + Convert.ToString(inKpNum);


                Communicators.comPort.WriteLine("$" + dgKpNum + "M\r");
                buffer = Communicators.comPort.ReadLine();
                if (buffer.Substring(3, 4) != "4050") result += " Неверно указан тип КП" + Convert.ToString(dgKpNum);

                // Указать, что команда закончена.
                InWork.Set();

                return result;
            }
            catch (Exception e)
            {
                // Указать, что команда закончена.
                InWork.Set();

                return e.Message;
            };


        }

        /// <summary>
        /// Преобразует полубайты в символы
        /// </summary>
        /// <param name="B">значение полубайта</param>
        /// <returns>распознанный символ</returns>
        public char ConvertToSymbol(byte B)
        {
            char RESULT;

            switch (B)
            {
                case 0:
                    RESULT = '+';
                    break;
                case 1:
                    RESULT = ',';
                    break;
                case 2:
                    RESULT = '-';
                    break;
                case 3:
                    RESULT = '.';
                    break;
                case 4:
                    RESULT = '/';
                    break;
                case 5:
                    RESULT = '0';
                    break;
                case 6:
                    RESULT = '1';
                    break;
                case 7:
                    RESULT = '2';
                    break;
                case 8:
                    RESULT = '3';
                    break;
                case 9:
                    RESULT = '4';
                    break;
                case 10:
                    RESULT = '5';
                    break;
                case 11:
                    RESULT = '6';
                    break;
                case 12:
                    RESULT = '7';
                    break;
                case 13:
                    RESULT = '8';
                    break;
                case 14:
                    RESULT = '9';
                    break;
                case 15:
                    RESULT = ':';
                    break;
                default:
                    RESULT = ' ';
                    break;
            }
            return RESULT;
        }

        /// <summary>
        /// Телеуправление. Управляет напряжением выходов 
        /// </summary>
        /// <param name="number">номер выхода: 0-7</param>
        /// <param name="value">действие: включить/выключить</param>
        public void Switch(int number)
        {
            // Указать, что запущенна новая команда
            InWork.Reset();

            try
            {
                byte[] buf = new byte[256];

                // Чтобы задать значение для нулевого бита нужно указать 8
                // иначе комманда будет воспринята как общая "00"
                if (number == 0) number = 8;

                // #1013FF - включение DIO4 модуля 
                string s = "#" + AddZero(Convert.ToString(_dgKpNum)) +
                    AddZero(Convert.ToString(number)) + "01" + "\r";

                byte[] queryOn = Encoding.ASCII.GetBytes(s);
                Communicators.comPort.Write(queryOn, 0, queryOn.Length);
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                        ConvertByteMass(queryOn, queryOn.Length));

                // Запись в лог-файл
                Loger.WriteToFile("Модуль №" + Convert.ToString(_dgKpNum) +
                     ". Подать напряжение на выход №" + number);

                // Ждем пока появится сгнал
                // отработки цепей СДТУ "У"
                Thread.Sleep(700);
                Communicators.comPort.Read(buf, 0, 255);

                // Обновляем значения переменных
                AllVariablesRefresh(true);

                // Задержка перед выключением агригата
                Thread.Sleep(TimeoutTU);

                // #101300 - выключение DIO4 модуля 
                byte[] queryOff = Encoding.ASCII.GetBytes("#" + AddZero(Convert.ToString(_dgKpNum)) + "0000\r");
                Communicators.comPort.Write(queryOff, 0, queryOff.Length);
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    "RX: " + ConvertByteMass(queryOff, queryOff.Length));

                // Запись в лог-файл
                Loger.WriteToFile("Модуль №" + Convert.ToString(_dgKpNum) +
                    ". Снять напряжение с выхода №" + +number);

                Thread.Sleep(700);
                Communicators.comPort.Read(buf, 0, 255);
            }
            catch (Exception exp)
            {
                try
                {
                    Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                        "Ошибка телеуправления: " + exp.Message);

                    // Обновляем значения переменных
                    AllVariablesRefresh(true);

                    // Проверяем, не остался ли какой-то агрегат включенным
                    int numberOut = -1;
                    if (_DgOut1 == true) numberOut = 0;
                    if (_DgOut2 == true) numberOut = 1;
                    if (_DgOut3 == true) numberOut = 2;
                    if (_DgOut4 == true) numberOut = 3;
                    if (_DgOut5 == true) numberOut = 4;
                    if (_DgOut6 == true) numberOut = 5;
                    if (_DgOut7 == true) numberOut = 6;
                    if (_DgOut8 == true) numberOut = 7;
                    // Если необходимо повторно пытаемя выключить агрегат
                    if (numberOut != -1)
                    {
                        Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                            "Повторная попытка отключения агрегата");
                        NevodCommand offCmd = new NevodCommand("off", _dgKpNum, numberOut);
                        DataServer.hendCommmands.Add(offCmd);
                        DataServer.WaitHandler.Set();
                    }
                }
                catch
                {

                }
            }

            // Обновляем значения переменных
            Thread.Sleep(TimeoutAfterTU);
            AllVariablesRefresh(true);

            // Указать, что команда закончена.
            InWork.Set();
        }

        /// <summary>
        /// Добавляет нуль впереди, если val<10
        /// </summary>
        /// <param name="val">старое значение</param>
        /// <returns>новое значение</returns>
        public string AddZero(string val)
        {
            if (val.Length == 1) val = "0" + val;
            return val;
        }

        /// <summary>
        /// Потоковый режи. Обнавление значений всех регистров за одну посылку.
        /// </summary>
        /// <param name="dependentCommand">Истина если команда зависимая
        /// (была вызвана другой команда модуля, а не как самостоятельная)</param>
        /// <returns></returns>
        public string AllVariablesRefresh(bool dependentCommand = false)
        {
            // Указать, что запущенна новая команда
            InWork.Reset();

            try
            {
                // Отправляем команду на устройство
                string command = "|" + AddZero(Convert.ToString(dgKpNum)) + "\r";
                //Communicators.comPort.WriteLine(command);

                byte[] query = Encoding.ASCII.GetBytes(command);
                Communicators.comPort.Write(query, 0, query.Length);

                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    "TX: " + ConvertByteMass(query, query.Length));

                // Читаем ответ
                byte[] buffer = new byte[22];
                int receiveBytes = 0;

                while (receiveBytes != 22)
                {
                    buffer[receiveBytes] = (byte)Communicators.comPort.ReadByte();
                    receiveBytes++;
                }

                // Выводим ответ 
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    "RX: " + ConvertByteMass(buffer, 22));

                // Преобразуем байты из полубайты
                int k = 0;
                byte[] B = new byte[40];
                for (int i = 0; i < 20; i++)
                {
                    B[k] = Convert.ToByte(buffer[i] >> 4);
                    B[k + 1] = Convert.ToByte(buffer[i] & 15);
                    k += 2;
                }

                // Первое измерение
                string AnIn1 = "";
                for (int l = 0; l < 7; l++)
                {
                    AnIn1 += ConvertToSymbol(B[l]);
                }
                _AnIn1 = Convert.ToDouble(AnIn1.Replace('.', ','));

                // Второе измерение
                string AnIn2 = "";

                for (int l = 8; l < 15; l++)
                {
                    AnIn2 += ConvertToSymbol(B[l]);
                }
                _AnIn2 = Convert.ToDouble(AnIn2.Replace('.', ','));

                // Третье измерение
                string AnIn3 = "";

                for (int l = 16; l < 23; l++)
                {
                    AnIn3 += ConvertToSymbol(B[l]);
                }
                _AnIn3 = Convert.ToDouble(AnIn3.Replace('.', ','));

                // Четвертое измерение
                string AnIn4 = "";

                for (int l = 24; l < 31; l++)
                {
                    AnIn4 += ConvertToSymbol(B[l]);
                }
                _AnIn4 = Convert.ToDouble(AnIn4.Replace('.', ','));

                // Значение температуры
                string temperature = "";

                for (int l = 32; l < 39; l++)
                {
                    temperature += ConvertToSymbol(B[l]);
                }
                _temperature = Convert.ToDouble(temperature.Replace('.', ','));

                // Дискретные выходы
                _DgOut1 = Convert.ToBoolean(buffer[20] & 1);
                _DgOut2 = Convert.ToBoolean(buffer[20] & 2);
                _DgOut3 = Convert.ToBoolean(buffer[20] & 4);
                _DgOut4 = Convert.ToBoolean(buffer[20] & 8);
                _DgOut5 = Convert.ToBoolean(buffer[20] & 16);
                _DgOut6 = Convert.ToBoolean(buffer[20] & 32);
                _DgOut7 = Convert.ToBoolean(buffer[20] & 64);
                _DgOut8 = Convert.ToBoolean(buffer[20] & 128);

                // Дискретные входы
                _DgIn1 = Convert.ToBoolean(buffer[21] & 1);
                _DgIn2 = Convert.ToBoolean(buffer[21] & 2);
                _DgIn3 = Convert.ToBoolean(buffer[21] & 4);
                _DgIn4 = Convert.ToBoolean(buffer[21] & 8);
                _DgIn5 = Convert.ToBoolean(buffer[21] & 16);
                _DgIn6 = Convert.ToBoolean(buffer[21] & 32);
                _DgIn7 = Convert.ToBoolean(buffer[21] & 64);
                _DgIn8 = Convert.ToBoolean(buffer[21] & 128);

                // Если есть подписчики, сообщить, что данные обновились
                if (DataReceived != null)
                    DataReceived(this, new MsgArrivedEventArgs("AllVariablesRefresh"));
            }
            catch (Exception exp)
            {
                // Очистить входной буферъ COM-порта
                Communicators.ClearPortBuffer(this);

                if (DataReceiveFailed != null)
                    DataReceiveFailed(this, new MsgArrivedEventArgs("Failed"));

                // Указать, что команда закончена.
                if (!dependentCommand) InWork.Set();

                return exp.Message;
            }

            // Указать, что команда закончена.
            if (!dependentCommand) InWork.Set();
            return null;
        }

        /// <summary>
        /// Устанавливает состояние бита выхода
        /// </summary>
        /// <param name="number"></param>
        /// <param name="state"></param>
        public void SetState(int number, int state)
        {
            // Указать, что запущенна новая команда
            InWork.Reset();

            try
            {
                // Чтобы задать значение для нулевого бита нужно указать 8
                // иначе комманда будет воспринята как общая "00"
                if (number == 0) number = 8;

                // #1013FF - включение DIO4 модуля 
                string s = "#" + AddZero(Convert.ToString(_dgKpNum)) + AddZero(Convert.ToString(number)) +
                    AddZero(Convert.ToString(state)) + "\r";

                byte[] query = Encoding.ASCII.GetBytes(s);
                Communicators.comPort.Write(query, 0, query.Length);
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                        "TX: " + ConvertByteMass(query, query.Length));


                if (state == 1)
                {
                    Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                        "Модуль №" + Convert.ToString(_dgKpNum) +
                        ". Подать напряжение на выход №" + Convert.ToString(number));

                    // Запись в лог-файл
                    Loger.WriteToFile("Модуль №" + Convert.ToString(_dgKpNum) +
                        ". Подать напряжение на выход №" + Convert.ToString(number));
                }
                else
                {
                    Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                        "Модуль №" + Convert.ToString(_dgKpNum) +
                        ". Снять напряжение с выхода №" + Convert.ToString(number));

                    // Запись в лог-файл
                    Loger.WriteToFile("Модуль №" + Convert.ToString(_dgKpNum) +
                        ". Снять напряжение с выхода №" + Convert.ToString(number));
                }

                // Подождать ответ о выполнении команды
                byte[] buffer = new byte[2];
                int receiveBytes = 0;
                while (receiveBytes != 2)
                {
                    buffer[receiveBytes] = (byte)Communicators.comPort.ReadByte();
                    receiveBytes++;
                }
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    "RX: " + ConvertByteMass(buffer, buffer.Length));
            }
            catch (Exception exp)
            {
                // Сообщить об ошибке
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    exp.Message);
                // Очистить входной буферъ COM-порта
                Communicators.ClearPortBuffer(this);
            }

            // Обновляем значения переменных
            Thread.Sleep(TimeoutAfterTU);
            AllVariablesRefresh(true);

            // Указать, что команда закончена.
            InWork.Set();
        }

        /// <summary>
        /// Преобразовывает байтовый массив в строку с ASCI кодами
        /// </summary>
        /// <param name="b"> Массив байтов </param>
        /// <param name="count"> Длинна массива </param>
        /// <returns> Строка с преобразованным массиивом </returns>
        public string ConvertByteMass(byte[] b, int count)
        {
            string hexOutput = "";
            for (int i = 0; i < count; i++)
                hexOutput += AddZero(Convert.ToString(b[i], 16).ToUpperInvariant()) + " ";
            return hexOutput.Substring(0, hexOutput.Length - 1);
        }
    }

    /// <summary>
    /// Параметр события модуля Невод 
    /// </summary>
    public class MsgArrivedEventArgs : EventArgs
    {
        readonly string message;

        public MsgArrivedEventArgs(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get { return message; }
        }
    }
}
