using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;
using System.IO;
using System.Xml.Linq;

namespace FISHER
{
    /// <summary>
    /// Класс формы для настройки параметров COM-порта
    /// </summary>
    public partial class FormCOMPort : Form
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public FormCOMPort()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Происходит при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCOMPort_Load(object sender, EventArgs e)
        {
            // Поиск доступных портов
            string[] availablePorts = SerialPort.GetPortNames();
            foreach (string port in availablePorts)
            {
                cBoxPortName.Items.Add(port);
            }

            // Если порт создан подгружаем текущие настройки
            if (Communicators.comPort != null)
            {
                cBoxPortName.Text = Communicators.comPort.PortName;
                cBoxBaudRate.Text = Convert.ToString(Communicators.comPort.BaudRate);
                cBoxDataBits.Text = Convert.ToString(Communicators.comPort.DataBits);
                cBoxParity.SelectedIndex = (int)Communicators.comPort.Parity;
                cBoxStopBits.SelectedIndex = (int)Communicators.comPort.StopBits - 1;
            }
            else
            {
                if (availablePorts.Length > 0) cBoxPortName.SelectedIndex = 0;
                cBoxBaudRate.SelectedIndex = 3;
                cBoxDataBits.SelectedIndex = 0;
                cBoxParity.SelectedIndex = 0;
                cBoxStopBits.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Закрыть форму, не сохранив изменения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Закрыть форму, и сохранть изменения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                // Четность
                Parity parity;
                switch (cBoxParity.Text)
                {
                    case "нет - 00":
                        parity = Parity.None;
                        break;
                    case "четный":
                        parity = Parity.Even;
                        break;
                    case "нечетный":
                        parity = Parity.Odd;
                        break;
                    default:
                        parity = Parity.None;
                        break;
                }

                // Стоп-биты
                StopBits stopBits;
                switch (cBoxStopBits.Text)
                {
                    case "1":
                        stopBits = StopBits.One;
                        break;
                    case "1.5":
                        stopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        stopBits = StopBits.Two;
                        break;
                    default:
                        stopBits = StopBits.One;
                        break;
                }

                // Инициализация порта
                Communicators.Initialization(cBoxPortName.Text,
                    Int32.Parse(cBoxBaudRate.Text), parity, stopBits);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, this.Text);
            }
            finally
            {
                // Пишем лог о изменении данных
                FormMain f = (FormMain)Owner;
                f.textBoxLogs.AppendText("Установлены настройки COM-порта." + Environment.NewLine);
                f.textBoxLogs.AppendText(
                    "Имя - " + Communicators.comPort.PortName +
                    "; скорость - " + cBoxBaudRate.Text +
                    "; число бит - " + cBoxDataBits.Text +
                    "; четность - " + cBoxParity.Text +
                    "; стоп биты - " + cBoxStopBits.Text +
                    "." + Environment.NewLine);

                ChangeXmlConfigFile();

                // Закрыть форму настроек
                Close();
            }
        }
        /// <summary>
        /// Сохранить изменения в xml-файле конфигурации
        /// </summary>
        /// <returns>Возвращает результат сохранения</returns>
        public static bool ChangeXmlConfigFile()
        {
            string fileName = "server_config.xml";
            if (!File.Exists(fileName))
            {
                // Выводим ответ 
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    "Параметры COM-порта не были сохранены. Файл конфигурации не найден!");
                return false;
            }
            try
            {
                // Загружаенм данные из файла
                XDocument xdoc = XDocument.Load(fileName);

                // Находим элемент хранящий настройки COM-порта
                XElement xSerialPort = xdoc.Element("root").Element("serial_port");

                // Если эхлемент с настройками COM-порта был удален,
                // создаем его зпанова.
                if (xSerialPort == null)
                {
                    xdoc.Element("root").AddFirst(new XElement("serial_port"));
                    xSerialPort = xdoc.Element("root").Element("serial_port");
                }

                // Изменяем атрибуты элемента "serial_port"
                // Имя COM-порта
                xSerialPort.SetAttributeValue("port_name", Communicators.comPort.PortName);
                // Скорость передачи данных
                xSerialPort.SetAttributeValue("baud_rate", Communicators.comPort.BaudRate.ToString());
                // Четность
                xSerialPort.SetAttributeValue("parity", Communicators.comPort.Parity.ToString());
                // Число стоповых битов в байте
                xSerialPort.SetAttributeValue("stop_bits", Communicators.comPort.StopBits.ToString()
                    .Replace("One", "1")
                    .Replace("OnePointFive","1.5")
                    .Replace("Two", "2"));

                // Сохраняем имзмененный файл
                xdoc.Save(fileName);

                return true;
            }
            catch (Exception exp)
            {
                // Выводим ответ 
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    "Параметры COM-порта не были сохранены. " + exp.Message);
                return false;
            }
        }
    }
}
