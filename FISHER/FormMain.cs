using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace FISHER
{
    public partial class FormMain : Form
    {
        /// <summary>
        ///  Старый индекс. Необходима для отображения состояния
        ///  выбранного в cBoxModuls модуля "Невод".
        /// </summary>
        int OldIndex = 0;

        /// <summary>
        /// Остановить запись логов в textBox
        /// </summary>
        bool pauseLoger = false;

        /// <summary>
        /// Серверный поток сбора данных
        /// </summary>
        Thread getDataThread;

        /// <summary>
        /// Поток TCP-сервера
        /// </summary>
        Thread tcpServerThread;

        /// <summary>
        /// Конструктор
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            // Подписываем фенкцию формы LogerDataReceived 
            // на событе модуля Loger - DataReceived
            Loger.DataReceived += new Loger.DataReceivedEventHandler(this.LogerDataReceived);

            Loger.WriteToFile("Запущенна оконная форма приложения...");

            // Загрузить список модулей и параметры COM-порта из конфигурационного файла
            loadConfiguration();

            // Поток для сбора данных и управления модулями Невод
            getDataThread = new Thread(delegate()
            {
                DataServer.Run();
            });
            getDataThread.IsBackground = true;
            getDataThread.Name = "Get data thread";
            getDataThread.Start();

            // Поток для подключеия клиентов по TCP
            tcpServerThread = new Thread(delegate()
            {
                // Создадим новый сервер
                new TcpServer();
            });
            tcpServerThread.IsBackground = true;
            tcpServerThread.Name = "TCP server thread";
            tcpServerThread.Start();

            if (cBoxModuls.Items.Count > 0) cBoxModuls.SelectedIndex = 0;
        }
        /// <summary>
        /// Установить соединение 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Открыть соединение
                Communicators.CommunicationOpen();
                buttonConnect.Enabled = false;
                enebleControls = true;
                writeLog(Convert.ToString(Communicators.comPort.PortName) + " открыт...");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, Text);
            }

        }
        /// <summary>
        /// Начать работу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Обнулить счетчики ошибок
            Communicators.WrongPackets = 0;
            Communicators.LastPackets = 0;
            foreach (ModulNevod modul in DataServer.modulNevod)
            {
                modul.WrongPackets = 0;
                modul.LastPackets = 0;
            }

            // Запустить поток опроса 
            DataServer.Running = true;
            DataServer.WaitHandler.Set();

            // Управление доступностью кнопок
            buttonStop.Enabled = true;
            buttonStart.Enabled = false;
            buttonСomSetings.Enabled = false;

            // Сделать доступной панель управления
            tabelPanel1.Enabled = true;
        }
        /// <summary>
        /// Закрывает COM-порт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            DataServer.Running = false;
            // Управление доступностью кнопок
            buttonStop.Enabled = false;
            buttonStart.Enabled = true;
            buttonСomSetings.Enabled = true;
            // Заблокировать панель управления
            tabelPanel1.Enabled = false;

            // Не допускаем повторный вывод отчета по ошибкам
            if (((Button)sender).Name != "buttonStop") return;

            Thread ErrorMessage = new Thread(delegate()
            {
                // Ждем пока закончиться выполнение всех комманд. 
                // Перед тем как выводить отчет.
                ModulNevod.InWork.WaitOne(2100);
                Thread.Sleep(150);

                // Вывести отчет по ошибкам
                foreach (ModulNevod modul in DataServer.modulNevod)
                {
                    // Вывести отчет по ошибкам
                    Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.Log,
                        "[ " + modul.ModulName + " ]: " +
                        "Потеряно пакетов: " + modul.LastPackets.ToString() +
                        ", Пакетов с ошибками: " + modul.WrongPackets.ToString());
                }
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.Log,
                    "Всего потеряно пакетов: " + Convert.ToString(Communicators.LastPackets) +
                    ", Всего пакетов с ошибками: " + Convert.ToString(Communicators.WrongPackets));

                // Обесцветить дерево объектов
                treeViewConfig.Nodes[0].ForeColor = Color.Black;
                foreach (TreeNode node in treeViewConfig.Nodes[0].Nodes)
                {
                    node.ForeColor = Color.Black;
                }
            });
            ErrorMessage.IsBackground = true;
            ErrorMessage.Name = "Write errors report in last line in logs thread";
            ErrorMessage.Start();

        }
        /// <summary>
        /// Отправить команду вручную
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            // Добавить новую команду в очередь
            NevodCommand tempCmd = new NevodCommand();
            tempCmd.Cmd = textBoxCMD.Text + "\r";
            tempCmd.NumberKP = -1;
            DataServer.hendCommmands.Add(tempCmd);
            // Возобновить поток опроса, если необходимо
            DataServer.WaitHandler.Set();
        }
        /// <summary>
        /// Разорвать соединение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (Communicators.comPort.IsOpen)
            {
                buttonStop_Click(sender, e);
                Communicators.comPort.Close();
                writeLog(Communicators.comPort.PortName + " закрыт...");
            }
            buttonConnect.Enabled = true;
            enebleControls = false;
        }
        /// <summary>
        /// Корректное завершение работы приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Остановить опрос
            DataServer.Running = false;
            // Подать сигнал о завершении работы всех потоков
            Loger.closeAllThreads = true;
            DataServer.WaitHandler.Set();
            // Дать время потокам для завершения работы
            Thread.Sleep(1000);
            // Принудительно завершить поток TcpServer`а при необходимости
            if (tcpServerThread.IsAlive) tcpServerThread.Abort();
            if (getDataThread.IsAlive) getDataThread.Join();
            // Закрыть COM-порт
            if (Communicators.comPort != null)
                if (Communicators.comPort.IsOpen) Communicators.comPort.Close();
            Thread.Sleep(200);
            // Пишем в лог-файл сообщение о завершении работы
            Loger.WriteToFile("Закрыта оконная форма приложения...");
        }
        /// <summary>
        /// Управляет доступностью котролов
        /// </summary>
        private bool enebleControls
        {
            set
            {
                buttonStart.Enabled = value;
                buttonSend.Enabled = value;
                buttonStop.Enabled = value;
                buttonDisconnect.Enabled = value;
                textBoxCMD.Enabled = value;
            }
        }
        /// <summary>
        /// Запись сообщений о работе приложения
        /// </summary>
        /// <param name="msg">сообщение</param>
        public void writeLog(string msg)
        {
            // Приостановить выведение логов
            if (pauseLoger)
            {
                if (labelParametrs.InvokeRequired)
                {
                    labelParametrs.BeginInvoke((Action)delegate
                    {
                        labelParametrs.Text = "Ошибки: " + Communicators.LastPackets.ToString() + "; " +
                            Communicators.WrongPackets.ToString() + ".";
                    });
                }
                else
                {
                    labelParametrs.Text = "Ошибки: " + Communicators.LastPackets.ToString() + "; " +
                         Communicators.WrongPackets.ToString() + ".";
                }
                return;
            }

            DateTime dt = DateTime.Now;
            string time = " [" + dt.ToLongTimeString() + "." + dt.Millisecond.ToString() + "]";

            if (textBoxLogs.InvokeRequired)
            {
                textBoxLogs.BeginInvoke((Action)delegate
                {
                    if (textBoxLogs.Lines.Length > 200) textBoxLogs.Clear();
                    textBoxLogs.AppendText(msg + time + Environment.NewLine);

                    labelParametrs.Text = "Ошибки: " + Communicators.LastPackets.ToString() + "; " +
                        Communicators.WrongPackets.ToString() + ".";
                });
            }
            else
            {
                if (textBoxLogs.Lines.Length > 100) textBoxLogs.Clear();
                textBoxLogs.AppendText(msg + time + Environment.NewLine);

                labelParametrs.Text = "Ошибки: " + Communicators.LastPackets.ToString() + "; " +
                     Communicators.WrongPackets.ToString() + ".";
            }
        }
        /// <summary>
        /// Загрузка конфигурации системы из xml файла 
        /// </summary>
        private void loadConfiguration()
        {
            string fileName = "server_config.xml";
            if (!File.Exists(fileName))
            {
                MessageBox.Show("Файл конфигурации не найден!", "Ошибка файла конфигурации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                treeViewConfig.BeginUpdate();
                // Загружаенм данные из файла
                XDocument xdoc = XDocument.Load(fileName);

                // Параметры соединения
                XElement connection = xdoc.Element("root").Element("connection");
                // Устанавливаем порт 
                TcpServer.Port = Convert.ToInt32(connection.Attribute("port").Value);

                // Параметры COM-порта, инициализация порта
                ParseSerialPortConfig(xdoc.Element("root").Element("serial_port"));

                // Список пользователей с правами на телеуправление
                string users = xdoc.Element("root").Element("users").Attribute("names").Value;
                if (users != null)
                {
                    string[] names = users.Split(';');
                    foreach(string name in names) TcpServer.ListOfUsers.Add(name);
                }

                // Список и параметры модулей
                XElement configuration = xdoc.Element("root").Element("configuration");

                foreach (XElement el in configuration.Elements())
                {
                    string name = el.Attribute("name").Value + " (" +
                        el.Attribute("dg_kp").Value + ", " + el.Attribute("an_kp").Value + ")";
                    treeViewConfig.Nodes[0].Nodes.Add(name);
                    cBoxModuls.Items.Add(name);

                    int dg_kp = Convert.ToInt32(el.Attribute("dg_kp").Value); ;
                    int an_kp = Convert.ToInt32(el.Attribute("an_kp").Value); ;

                    // Создание модулей
                    DataServer.modulNevod.Add(new ModulNevod(an_kp, dg_kp, el.Attribute("name").Value));

                    // Подписываем функцию отправки сообщений TCP-клиентам SendDataClient
                    // на событие обновление данных на модуле
                    DataServer.modulNevod[DataServer.modulNevod.Count - 1].DataReceived +=
                        new ModulNevod.DataReceivedEventHandler(TcpServer.SendDataClient);

                    // Подписываем функцию слежения за состоянием связи
                    // с КП на обновления данных модуля
                    DataServer.modulNevod[DataServer.modulNevod.Count - 1].DataReceived +=
                        new ModulNevod.DataReceivedEventHandler(MonitorConnectonState);

                    // Подписываем функцию слежения за состоянием связи
                    // с КП на событие ошибки обновления данных модуля
                    DataServer.modulNevod[DataServer.modulNevod.Count - 1].DataReceiveFailed +=
                        new ModulNevod.DataReceiveFailedEventHandler(MonitorConnectonState);
                }

                treeViewConfig.EndUpdate();
            }
            catch (XmlException exp)
            {
                MessageBox.Show(exp.Message, "Ошибка файла конфигурации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Сворачивать программу в трей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                taskBarIco.Visible = true;
                Hide();
            }
        }
        /// <summary>
        /// Восстановление из трея
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void taskBarIco_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                taskBarIco.Visible = false;
            }
        }
        /// <summary>
        /// Вызов окна для конфигурации COM-порта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonСomSetings_Click(object sender, EventArgs e)
        {
            Form f = new FormCOMPort();
            f.Owner = this;
            f.ShowDialog();

            if (Communicators.comPort != null)
            {
                if (!Communicators.comPort.IsOpen)
                {
                    buttonDisconnect_Click(this, new EventArgs());
                }
            }
        }
        /// <summary>
        /// Вызывается при обновлении данных модуля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShowData(object sender, MsgArrivedEventArgs e)
        {
            ModulNevod modul = sender as ModulNevod;

            if (this.InvokeRequired)
            {
                this.BeginInvoke((Action)delegate()
                {
                    RefreshTable(modul);
                });
            }
            else
            {
                RefreshTable(modul);
            }
        }
        /// <summary>
        /// Обновляет значения переменных в таблице  
        /// </summary>
        /// <param name="modul"></param>
        void RefreshTable(ModulNevod modul)
        {
            // (начало) Дискретные входы
            // 1
            if (modul.DgIn1) dIn1.BackColor = Color.White;
            else dIn1.BackColor = Color.Black;
            // 2
            if (modul.DgIn2) dIn2.BackColor = Color.White;
            else dIn2.BackColor = Color.Black;
            // 3
            if (modul.DgIn3) dIn3.BackColor = Color.White;
            else dIn3.BackColor = Color.Black;
            // 4
            if (modul.DgIn4) dIn4.BackColor = Color.White;
            else dIn4.BackColor = Color.Black;
            // 5
            if (modul.DgIn5) dIn5.BackColor = Color.White;
            else dIn5.BackColor = Color.Black;
            // 6
            if (modul.DgIn6) dIn6.BackColor = Color.White;
            else dIn6.BackColor = Color.Black;
            // 7
            if (modul.DgIn7) dIn7.BackColor = Color.White;
            else dIn7.BackColor = Color.Black;
            // 8
            if (modul.DgIn8) dIn8.BackColor = Color.White;
            else dIn8.BackColor = Color.Black;
            // (конец) Дискретные входы

            // (начало) Дискретный выход
            // 1
            if (modul.DgOut1) dOut1.BackColor = Color.White;
            else dOut1.BackColor = Color.Black;
            // 2
            if (modul.DgOut2) dOut2.BackColor = Color.White;
            else dOut2.BackColor = Color.Black;
            // 3
            if (modul.DgOut3) dOut3.BackColor = Color.White;
            else dOut3.BackColor = Color.Black;
            // 4
            if (modul.DgOut4) dOut4.BackColor = Color.White;
            else dOut4.BackColor = Color.Black;
            // 5
            if (modul.DgOut5) dOut5.BackColor = Color.White;
            else dOut5.BackColor = Color.Black;
            // 6
            if (modul.DgOut6) dOut6.BackColor = Color.White;
            else dOut6.BackColor = Color.Black;
            // 7
            if (modul.DgOut7) dOut7.BackColor = Color.White;
            else dOut7.BackColor = Color.Black;
            // 8
            if (modul.DgOut8) dOut8.BackColor = Color.White;
            else dOut8.BackColor = Color.Black;
            // (конец) Дискретный выход

            // (начало) Аналоговый вход
            // 1
            aIn1.Text = ConvertValue(modul.AnIn1);
            // 2
            aIn2.Text = ConvertValue(modul.AnIn2);
            // 3
            aIn3.Text = ConvertValue(modul.AnIn3);
            // 4
            aIn4.Text = ConvertValue(modul.AnIn4);
            // 5
            aIn5.Text = ConvertValue(modul.Temperature);
            // (конец) Аналоговый вход
        }
        /// <summary>
        /// Округление переменных в звависимости от разрядов
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string ConvertValue(double value)
        {
            if (value <= -100)
                return Convert.ToString(Math.Round(value, 1));
            if (value >= 100)
                return Convert.ToString(Math.Round(value, 1));
            if ((value < 10) & (value > -10))
                return Convert.ToString(Math.Round(value, 3));

            return Convert.ToString(Math.Round(value, 2));
        }
        /// <summary>
        /// Запустк операции ТУ (включение)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetOne_Click(object sender, EventArgs e)
        {
            int index = cBoxModuls.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Выберите модуль из списка!", Text);
                return;
            }

            int numberKp = DataServer.modulNevod[index].dgKpNum;
            Button button = sender as Button;
            int numberOut = Convert.ToInt32(button.Tag);
            DataServer.hendCommmands.Add(new NevodCommand("on", numberKp, numberOut));

            // Запись в лог-файл
            Loger.WriteToFile("Сервер запустил операцию ТУ. Подать напряжение на КП №" +
                Convert.ToString(numberKp) + " выход №" + Convert.ToString(numberOut));
        }
        /// <summary>
        /// Запустк операции ТУ (выключение)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetZero_Click(object sender, EventArgs e)
        {
            int index = cBoxModuls.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Выберите модуль из списка!", Text);
                return;
            }

            int numberKp = DataServer.modulNevod[index].dgKpNum;
            Button button = sender as Button;
            int numberOut = Convert.ToInt32(button.Tag);
            DataServer.hendCommmands.Add(new NevodCommand("off", numberKp, numberOut));

            // Запись в лог-файл
            Loger.WriteToFile("Сервер запустил операцию ТУ. Снять напряжение c КП №" +
                Convert.ToString(numberKp) + " выход №" + Convert.ToString(numberOut));

        }
        /// <summary>
        /// Запуск операции ТУ (кратковременно подать напряжение на выход)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            int index = cBoxModuls.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Выберите модуль из списка!", Text);
                return;
            }

            int numberKp = DataServer.modulNevod[index].dgKpNum;
            Button button = sender as Button;
            int numberOut = Convert.ToInt32(button.Tag);
            DataServer.hendCommmands.Add(new NevodCommand("switch", numberKp, numberOut));

            // Запись в лог-файл
            Loger.WriteToFile("Сервер запустил операцию кратковременной подачи напряжения.  КП №" +
                Convert.ToString(numberKp) + " выход №" + Convert.ToString(numberOut));
        }
        /// <summary>
        /// Выбор модуля для слежения и управления с формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cBoxModuls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cBoxModuls.SelectedIndex == -1) || (OldIndex == -1))
                return;

            // Отписаться от предыдущего модуля
            DataServer.modulNevod[OldIndex].DataReceived -=
                    new ModulNevod.DataReceivedEventHandler(ShowData);
            // Подписаться на новый модуль
            DataServer.modulNevod[cBoxModuls.SelectedIndex].DataReceived +=
                    new ModulNevod.DataReceivedEventHandler(ShowData);
            // Запомнить номер подписанного
            OldIndex = cBoxModuls.SelectedIndex;
        }
        /// <summary>
        /// Приостанавливает вывод логов в textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPauseLoger_Click(object sender, EventArgs e)
        {
            if (pauseLoger)
            {
                pauseLoger = false;
                buttonPauseLoger.Text = "Остановить вывод";
                buttonPauseLoger.ForeColor = Color.Black;
            }
            else
            {
                pauseLoger = true;
                buttonPauseLoger.Text = "Запустить вывод";
                buttonPauseLoger.ForeColor = Color.Red;
            }
        }
        /// <summary>
        /// Вызывается при появлении новых логов
        /// </summary>
        /// <param name="e"></param>
        private void LogerDataReceived(LogerEventArgs e)
        {
            writeLog(e.Message.tMessage);
        }
        /// <summary>
        /// Кнопка очистки textBox с логами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxLogs.Clear();
        }
        /// <summary>
        /// Загружает из файла данные о COM-порте
        /// </summary>
        /// <param name="serial_port">Элемент с параметрами COM-порта</param>
        private void ParseSerialPortConfig(XElement serial_port)
        {
            // Сообщения для пояснения ошибок
            const string errorTitle = "Ошибка файла конфигурации! Настройки COM-порта, параметр ";
            const string useHelp = " Смотрите справку по правилам заполнения файла конфигурации.";

            string port_name = null;
            try
            {
                port_name = serial_port.Attribute("port_name").Value.ToUpper();
            }
            catch (Exception)
            {
                Loger.WriteToFile(errorTitle + "имени порта ('port_name')." + useHelp);
            }

            int baudRate = 19200;
            try
            {
                baudRate = Convert.ToInt32(serial_port.Attribute("baud_rate").Value);
            }
            catch (Exception)
            {
                Loger.WriteToFile(errorTitle + "скорости передачи данных ('baud_rate')." + useHelp);
            }

            Parity parity = Parity.None;
            try
            {
                switch (serial_port.Attribute("parity").Value.ToLower())
                {
                    case "none":
                        parity = Parity.None;
                        break;
                    case "even":
                        parity = Parity.Even;
                        break;
                    case "odd":
                        parity = Parity.Odd;
                        break;
                    case "mark":
                        parity = Parity.Mark;
                        break;
                    case "space":
                        parity = Parity.Space;
                        break;
                    case "нет":
                        parity = Parity.None;
                        break;
                    case "чет":
                        parity = Parity.Even;
                        break;
                    case "нечет":
                        parity = Parity.Odd;
                        break;
                    case "марк":
                        parity = Parity.Mark;
                        break;
                    case "пробел":
                        parity = Parity.Space;
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                Loger.WriteToFile(errorTitle + "четности ('parity')." + useHelp);
            }

            StopBits stopBits = StopBits.One;
            try
            {
                switch (serial_port.Attribute("stop_bits").Value.ToLower())
                {
                    case "1":
                        stopBits = StopBits.One;
                        break;
                    case "1.5":
                        stopBits = StopBits.OnePointFive;
                        break;
                    case "1,5":
                        stopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        stopBits = StopBits.Two;
                        break;
                    case "one":
                        stopBits = StopBits.One;
                        break;
                    case "onepointfive":
                        stopBits = StopBits.OnePointFive;
                        break;
                    case "two":
                        stopBits = StopBits.Two;
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                Loger.WriteToFile(errorTitle + "количества стоповых бит ('stop_bits')." + useHelp);
            }

            // Инициализируем COM-порт
            if (Communicators.Initialization(port_name, baudRate, parity, stopBits))
            {
                textBoxLogs.AppendText("Установлены настройки COM-порта." + Environment.NewLine);
                textBoxLogs.AppendText(
                    "Имя - " + Communicators.comPort.PortName +
                    "; скорость - " + Communicators.comPort.BaudRate.ToString() +
                    "; число бит - " + Communicators.comPort.DataBits.ToString() +
                    "; четность - " + Communicators.ParityToStr(Communicators.comPort.Parity) +
                    "; стоп биты - " + Communicators.StopBitsToStr(Communicators.comPort.StopBits) +
                    "." + Environment.NewLine);
            }
        }
        /// <summary>
        /// Изменяент цвета КП в дереве доступных объектов
        /// в зависимости от наличия связи. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MonitorConnectonState(object sender, MsgArrivedEventArgs e)
        {
            // Модуль сгенерировавший событие
            ModulNevod modul = sender as ModulNevod;

            if (e.Message == "Failed")
            {
                treeViewConfig.NodeConnectionState(modul.ModulName, false);
            }
            else
            {
                treeViewConfig.NodeConnectionState(modul.ModulName, true);
            }
        }
        /// <summary>
        /// Происходит при выборе модуля в дереве объектов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewConfig_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Синхронизируем выбо в дереве оъектов 
            // с выбором в выпадающем списке
            cBoxModuls.SelectedIndex = e.Node.Index;
        }
    }

    public struct NevodCommand
    {
        public string Cmd;
        public int NumberKP;
        public object Parameter;

        public NevodCommand(string Cmd, int NumberKP, object Parameter)
        {
            this.Cmd = Cmd;
            this.NumberKP = NumberKP;
            this.Parameter = Parameter;
        }
    }
}
