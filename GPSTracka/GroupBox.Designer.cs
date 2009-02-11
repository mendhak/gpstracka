namespace GPSTracka
{
    partial class GroupBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label Label1;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(13, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(45, 15);
            this.Label1.Text = "Label1";
            this.Label1.TextChanged += new System.EventHandler(this.Label1_TextChanged);
            // 
            // GroupBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.Label1);
            this.Name = "GroupBox";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GroupBox_Paint);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
