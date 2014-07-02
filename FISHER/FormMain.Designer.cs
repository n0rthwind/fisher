namespace FISHER
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Список устройств");
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxCMD = new System.Windows.Forms.TextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.taskBarIco = new System.Windows.Forms.NotifyIcon(this.components);
            this.buttonСomSetings = new System.Windows.Forms.Button();
            this.labelParametrs = new System.Windows.Forms.Label();
            this.cBoxModuls = new System.Windows.Forms.ComboBox();
            this.labelForModulName = new System.Windows.Forms.Label();
            this.labelForCommand = new System.Windows.Forms.Label();
            this.buttonPauseLoger = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabelPanel1 = new FISHER.TablePanel();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dOut8 = new System.Windows.Forms.Panel();
            this.dOut7 = new System.Windows.Forms.Panel();
            this.dOut6 = new System.Windows.Forms.Panel();
            this.dOut5 = new System.Windows.Forms.Panel();
            this.dOut4 = new System.Windows.Forms.Panel();
            this.dOut3 = new System.Windows.Forms.Panel();
            this.dOut2 = new System.Windows.Forms.Panel();
            this.dIn8 = new System.Windows.Forms.Panel();
            this.dIn7 = new System.Windows.Forms.Panel();
            this.dIn6 = new System.Windows.Forms.Panel();
            this.dIn5 = new System.Windows.Forms.Panel();
            this.dIn4 = new System.Windows.Forms.Panel();
            this.dIn3 = new System.Windows.Forms.Panel();
            this.dIn2 = new System.Windows.Forms.Panel();
            this.aIn5 = new System.Windows.Forms.TextBox();
            this.aIn4 = new System.Windows.Forms.TextBox();
            this.aIn3 = new System.Windows.Forms.TextBox();
            this.aIn2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelForSwitch = new System.Windows.Forms.Label();
            this.labelForSetBit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetZero1 = new System.Windows.Forms.Button();
            this.dOut1 = new System.Windows.Forms.Panel();
            this.btnSetOne1 = new System.Windows.Forms.Button();
            this.labelForInputs = new System.Windows.Forms.Label();
            this.labelForOutputs = new System.Windows.Forms.Label();
            this.dIn1 = new System.Windows.Forms.Panel();
            this.btnSwitch1 = new System.Windows.Forms.Button();
            this.labelForAInputs = new System.Windows.Forms.Label();
            this.aIn1 = new System.Windows.Forms.TextBox();
            this.treeViewConfig = new FISHER.MyTreeView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabelPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Location = new System.Drawing.Point(281, 6);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(93, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLogs.Location = new System.Drawing.Point(172, 19);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogs.Size = new System.Drawing.Size(415, 277);
            this.textBoxLogs.TabIndex = 1;
            this.textBoxLogs.Text = "История обмена:\r\n";
            // 
            // buttonSend
            // 
            this.buttonSend.Enabled = false;
            this.buttonSend.Location = new System.Drawing.Point(516, 24);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxCMD
            // 
            this.textBoxCMD.Enabled = false;
            this.textBoxCMD.Location = new System.Drawing.Point(287, 26);
            this.textBoxCMD.Name = "textBoxCMD";
            this.textBoxCMD.Size = new System.Drawing.Size(223, 20);
            this.textBoxCMD.TabIndex = 3;
            this.textBoxCMD.Text = "$44M";
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(380, 6);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(93, 23);
            this.buttonStop.TabIndex = 6;
            this.buttonStop.Text = "Стоп";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(182, 6);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(93, 23);
            this.buttonConnect.TabIndex = 8;
            this.buttonConnect.Text = "Подключиться";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(479, 6);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(93, 23);
            this.buttonDisconnect.TabIndex = 9;
            this.buttonDisconnect.Text = "Отключиться";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // taskBarIco
            // 
            this.taskBarIco.Icon = ((System.Drawing.Icon)(resources.GetObject("taskBarIco.Icon")));
            this.taskBarIco.Text = "Fisher";
            this.taskBarIco.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.taskBarIco_MouseDoubleClick);
            // 
            // buttonСomSetings
            // 
            this.buttonСomSetings.Location = new System.Drawing.Point(12, 6);
            this.buttonСomSetings.Name = "buttonСomSetings";
            this.buttonСomSetings.Size = new System.Drawing.Size(154, 23);
            this.buttonСomSetings.TabIndex = 11;
            this.buttonСomSetings.Text = "Настройки COM-порта";
            this.buttonСomSetings.UseVisualStyleBackColor = true;
            this.buttonСomSetings.Click += new System.EventHandler(this.buttonСomSetings_Click);
            // 
            // labelParametrs
            // 
            this.labelParametrs.AutoSize = true;
            this.labelParametrs.Location = new System.Drawing.Point(172, 312);
            this.labelParametrs.Name = "labelParametrs";
            this.labelParametrs.Size = new System.Drawing.Size(74, 13);
            this.labelParametrs.TabIndex = 12;
            this.labelParametrs.Text = "Ошибки: 0; 0.";
            // 
            // cBoxModuls
            // 
            this.cBoxModuls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxModuls.FormattingEnabled = true;
            this.cBoxModuls.Location = new System.Drawing.Point(57, 24);
            this.cBoxModuls.Name = "cBoxModuls";
            this.cBoxModuls.Size = new System.Drawing.Size(106, 21);
            this.cBoxModuls.TabIndex = 14;
            this.cBoxModuls.SelectedIndexChanged += new System.EventHandler(this.cBoxModuls_SelectedIndexChanged);
            // 
            // labelForModulName
            // 
            this.labelForModulName.AutoSize = true;
            this.labelForModulName.Location = new System.Drawing.Point(6, 27);
            this.labelForModulName.Name = "labelForModulName";
            this.labelForModulName.Size = new System.Drawing.Size(45, 13);
            this.labelForModulName.TabIndex = 15;
            this.labelForModulName.Text = "Модуль";
            // 
            // labelForCommand
            // 
            this.labelForCommand.AutoSize = true;
            this.labelForCommand.Location = new System.Drawing.Point(229, 28);
            this.labelForCommand.Name = "labelForCommand";
            this.labelForCommand.Size = new System.Drawing.Size(52, 13);
            this.labelForCommand.TabIndex = 16;
            this.labelForCommand.Text = "Команда";
            // 
            // buttonPauseLoger
            // 
            this.buttonPauseLoger.Location = new System.Drawing.Point(497, 307);
            this.buttonPauseLoger.Name = "buttonPauseLoger";
            this.buttonPauseLoger.Size = new System.Drawing.Size(90, 23);
            this.buttonPauseLoger.TabIndex = 17;
            this.buttonPauseLoger.Text = "Остановить";
            this.buttonPauseLoger.UseVisualStyleBackColor = true;
            this.buttonPauseLoger.Click += new System.EventHandler(this.buttonPauseLoger_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(401, 308);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(90, 23);
            this.buttonClear.TabIndex = 18;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxLogs);
            this.groupBox1.Controls.Add(this.buttonPauseLoger);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.labelParametrs);
            this.groupBox1.Controls.Add(this.treeViewConfig);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 336);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные обмена";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelForModulName);
            this.groupBox2.Controls.Add(this.buttonSend);
            this.groupBox2.Controls.Add(this.labelForCommand);
            this.groupBox2.Controls.Add(this.textBoxCMD);
            this.groupBox2.Controls.Add(this.tabelPanel1);
            this.groupBox2.Controls.Add(this.cBoxModuls);
            this.groupBox2.Location = new System.Drawing.Point(12, 389);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 212);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Управление модулем";
            // 
            // tabelPanel1
            // 
            this.tabelPanel1.BorderColor = System.Drawing.Color.DimGray;
            this.tabelPanel1.CellHeigth = 23;
            this.tabelPanel1.CellWidth = 60;
            this.tabelPanel1.Columns = 10;
            this.tabelPanel1.Controls.Add(this.button20);
            this.tabelPanel1.Controls.Add(this.button21);
            this.tabelPanel1.Controls.Add(this.button22);
            this.tabelPanel1.Controls.Add(this.button17);
            this.tabelPanel1.Controls.Add(this.button18);
            this.tabelPanel1.Controls.Add(this.button19);
            this.tabelPanel1.Controls.Add(this.button14);
            this.tabelPanel1.Controls.Add(this.button15);
            this.tabelPanel1.Controls.Add(this.button16);
            this.tabelPanel1.Controls.Add(this.button11);
            this.tabelPanel1.Controls.Add(this.button12);
            this.tabelPanel1.Controls.Add(this.button13);
            this.tabelPanel1.Controls.Add(this.button8);
            this.tabelPanel1.Controls.Add(this.button9);
            this.tabelPanel1.Controls.Add(this.button10);
            this.tabelPanel1.Controls.Add(this.button5);
            this.tabelPanel1.Controls.Add(this.button6);
            this.tabelPanel1.Controls.Add(this.button7);
            this.tabelPanel1.Controls.Add(this.button2);
            this.tabelPanel1.Controls.Add(this.button3);
            this.tabelPanel1.Controls.Add(this.button4);
            this.tabelPanel1.Controls.Add(this.dOut8);
            this.tabelPanel1.Controls.Add(this.dOut7);
            this.tabelPanel1.Controls.Add(this.dOut6);
            this.tabelPanel1.Controls.Add(this.dOut5);
            this.tabelPanel1.Controls.Add(this.dOut4);
            this.tabelPanel1.Controls.Add(this.dOut3);
            this.tabelPanel1.Controls.Add(this.dOut2);
            this.tabelPanel1.Controls.Add(this.dIn8);
            this.tabelPanel1.Controls.Add(this.dIn7);
            this.tabelPanel1.Controls.Add(this.dIn6);
            this.tabelPanel1.Controls.Add(this.dIn5);
            this.tabelPanel1.Controls.Add(this.dIn4);
            this.tabelPanel1.Controls.Add(this.dIn3);
            this.tabelPanel1.Controls.Add(this.dIn2);
            this.tabelPanel1.Controls.Add(this.aIn5);
            this.tabelPanel1.Controls.Add(this.aIn4);
            this.tabelPanel1.Controls.Add(this.aIn3);
            this.tabelPanel1.Controls.Add(this.aIn2);
            this.tabelPanel1.Controls.Add(this.label8);
            this.tabelPanel1.Controls.Add(this.label7);
            this.tabelPanel1.Controls.Add(this.label6);
            this.tabelPanel1.Controls.Add(this.label5);
            this.tabelPanel1.Controls.Add(this.label4);
            this.tabelPanel1.Controls.Add(this.label3);
            this.tabelPanel1.Controls.Add(this.label2);
            this.tabelPanel1.Controls.Add(this.labelForSwitch);
            this.tabelPanel1.Controls.Add(this.labelForSetBit);
            this.tabelPanel1.Controls.Add(this.label1);
            this.tabelPanel1.Controls.Add(this.btnSetZero1);
            this.tabelPanel1.Controls.Add(this.dOut1);
            this.tabelPanel1.Controls.Add(this.btnSetOne1);
            this.tabelPanel1.Controls.Add(this.labelForInputs);
            this.tabelPanel1.Controls.Add(this.labelForOutputs);
            this.tabelPanel1.Controls.Add(this.dIn1);
            this.tabelPanel1.Controls.Add(this.btnSwitch1);
            this.tabelPanel1.Controls.Add(this.labelForAInputs);
            this.tabelPanel1.Controls.Add(this.aIn1);
            this.tabelPanel1.Enabled = false;
            this.tabelPanel1.FirstColumn = 100;
            this.tabelPanel1.Location = new System.Drawing.Point(9, 57);
            this.tabelPanel1.Name = "tabelPanel1";
            this.tabelPanel1.Rows = 7;
            this.tabelPanel1.Size = new System.Drawing.Size(582, 140);
            this.tabelPanel1.TabIndex = 13;
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(527, 94);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(22, 20);
            this.button20.TabIndex = 75;
            this.button20.Tag = "7";
            this.button20.Text = "0";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(553, 94);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(22, 20);
            this.button21.TabIndex = 74;
            this.button21.Tag = "7";
            this.button21.Text = "1";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.btnSetOne_Click);
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(527, 117);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(48, 20);
            this.button22.TabIndex = 76;
            this.button22.Tag = "7";
            this.button22.Text = "1 - - - 0";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(466, 94);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(22, 20);
            this.button17.TabIndex = 72;
            this.button17.Tag = "6";
            this.button17.Text = "0";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(492, 94);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(22, 20);
            this.button18.TabIndex = 71;
            this.button18.Tag = "6";
            this.button18.Text = "1";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.btnSetOne_Click);
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(466, 117);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(48, 20);
            this.button19.TabIndex = 73;
            this.button19.Tag = "6";
            this.button19.Text = "1 - - - 0";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(407, 94);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(22, 20);
            this.button14.TabIndex = 69;
            this.button14.Tag = "5";
            this.button14.Text = "0";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(433, 94);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(22, 20);
            this.button15.TabIndex = 68;
            this.button15.Tag = "5";
            this.button15.Text = "1";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.btnSetOne_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(407, 117);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(48, 20);
            this.button16.TabIndex = 70;
            this.button16.Tag = "5";
            this.button16.Text = "1 - - - 0";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(347, 94);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(22, 20);
            this.button11.TabIndex = 66;
            this.button11.Tag = "4";
            this.button11.Text = "0";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(373, 94);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(22, 20);
            this.button12.TabIndex = 65;
            this.button12.Tag = "4";
            this.button12.Text = "1";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.btnSetOne_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(347, 117);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(48, 20);
            this.button13.TabIndex = 67;
            this.button13.Tag = "4";
            this.button13.Text = "1 - - - 0";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(287, 94);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(22, 20);
            this.button8.TabIndex = 63;
            this.button8.Tag = "3";
            this.button8.Text = "0";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(313, 94);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(22, 20);
            this.button9.TabIndex = 62;
            this.button9.Tag = "3";
            this.button9.Text = "1";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.btnSetOne_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(287, 117);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(48, 20);
            this.button10.TabIndex = 64;
            this.button10.Tag = "3";
            this.button10.Text = "1 - - - 0";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(227, 94);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(22, 20);
            this.button5.TabIndex = 60;
            this.button5.Tag = "2";
            this.button5.Text = "0";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(253, 94);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(22, 20);
            this.button6.TabIndex = 59;
            this.button6.Tag = "2";
            this.button6.Text = "1";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnSetOne_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(227, 117);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(48, 20);
            this.button7.TabIndex = 61;
            this.button7.Tag = "2";
            this.button7.Text = "1 - - - 0";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(167, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(22, 20);
            this.button2.TabIndex = 57;
            this.button2.Tag = "1";
            this.button2.Text = "0";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(193, 94);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(22, 20);
            this.button3.TabIndex = 56;
            this.button3.Tag = "1";
            this.button3.Text = "1";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnSetOne_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(167, 117);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(48, 20);
            this.button4.TabIndex = 58;
            this.button4.Tag = "1";
            this.button4.Text = "1 - - - 0";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // dOut8
            // 
            this.dOut8.BackColor = System.Drawing.Color.Black;
            this.dOut8.Location = new System.Drawing.Point(527, 71);
            this.dOut8.Name = "dOut8";
            this.dOut8.Size = new System.Drawing.Size(48, 19);
            this.dOut8.TabIndex = 28;
            // 
            // dOut7
            // 
            this.dOut7.BackColor = System.Drawing.Color.Black;
            this.dOut7.Location = new System.Drawing.Point(466, 71);
            this.dOut7.Name = "dOut7";
            this.dOut7.Size = new System.Drawing.Size(48, 19);
            this.dOut7.TabIndex = 27;
            // 
            // dOut6
            // 
            this.dOut6.BackColor = System.Drawing.Color.Black;
            this.dOut6.Location = new System.Drawing.Point(407, 71);
            this.dOut6.Name = "dOut6";
            this.dOut6.Size = new System.Drawing.Size(48, 19);
            this.dOut6.TabIndex = 26;
            // 
            // dOut5
            // 
            this.dOut5.BackColor = System.Drawing.Color.Black;
            this.dOut5.Location = new System.Drawing.Point(347, 71);
            this.dOut5.Name = "dOut5";
            this.dOut5.Size = new System.Drawing.Size(48, 19);
            this.dOut5.TabIndex = 25;
            // 
            // dOut4
            // 
            this.dOut4.BackColor = System.Drawing.Color.Black;
            this.dOut4.Location = new System.Drawing.Point(287, 71);
            this.dOut4.Name = "dOut4";
            this.dOut4.Size = new System.Drawing.Size(48, 19);
            this.dOut4.TabIndex = 24;
            // 
            // dOut3
            // 
            this.dOut3.BackColor = System.Drawing.Color.Black;
            this.dOut3.Location = new System.Drawing.Point(227, 71);
            this.dOut3.Name = "dOut3";
            this.dOut3.Size = new System.Drawing.Size(48, 19);
            this.dOut3.TabIndex = 23;
            // 
            // dOut2
            // 
            this.dOut2.BackColor = System.Drawing.Color.Black;
            this.dOut2.Location = new System.Drawing.Point(167, 71);
            this.dOut2.Name = "dOut2";
            this.dOut2.Size = new System.Drawing.Size(48, 19);
            this.dOut2.TabIndex = 22;
            // 
            // dIn8
            // 
            this.dIn8.BackColor = System.Drawing.Color.Black;
            this.dIn8.Location = new System.Drawing.Point(527, 48);
            this.dIn8.Name = "dIn8";
            this.dIn8.Size = new System.Drawing.Size(48, 19);
            this.dIn8.TabIndex = 55;
            // 
            // dIn7
            // 
            this.dIn7.BackColor = System.Drawing.Color.Black;
            this.dIn7.Location = new System.Drawing.Point(466, 48);
            this.dIn7.Name = "dIn7";
            this.dIn7.Size = new System.Drawing.Size(48, 19);
            this.dIn7.TabIndex = 54;
            // 
            // dIn6
            // 
            this.dIn6.BackColor = System.Drawing.Color.Black;
            this.dIn6.Location = new System.Drawing.Point(407, 48);
            this.dIn6.Name = "dIn6";
            this.dIn6.Size = new System.Drawing.Size(48, 19);
            this.dIn6.TabIndex = 53;
            // 
            // dIn5
            // 
            this.dIn5.BackColor = System.Drawing.Color.Black;
            this.dIn5.Location = new System.Drawing.Point(347, 48);
            this.dIn5.Name = "dIn5";
            this.dIn5.Size = new System.Drawing.Size(48, 19);
            this.dIn5.TabIndex = 52;
            // 
            // dIn4
            // 
            this.dIn4.BackColor = System.Drawing.Color.Black;
            this.dIn4.Location = new System.Drawing.Point(287, 48);
            this.dIn4.Name = "dIn4";
            this.dIn4.Size = new System.Drawing.Size(48, 19);
            this.dIn4.TabIndex = 51;
            // 
            // dIn3
            // 
            this.dIn3.BackColor = System.Drawing.Color.Black;
            this.dIn3.Location = new System.Drawing.Point(227, 48);
            this.dIn3.Name = "dIn3";
            this.dIn3.Size = new System.Drawing.Size(48, 19);
            this.dIn3.TabIndex = 24;
            // 
            // dIn2
            // 
            this.dIn2.BackColor = System.Drawing.Color.Black;
            this.dIn2.Location = new System.Drawing.Point(167, 48);
            this.dIn2.Name = "dIn2";
            this.dIn2.Size = new System.Drawing.Size(48, 19);
            this.dIn2.TabIndex = 23;
            // 
            // aIn5
            // 
            this.aIn5.BackColor = System.Drawing.Color.White;
            this.aIn5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aIn5.Cursor = System.Windows.Forms.Cursors.Default;
            this.aIn5.Location = new System.Drawing.Point(347, 25);
            this.aIn5.Name = "aIn5";
            this.aIn5.ReadOnly = true;
            this.aIn5.Size = new System.Drawing.Size(48, 20);
            this.aIn5.TabIndex = 50;
            this.aIn5.Text = "###.##";
            this.aIn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // aIn4
            // 
            this.aIn4.BackColor = System.Drawing.Color.White;
            this.aIn4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aIn4.Cursor = System.Windows.Forms.Cursors.Default;
            this.aIn4.Location = new System.Drawing.Point(287, 25);
            this.aIn4.Name = "aIn4";
            this.aIn4.ReadOnly = true;
            this.aIn4.Size = new System.Drawing.Size(48, 20);
            this.aIn4.TabIndex = 41;
            this.aIn4.Text = "###.##";
            this.aIn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // aIn3
            // 
            this.aIn3.BackColor = System.Drawing.Color.White;
            this.aIn3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aIn3.Cursor = System.Windows.Forms.Cursors.Default;
            this.aIn3.Location = new System.Drawing.Point(227, 25);
            this.aIn3.Name = "aIn3";
            this.aIn3.ReadOnly = true;
            this.aIn3.Size = new System.Drawing.Size(48, 20);
            this.aIn3.TabIndex = 49;
            this.aIn3.Text = "###.##";
            this.aIn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // aIn2
            // 
            this.aIn2.BackColor = System.Drawing.Color.White;
            this.aIn2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aIn2.Cursor = System.Windows.Forms.Cursors.Default;
            this.aIn2.Location = new System.Drawing.Point(167, 25);
            this.aIn2.Name = "aIn2";
            this.aIn2.ReadOnly = true;
            this.aIn2.Size = new System.Drawing.Size(48, 20);
            this.aIn2.TabIndex = 48;
            this.aIn2.Text = "###.##";
            this.aIn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(543, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "8";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(484, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 46;
            this.label7.Text = "7";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(422, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "6";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(364, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(305, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(243, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(182, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelForSwitch
            // 
            this.labelForSwitch.AutoSize = true;
            this.labelForSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelForSwitch.Location = new System.Drawing.Point(3, 120);
            this.labelForSwitch.Name = "labelForSwitch";
            this.labelForSwitch.Size = new System.Drawing.Size(75, 13);
            this.labelForSwitch.TabIndex = 32;
            this.labelForSwitch.Text = "Переключить";
            // 
            // labelForSetBit
            // 
            this.labelForSetBit.AutoSize = true;
            this.labelForSetBit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelForSetBit.Location = new System.Drawing.Point(3, 97);
            this.labelForSetBit.Name = "labelForSetBit";
            this.labelForSetBit.Size = new System.Drawing.Size(87, 13);
            this.labelForSetBit.TabIndex = 31;
            this.labelForSetBit.Text = "Установить бит";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(122, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnSetZero1
            // 
            this.btnSetZero1.Location = new System.Drawing.Point(106, 94);
            this.btnSetZero1.Name = "btnSetZero1";
            this.btnSetZero1.Size = new System.Drawing.Size(22, 20);
            this.btnSetZero1.TabIndex = 34;
            this.btnSetZero1.Tag = "0";
            this.btnSetZero1.Text = "0";
            this.btnSetZero1.UseVisualStyleBackColor = true;
            this.btnSetZero1.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // dOut1
            // 
            this.dOut1.BackColor = System.Drawing.Color.Black;
            this.dOut1.Location = new System.Drawing.Point(106, 71);
            this.dOut1.Name = "dOut1";
            this.dOut1.Size = new System.Drawing.Size(48, 19);
            this.dOut1.TabIndex = 21;
            // 
            // btnSetOne1
            // 
            this.btnSetOne1.Location = new System.Drawing.Point(132, 94);
            this.btnSetOne1.Name = "btnSetOne1";
            this.btnSetOne1.Size = new System.Drawing.Size(22, 20);
            this.btnSetOne1.TabIndex = 33;
            this.btnSetOne1.Tag = "0";
            this.btnSetOne1.Text = "1";
            this.btnSetOne1.UseVisualStyleBackColor = true;
            this.btnSetOne1.Click += new System.EventHandler(this.btnSetOne_Click);
            // 
            // labelForInputs
            // 
            this.labelForInputs.AutoSize = true;
            this.labelForInputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelForInputs.Location = new System.Drawing.Point(3, 51);
            this.labelForInputs.Name = "labelForInputs";
            this.labelForInputs.Size = new System.Drawing.Size(88, 13);
            this.labelForInputs.TabIndex = 36;
            this.labelForInputs.Text = "Дискрет. входы";
            // 
            // labelForOutputs
            // 
            this.labelForOutputs.AutoSize = true;
            this.labelForOutputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelForOutputs.Location = new System.Drawing.Point(3, 74);
            this.labelForOutputs.Name = "labelForOutputs";
            this.labelForOutputs.Size = new System.Drawing.Size(96, 13);
            this.labelForOutputs.TabIndex = 37;
            this.labelForOutputs.Text = "Дискрет. выходы";
            // 
            // dIn1
            // 
            this.dIn1.BackColor = System.Drawing.Color.Black;
            this.dIn1.Location = new System.Drawing.Point(106, 48);
            this.dIn1.Name = "dIn1";
            this.dIn1.Size = new System.Drawing.Size(48, 19);
            this.dIn1.TabIndex = 22;
            // 
            // btnSwitch1
            // 
            this.btnSwitch1.Location = new System.Drawing.Point(106, 117);
            this.btnSwitch1.Name = "btnSwitch1";
            this.btnSwitch1.Size = new System.Drawing.Size(48, 20);
            this.btnSwitch1.TabIndex = 35;
            this.btnSwitch1.Tag = "0";
            this.btnSwitch1.Text = "1 - - - 0";
            this.btnSwitch1.UseVisualStyleBackColor = true;
            this.btnSwitch1.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // labelForAInputs
            // 
            this.labelForAInputs.AutoSize = true;
            this.labelForAInputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelForAInputs.Location = new System.Drawing.Point(3, 28);
            this.labelForAInputs.Name = "labelForAInputs";
            this.labelForAInputs.Size = new System.Drawing.Size(80, 13);
            this.labelForAInputs.TabIndex = 38;
            this.labelForAInputs.Text = "Аналог. входы";
            // 
            // aIn1
            // 
            this.aIn1.BackColor = System.Drawing.Color.White;
            this.aIn1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.aIn1.Cursor = System.Windows.Forms.Cursors.Default;
            this.aIn1.Location = new System.Drawing.Point(106, 25);
            this.aIn1.Name = "aIn1";
            this.aIn1.ReadOnly = true;
            this.aIn1.Size = new System.Drawing.Size(48, 20);
            this.aIn1.TabIndex = 40;
            this.aIn1.Text = "###.##";
            this.aIn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // treeViewConfig
            // 
            this.treeViewConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeViewConfig.Location = new System.Drawing.Point(9, 19);
            this.treeViewConfig.Name = "treeViewConfig";
            treeNode1.Name = "ListOfModuls";
            treeNode1.Text = "Список устройств";
            this.treeViewConfig.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewConfig.Size = new System.Drawing.Size(148, 277);
            this.treeViewConfig.TabIndex = 10;
            this.treeViewConfig.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewConfig_AfterSelect);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 613);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonСomSetings);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "FISHER NMCS (Nevod Monitor and Control System)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabelPanel1.ResumeLayout(false);
            this.tabelPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        public System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxCMD;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private MyTreeView treeViewConfig;
        private System.Windows.Forms.NotifyIcon taskBarIco;
        private System.Windows.Forms.Button buttonСomSetings;
        private System.Windows.Forms.Label labelParametrs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelForSwitch;
        private System.Windows.Forms.Label labelForSetBit;
        private System.Windows.Forms.Button btnSetZero1;
        private System.Windows.Forms.Panel dOut1;
        private System.Windows.Forms.Button btnSetOne1;
        private System.Windows.Forms.Panel dIn1;
        private System.Windows.Forms.Button btnSwitch1;
        private System.Windows.Forms.Label labelForOutputs;
        private System.Windows.Forms.Label labelForInputs;
        private System.Windows.Forms.Label labelForAInputs;
        private System.Windows.Forms.TextBox aIn1;
        private TablePanel tabelPanel1;
        private System.Windows.Forms.Panel dOut8;
        private System.Windows.Forms.Panel dOut7;
        private System.Windows.Forms.Panel dOut6;
        private System.Windows.Forms.Panel dOut5;
        private System.Windows.Forms.Panel dOut4;
        private System.Windows.Forms.Panel dOut3;
        private System.Windows.Forms.Panel dOut2;
        private System.Windows.Forms.Panel dIn8;
        private System.Windows.Forms.Panel dIn7;
        private System.Windows.Forms.Panel dIn6;
        private System.Windows.Forms.Panel dIn5;
        private System.Windows.Forms.Panel dIn4;
        private System.Windows.Forms.Panel dIn3;
        private System.Windows.Forms.Panel dIn2;
        private System.Windows.Forms.TextBox aIn5;
        private System.Windows.Forms.TextBox aIn4;
        private System.Windows.Forms.TextBox aIn3;
        private System.Windows.Forms.TextBox aIn2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxModuls;
        private System.Windows.Forms.Label labelForModulName;
        private System.Windows.Forms.Label labelForCommand;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonPauseLoger;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}

