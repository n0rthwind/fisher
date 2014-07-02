using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Net;

namespace FISHER
{
    static class DataServer
    {
        static public AutoResetEvent WaitHandler = new AutoResetEvent(false);
        /// <summary>
        /// Возвращает или устанавливает состояние операции опроса модулей
        /// </summary>
        static public bool Running
        {
            get { return running; }
            set
            {
                // Записываем в лог-файл информайио о том, что орос устройст остановлен
                if (value) 
                    Loger.WriteToFile("Опрос устройств запущен...");
                else
                    // Блокируем возможность появления повторного сообщения
                    if(running) Loger.WriteToFile("Опрос устройств остановлен...");
                running = value;
            }
        }
        /// <summary>
        /// Хранит состояние операции опроса модулей (запущен или нет)
        /// </summary>
        static private bool running;
        /// <summary>
        /// Хранит командв отправленные пользователем до их выполнения
        /// </summary>
        static public volatile List<NevodCommand> hendCommmands = new List<NevodCommand>();
        /// <summary>
        /// Список модулей
        /// </summary>
        static public volatile List<ModulNevod> modulNevod = new List<ModulNevod>();
        /// <summary>
        /// Таймаут на запрос данных после телеуправления
        /// </summary>
        static public int timeout = 100;
        /// <summary>
        /// Запуск сервера данных
        /// </summary>
        static public void Run()
        {
            int last_index = 0;

            while (!Loger.closeAllThreads)
            {
                for (int j = last_index; j < modulNevod.Count; j++)
                {
                    // Прерываемся на неотложные команды
                    if (hendCommmands.Count > 0) break;

                    // Опрос Модулей
                    if (Running)
                    {
                        RefreshData(modulNevod[j]);
                    }
                    else
                    {
                        WaitHandler.WaitOne();
                        if (Loger.closeAllThreads) return;
                    }

                    // Запоминаем индекс следующего модуля для опроса
                    if ((j + 1) == modulNevod.Count)
                        last_index = 0;
                    else
                        last_index = j + 1;
                }

                // Отправляем неотложные команды
                while (hendCommmands.Count > 0)
                {
                    NevodCommand tempCmd = hendCommmands[0];
                    if (tempCmd.NumberKP == -1)
                    {
                        try
                        {
                            Communicators.comPort.WriteLine(tempCmd.Cmd);
                            Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                                "TX: " + tempCmd.Cmd);
                            Thread.Sleep(600);
                            byte[] buf = new byte[256];
                            int leng = Communicators.comPort.Read(buf, 0, 255);
                            StringBuilder response = new StringBuilder(40);
                            for (int i = 0; i < leng; i++)
                                response.Append(buf[i]);
                            Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                                "RX: " + response.ToString());

                            // Преобразуем байты из полубайты
                            int k = 0;
                            byte[] B = new byte[40];
                            for (int i = 0; i < 20; i++)
                            {
                                B[k] = Convert.ToByte(buf[i] >> 4);
                                B[k + 1] = Convert.ToByte(buf[i] & 15);
                                k += 2;
                            }

                            // Первое измерение
                            string AnIn1 = "";
                            for (int l = 0; l < 7; l++)
                            {
                                AnIn1 += ConvertToSymbol(B[l]);
                            }
                        }
                        catch (Exception exp)
                        {
                            Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                                exp.Message);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < modulNevod.Count; i++)
                        {
                            if (modulNevod[i].dgKpNum == tempCmd.NumberKP)
                            {
                                switch (tempCmd.Cmd)
                                {
                                    case "switch":
                                        modulNevod[i].Switch(Convert.ToInt32(tempCmd.Parameter));
                                        //Thread.Sleep(timeout);
                                        //RefreshData(modulNevod[i]);
                                        break;

                                    case "refresh":
                                        if (RefreshData(modulNevod[i]))
                                        {
                                            Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                                                "КП-" + Convert.ToString(tempCmd.NumberKP) +
                                                " данные принудительно обновлены");
                                        }
                                        break;

                                    case "on":
                                        modulNevod[i].SetState(Convert.ToInt32(tempCmd.Parameter), 1);
                                        //Thread.Sleep(timeout);
                                        //RefreshData(modulNevod[i]);
                                        break;

                                    case "off":
                                        modulNevod[i].SetState(Convert.ToInt32(tempCmd.Parameter), 0);
                                        //Thread.Sleep(timeout);
                                        //RefreshData(modulNevod[i]);
                                        break;
                                }
                                break;
                            }
                        }
                    }

                    // Удаляем выполненную команду
                    hendCommmands.RemoveAt(0);
                }
            }
        }
        /// <summary>
        /// Обновить значения переменных модуля
        /// </summary>
        /// <param name="modul">Ссылка на модуль</param>
        /// <returns>Сообщает удалось ли обновить данные</returns>
        public static bool RefreshData(ModulNevod modul)
        {
            string result = modul.AllVariablesRefresh();
            if (result == null)
            {
                return true;
            }
            else
            {
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    result);
                // Заканчиваем работу, если была подана команда
                // о завершении работы DataServer.
                if (!Running) return false;

                // Пытаемся получить данные во второй раз
                Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                    "КП-" + Convert.ToString(modul.dgKpNum) + " повторный запрос...");
                result = modul.AllVariablesRefresh();

                // Если данные пришли отпраить их "подписчикам"
                if (result == null)
                {
                    return true;
                }
                else
                {
                    Loger.SendMsg((int)MessagerId.Log, (int)MessagerId.DataServer,
                        result);
                }
            }

            return false;
        }
        /// <summary>
        /// Переводит коды потокового режима обмена данными с модулями в символы  
        /// </summary>
        /// <param name="B">Код символа</param>
        /// <returns>Символ</returns>
        static public char ConvertToSymbol(byte B)
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
    }
}
