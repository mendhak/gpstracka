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
            this.ButtonStartStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.TextBoxRawLog = new System.Windows.Forms.TextBox();
            this.NumericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.ComboBoxCOMPorts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CheckBoxToFile = new System.Windows.Forms.CheckBox();
            this.CheckBoxToTextBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ButtonLogAll = new System.Windows.Forms.Button();
            this.TextBoxCls = new System.Windows.Forms.Button();
            this.ComboBaudRate = new System.Windows.Forms.ComboBox();
            this.LabelBaudRate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            this.TextBoxRawLog.Size = new System.Drawing.Size(234, 175);
            this.TextBoxRawLog.TabIndex = 1;
            // 
            // NumericUpDownInterval
            // 
            this.NumericUpDownInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.NumericUpDownInterval.Location = new System.Drawing.Point(40, 29);
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
            this.ComboBoxCOMPorts.Location = new System.Drawing.Point(40, 2);
            this.ComboBoxCOMPorts.Name = "ComboBoxCOMPorts";
            this.ComboBoxCOMPorts.Size = new System.Drawing.Size(55, 22);
            this.ComboBoxCOMPorts.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(132, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.Text = "seconds";
            // 
            // CheckBoxToFile
            // 
            this.CheckBoxToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxToFile.Location = new System.Drawing.Point(160, 58);
            this.CheckBoxToFile.Name = "CheckBoxToFile";
            this.CheckBoxToFile.Size = new System.Drawing.Size(71, 20);
            this.CheckBoxToFile.TabIndex = 5;
            this.CheckBoxToFile.Text = "To File";
            // 
            // CheckBoxToTextBox
            // 
            this.CheckBoxToTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxToTextBox.Checked = true;
            this.CheckBoxToTextBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxToTextBox.Location = new System.Drawing.Point(54, 57);
            this.CheckBoxToTextBox.Name = "CheckBoxToTextBox";
            this.CheckBoxToTextBox.Size = new System.Drawing.Size(100, 20);
            this.CheckBoxToTextBox.TabIndex = 6;
            this.CheckBoxToTextBox.Text = "To Textbox";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.Text = "every";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 20);
            this.label4.Text = "Read";
            // 
            // ButtonLogAll
            // 
            this.ButtonLogAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonLogAll.Location = new System.Drawing.Point(82, 3);
            this.ButtonLogAll.Name = "ButtonLogAll";
            this.ButtonLogAll.Size = new System.Drawing.Size(90, 20);
            this.ButtonLogAll.TabIndex = 10;
            this.ButtonLogAll.Text = "Verbose";
            this.ButtonLogAll.Click += new System.EventHandler(this.ButtonLogAll_Click);
            // 
            // TextBoxCls
            // 
            this.TextBoxCls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxCls.Location = new System.Drawing.Point(179, 3);
            this.TextBoxCls.Name = "TextBoxCls";
            this.TextBoxCls.Size = new System.Drawing.Size(58, 20);
            this.TextBoxCls.TabIndex = 15;
            this.TextBoxCls.Text = "Cls";
            this.TextBoxCls.Click += new System.EventHandler(this.TextBoxCls_Click);
            // 
            // ComboBaudRate
            // 
            this.ComboBaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBaudRate.Items.Add("4800");
            this.ComboBaudRate.Items.Add("9600");
            this.ComboBaudRate.Items.Add("19200");
            this.ComboBaudRate.Items.Add("38400");
            this.ComboBaudRate.Items.Add("57600");
            this.ComboBaudRate.Items.Add("115200");
            this.ComboBaudRate.Location = new System.Drawing.Point(124, 2);
            this.ComboBaudRate.Name = "ComboBaudRate";
            this.ComboBaudRate.Size = new System.Drawing.Size(75, 22);
            this.ComboBaudRate.TabIndex = 20;
            // 
            // LabelBaudRate
            // 
            this.LabelBaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelBaudRate.Location = new System.Drawing.Point(101, 4);
            this.LabelBaudRate.Name = "LabelBaudRate";
            this.LabelBaudRate.Size = new System.Drawing.Size(17, 20);
            this.LabelBaudRate.Text = "at";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.LabelBaudRate);
            this.panel1.Controls.Add(this.ComboBaudRate);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.CheckBoxToTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.CheckBoxToFile);
            this.panel1.Controls.Add(this.ComboBoxCOMPorts);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.NumericUpDownInterval);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 213);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 81);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(205, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 20);
            this.label5.Text = "baud";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.Text = "and log";
            // 
            // GPSTracka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextBoxCls);
            this.Controls.Add(this.ButtonLogAll);
            this.Controls.Add(this.TextBoxRawLog);
            this.Controls.Add(this.ButtonStartStop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GPSTracka";
            this.Text = "GPSTracka";
            this.Load += new System.EventHandler(this.GPSTracka_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.GPSTracka_Closing);
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ButtonLogAll;
        private System.Windows.Forms.Button TextBoxCls;
        private System.Windows.Forms.ComboBox ComboBaudRate;
        private System.Windows.Forms.Label LabelBaudRate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
    }
}

