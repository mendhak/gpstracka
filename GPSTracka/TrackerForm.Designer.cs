namespace GPSTracka
{
    partial class GPSTracka
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.ButtonStartStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.TextBoxRawLog = new System.Windows.Forms.TextBox();
            this.NumericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.ComboBoxCOMPorts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckBoxToFile = new System.Windows.Forms.CheckBox();
            this.CheckBoxToTextBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ButtonLogAll = new System.Windows.Forms.Button();
            this.TextBoxCls = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonStartStop
            // 
            this.ButtonStartStop.Location = new System.Drawing.Point(3, 3);
            this.ButtonStartStop.Name = "ButtonStartStop";
            this.ButtonStartStop.Size = new System.Drawing.Size(72, 20);
            this.ButtonStartStop.TabIndex = 0;
            this.ButtonStartStop.Text = "Start";
            this.ButtonStartStop.Click += new System.EventHandler(this.ButtonStartStop_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TextBoxRawLog
            // 
            this.TextBoxRawLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxRawLog.Location = new System.Drawing.Point(3, 29);
            this.TextBoxRawLog.Multiline = true;
            this.TextBoxRawLog.Name = "TextBoxRawLog";
            this.TextBoxRawLog.Size = new System.Drawing.Size(234, 184);
            this.TextBoxRawLog.TabIndex = 1;
            // 
            // NumericUpDownInterval
            // 
            this.NumericUpDownInterval.Location = new System.Drawing.Point(144, 219);
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
            this.NumericUpDownInterval.TabIndex = 2;
            this.NumericUpDownInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // ComboBoxCOMPorts
            // 
            this.ComboBoxCOMPorts.Items.Add("COM1");
            this.ComboBoxCOMPorts.Items.Add("COM2");
            this.ComboBoxCOMPorts.Items.Add("COM3");
            this.ComboBoxCOMPorts.Items.Add("COM4");
            this.ComboBoxCOMPorts.Location = new System.Drawing.Point(40, 219);
            this.ComboBoxCOMPorts.Name = "ComboBoxCOMPorts";
            this.ComboBoxCOMPorts.Size = new System.Drawing.Size(55, 22);
            this.ComboBoxCOMPorts.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(215, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 20);
            this.label1.Text = "s";
            // 
            // CheckBoxToFile
            // 
            this.CheckBoxToFile.Location = new System.Drawing.Point(61, 244);
            this.CheckBoxToFile.Name = "CheckBoxToFile";
            this.CheckBoxToFile.Size = new System.Drawing.Size(71, 20);
            this.CheckBoxToFile.TabIndex = 5;
            this.CheckBoxToFile.Text = "To File";
            // 
            // CheckBoxToTextBox
            // 
            this.CheckBoxToTextBox.Checked = true;
            this.CheckBoxToTextBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxToTextBox.Location = new System.Drawing.Point(127, 244);
            this.CheckBoxToTextBox.Name = "CheckBoxToTextBox";
            this.CheckBoxToTextBox.Size = new System.Drawing.Size(100, 20);
            this.CheckBoxToTextBox.TabIndex = 6;
            this.CheckBoxToTextBox.Text = "To Textbox";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(106, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.Text = "every";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.Text = "and log";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 20);
            this.label4.Text = "Read";
            // 
            // ButtonLogAll
            // 
            this.ButtonLogAll.Location = new System.Drawing.Point(82, 3);
            this.ButtonLogAll.Name = "ButtonLogAll";
            this.ButtonLogAll.Size = new System.Drawing.Size(77, 20);
            this.ButtonLogAll.TabIndex = 10;
            this.ButtonLogAll.Text = "Verbose";
            this.ButtonLogAll.Click += new System.EventHandler(this.ButtonLogAll_Click);
            // 
            // TextBoxCls
            // 
            this.TextBoxCls.Location = new System.Drawing.Point(166, 3);
            this.TextBoxCls.Name = "TextBoxCls";
            this.TextBoxCls.Size = new System.Drawing.Size(61, 20);
            this.TextBoxCls.TabIndex = 15;
            this.TextBoxCls.Text = "Cls";
            this.TextBoxCls.Click += new System.EventHandler(this.TextBoxCls_Click);
            // 
            // GPSTracka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.TextBoxCls);
            this.Controls.Add(this.ButtonLogAll);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CheckBoxToTextBox);
            this.Controls.Add(this.CheckBoxToFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComboBoxCOMPorts);
            this.Controls.Add(this.NumericUpDownInterval);
            this.Controls.Add(this.TextBoxRawLog);
            this.Controls.Add(this.ButtonStartStop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "GPSTracka";
            this.Text = "GPSTracka";
            this.Load += new System.EventHandler(this.GPSTracka_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.GPSTracka_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonStartStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox TextBoxRawLog;
        private System.Windows.Forms.NumericUpDown NumericUpDownInterval;
        private System.Windows.Forms.ComboBox ComboBoxCOMPorts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CheckBoxToFile;
        private System.Windows.Forms.CheckBox CheckBoxToTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ButtonLogAll;
        private System.Windows.Forms.Button TextBoxCls;
    }
}

