using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GPSTracka
{
    public partial class GroupBox : UserControl
    {
        public GroupBox()
        {
            InitializeComponent();
        }

        public string Title
        {
            get
            {
                return Label1.Text;
            }
            set
            {
                Label1.Text = value;
            }
        }

        private void Label1_TextChanged(object sender, EventArgs e)
        {
            using (Graphics gfx = this.CreateGraphics())
            {
                SizeF size = gfx.MeasureString(Label1.Text, Label1.Font);

                if (gfx.DpiX == 96)
                {
                    Label1.Width = Convert.ToInt32(size.Width + 4);
                    Label1.Height = Convert.ToInt32(size.Height);
                }
                else
                {
                    Label1.Width = Convert.ToInt32(size.Width / 2 + 4);
                    Label1.Height = Convert.ToInt32(size.Height / 2 + 1);
                }
            }
        }

        private void GroupBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Int32 x1 = 0;
            Int32 x2 = ClientRectangle.Width;
            Int32 y1 = Label1.Location.Y + (Label1.Size.Height / 2);
            Int32 y2 = ClientRectangle.Height;

            if (g.DpiX == 96)
            {
                using (Pen drawingPen = new Pen(Color.Black, 1))
                {
                    g.DrawLine(drawingPen, x1, y1, x2, y1);          // Draw the top line of the group box
                    g.DrawLine(drawingPen, x1, y1, x1, y2);          // Draw the left line of the group box
                    g.DrawLine(drawingPen, x1, y2 - 1, x2, y2 - 1);  // Draw the bottom line of the group box
                    g.DrawLine(drawingPen, x2 - 1, y1, x2 - 1, y2);  // Draw the right line of the group box
                }
            }
            else
            {
                using (Pen drawingPen = new Pen(Color.Black, 2))
                {
                    g.DrawLine(drawingPen, x1, y1, x2, y1);          // Draw the top line of the group box
                    g.DrawLine(drawingPen, x1 + 1, y1, x1 + 1, y2);  // Draw the left line of the group box
                    g.DrawLine(drawingPen, x1, y2 - 1, x2, y2 - 1);  // Draw the bottom line of the group box
                    g.DrawLine(drawingPen, x2 - 1, y1, x2 - 1, y2);  // Draw the right line of the group box
                }
            }

        }
    }
}
