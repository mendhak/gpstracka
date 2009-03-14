namespace GPSTracka
{
    partial class GPSTracka
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GPSTracka));
            this.timer1 = new System.Windows.Forms.Timer();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.TextBoxRawLog = new System.Windows.Forms.TextBox();
            this.mainPanelMenu = new System.Windows.Forms.MainMenu();
            this.startMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.clearMenuItem = new System.Windows.Forms.MenuItem();
            this.verboseMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.aboutPanel = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.aboutLabel = new System.Windows.Forms.Label();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.radioButtonKML = new System.Windows.Forms.RadioButton();
            this.radioButtonGPX = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new GroupBox();
            this.saveDialogButton = new System.Windows.Forms.Button();
            this.logLocationTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NumericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.ComboBoxCOMPorts = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new GroupBox();
            this.CheckBoxToFile = new System.Windows.Forms.CheckBox();
            this.CheckBoxToTextBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ComboBaudRate = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new GroupBox();
            this.groupBox3 = new GroupBox();
            this.groupBox4 = new GroupBox();
            this.aboutPanelMenu = new System.Windows.Forms.MainMenu();
            this.cancelMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsPanelMenu = new System.Windows.Forms.MainMenu();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.backMenuItem = new System.Windows.Forms.MenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.mainPanel.SuspendLayout();
            this.aboutPanel.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.TextBoxRawLog);
            this.mainPanel.Location = new System.Drawing.Point(3, 3);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(240, 294);
            // 
            // TextBoxRawLog
            // 
            this.TextBoxRawLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxRawLog.Location = new System.Drawing.Point(0, 0);
            this.TextBoxRawLog.Multiline = true;
            this.TextBoxRawLog.Name = "TextBoxRawLog";
            this.TextBoxRawLog.Size = new System.Drawing.Size(240, 294);
            this.TextBoxRawLog.TabIndex = 17;
            this.TextBoxRawLog.Text = "Don\'t forget to verify your settings in the settings screen. Use verbose to deter" +
                "mine if you have the right port. Use clear to clean up the textbox.";
            // 
            // mainPanelMenu
            // 
            this.mainPanelMenu.MenuItems.Add(this.startMenuItem);
            this.mainPanelMenu.MenuItems.Add(this.menuItem2);
            // 
            // startMenuItem
            // 
            this.startMenuItem.Text = "Start";
            this.startMenuItem.Click += new System.EventHandler(this.startMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.clearMenuItem);
            this.menuItem2.MenuItems.Add(this.verboseMenuItem);
            this.menuItem2.MenuItems.Add(this.settingsMenuItem);
            this.menuItem2.MenuItems.Add(this.menuItem5);
            this.menuItem2.MenuItems.Add(this.aboutMenuItem);
            this.menuItem2.MenuItems.Add(this.menuExit);
            this.menuItem2.Text = "Options";
            // 
            // clearMenuItem
            // 
            this.clearMenuItem.Text = "Clear";
            this.clearMenuItem.Click += new System.EventHandler(this.clearMenuItem_Click);
            // 
            // verboseMenuItem
            // 
            this.verboseMenuItem.Text = "Verbose";
            this.verboseMenuItem.Click += new System.EventHandler(this.verboseMenuItem_Click);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Text = "Settings";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "-";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Text = "About";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // menuExit
            // 
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // aboutPanel
            // 
            this.aboutPanel.Controls.Add(this.linkLabel1);
            this.aboutPanel.Controls.Add(this.pictureBox1);
            this.aboutPanel.Controls.Add(this.aboutLabel);
            this.aboutPanel.Location = new System.Drawing.Point(249, 3);
            this.aboutPanel.Name = "aboutPanel";
            this.aboutPanel.Size = new System.Drawing.Size(240, 294);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(3, 236);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(234, 20);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.Text = "http://www.codeplex.com/gpstracka";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // aboutLabel
            // 
            this.aboutLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.aboutLabel.Location = new System.Drawing.Point(3, 209);
            this.aboutLabel.Name = "aboutLabel";
            this.aboutLabel.Size = new System.Drawing.Size(234, 26);
            this.aboutLabel.Text = "GPSTracka Version 1.00";
            this.aboutLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // settingsPanel
            // 
            this.settingsPanel.AutoScroll = true;
            this.settingsPanel.Controls.Add(this.radioButtonKML);
            this.settingsPanel.Controls.Add(this.radioButtonGPX);
            this.settingsPanel.Controls.Add(this.groupBox5);
            this.settingsPanel.Controls.Add(this.saveDialogButton);
            this.settingsPanel.Controls.Add(this.logLocationTextBox);
            this.settingsPanel.Controls.Add(this.label1);
            this.settingsPanel.Controls.Add(this.NumericUpDownInterval);
            this.settingsPanel.Controls.Add(this.ComboBoxCOMPorts);
            this.settingsPanel.Controls.Add(this.label4);
            this.settingsPanel.Controls.Add(this.groupBox2);
            this.settingsPanel.Controls.Add(this.CheckBoxToFile);
            this.settingsPanel.Controls.Add(this.CheckBoxToTextBox);
            this.settingsPanel.Controls.Add(this.label5);
            this.settingsPanel.Controls.Add(this.ComboBaudRate);
            this.settingsPanel.Controls.Add(this.groupBox1);
            this.settingsPanel.Controls.Add(this.groupBox3);
            this.settingsPanel.Controls.Add(this.groupBox4);
            this.settingsPanel.Location = new System.Drawing.Point(495, 3);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(240, 349);
            // 
            // radioButtonKML
            // 
            this.radioButtonKML.Location = new System.Drawing.Point(12, 314);
            this.radioButtonKML.Name = "radioButtonKML";
            this.radioButtonKML.Size = new System.Drawing.Size(86, 20);
            this.radioButtonKML.TabIndex = 63;
            this.radioButtonKML.Text = "KML";
            // 
            // radioButtonGPX
            // 
            this.radioButtonGPX.Location = new System.Drawing.Point(12, 288);
            this.radioButtonGPX.Name = "radioButtonGPX";
            this.radioButtonGPX.Size = new System.Drawing.Size(86, 20);
            this.radioButtonGPX.TabIndex = 62;
            this.radioButtonGPX.Text = "GPX";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(3, 273);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(233, 72);
            this.groupBox5.TabIndex = 61;
            this.groupBox5.Title = "Format";
            // 
            // saveDialogButton
            // 
            this.saveDialogButton.Location = new System.Drawing.Point(165, 233);
            this.saveDialogButton.Name = "saveDialogButton";
            this.saveDialogButton.Size = new System.Drawing.Size(31, 20);
            this.saveDialogButton.TabIndex = 57;
            this.saveDialogButton.Text = "...";
            this.saveDialogButton.Click += new System.EventHandler(this.saveDialogButton_Click);
            // 
            // logLocationTextBox
            // 
            this.logLocationTextBox.Location = new System.Drawing.Point(12, 232);
            this.logLocationTextBox.Name = "logLocationTextBox";
            this.logLocationTextBox.Size = new System.Drawing.Size(150, 21);
            this.logLocationTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(107, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.Text = "seconds";
            // 
            // NumericUpDownInterval
            // 
            this.NumericUpDownInterval.Location = new System.Drawing.Point(30, 144);
            this.NumericUpDownInterval.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.NumericUpDownInterval.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NumericUpDownInterval.Name = "NumericUpDownInterval";
            this.NumericUpDownInterval.Size = new System.Drawing.Size(71, 22);
            this.NumericUpDownInterval.TabIndex = 54;
            this.NumericUpDownInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // ComboBoxCOMPorts
            // 
            this.ComboBoxCOMPorts.Location = new System.Drawing.Point(61, 80);
            this.ComboBoxCOMPorts.Name = "ComboBoxCOMPorts";
            this.ComboBoxCOMPorts.Size = new System.Drawing.Size(55, 22);
            this.ComboBoxCOMPorts.TabIndex = 50;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(30, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 20);
            this.label4.Text = "Read";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(3, 60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 56);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.Title = "Comm Port";
            // 
            // CheckBoxToFile
            // 
            this.CheckBoxToFile.Location = new System.Drawing.Point(114, 206);
            this.CheckBoxToFile.Name = "CheckBoxToFile";
            this.CheckBoxToFile.Size = new System.Drawing.Size(71, 20);
            this.CheckBoxToFile.TabIndex = 32;
            this.CheckBoxToFile.Text = "To File";
            // 
            // CheckBoxToTextBox
            // 
            this.CheckBoxToTextBox.Checked = true;
            this.CheckBoxToTextBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxToTextBox.Location = new System.Drawing.Point(12, 206);
            this.CheckBoxToTextBox.Name = "CheckBoxToTextBox";
            this.CheckBoxToTextBox.Size = new System.Drawing.Size(99, 20);
            this.CheckBoxToTextBox.TabIndex = 31;
            this.CheckBoxToTextBox.Text = "To Textbox";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(105, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 20);
            this.label5.Text = "baud";
            // 
            // ComboBaudRate
            // 
            this.ComboBaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBaudRate.Items.Add("4800");
            this.ComboBaudRate.Items.Add("9600");
            this.ComboBaudRate.Items.Add("19200");
            this.ComboBaudRate.Items.Add("38400");
            this.ComboBaudRate.Items.Add("57600");
            this.ComboBaudRate.Items.Add("115200");
            this.ComboBaudRate.Location = new System.Drawing.Point(24, 23);
            this.ComboBaudRate.Name = "ComboBaudRate";
            this.ComboBaudRate.Size = new System.Drawing.Size(74, 22);
            this.ComboBaudRate.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 51);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.Title = "Baud Rate";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(3, 122);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(234, 60);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.Title = "Polling Rate";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Location = new System.Drawing.Point(3, 188);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(234, 79);
            this.groupBox4.TabIndex = 56;
            this.groupBox4.Title = "Logging Options";
            // 
            // aboutPanelMenu
            // 
            this.aboutPanelMenu.MenuItems.Add(this.cancelMenuItem);
            // 
            // cancelMenuItem
            // 
            this.cancelMenuItem.Text = "Cancel";
            this.cancelMenuItem.Click += new System.EventHandler(this.cancelMenuItem_Click);
            // 
            // settingsPanelMenu
            // 
            this.settingsPanelMenu.MenuItems.Add(this.saveMenuItem);
            this.settingsPanelMenu.MenuItems.Add(this.backMenuItem);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Text = "Save";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // backMenuItem
            // 
            this.backMenuItem.Text = "Back";
            this.backMenuItem.Click += new System.EventHandler(this.backMenuItem_Click);
            // 
            // GPSTracka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.aboutPanel);
            this.Controls.Add(this.mainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainPanelMenu;
            this.Name = "GPSTracka";
            this.Text = "GPSTracka";
            this.Load += new System.EventHandler(this.GPSTracka_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.GPSTracka_Closing);
            this.mainPanel.ResumeLayout(false);
            this.aboutPanel.ResumeLayout(false);
            this.settingsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TextBox TextBoxRawLog;
        private System.Windows.Forms.MainMenu mainPanelMenu;
        private System.Windows.Forms.MenuItem startMenuItem;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem clearMenuItem;
        private System.Windows.Forms.MenuItem verboseMenuItem;
        private System.Windows.Forms.MenuItem settingsMenuItem;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem aboutMenuItem;
        private System.Windows.Forms.Panel aboutPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label aboutLabel;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.CheckBox CheckBoxToFile;
        private System.Windows.Forms.CheckBox CheckBoxToTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ComboBaudRate;
        private System.Windows.Forms.MainMenu aboutPanelMenu;
        private System.Windows.Forms.MainMenu settingsPanelMenu;
        private System.Windows.Forms.MenuItem cancelMenuItem;
        private System.Windows.Forms.MenuItem saveMenuItem;
        private GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NumericUpDownInterval;
        private System.Windows.Forms.ComboBox ComboBoxCOMPorts;
        private System.Windows.Forms.Label label4;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private System.Windows.Forms.Button saveDialogButton;
        private System.Windows.Forms.TextBox logLocationTextBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuItem backMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.RadioButton radioButtonKML;
        private System.Windows.Forms.RadioButton radioButtonGPX;
        private GroupBox groupBox5;
    }
}

