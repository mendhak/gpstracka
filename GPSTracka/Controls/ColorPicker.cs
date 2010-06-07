using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GPSTracka
{
    /// <summary>Control to select color</summary>
    public partial class ColorPicker : UserControl
    {
        /// <summary>CTor</summary>
        public ColorPicker()
        {
            InitializeComponent();
            lblPrompt.Text = this.Text;
            Color = Color.White;
        }
        /// <summary>Raises the <see cref="System.Windows.Forms.Control.TextChanged"/> event.</summary>
        /// <param name="e">An <see cref="System.EventArgs"/> that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (lblPrompt != null)
                lblPrompt.Text = this.Text;
            base.OnTextChanged(e);
        }


        private void nud_ValueChanged(object sender, EventArgs e)
        {
            panColor.BackColor = Color.FromArgb((int)nudR.Value, (int)nudG.Value, (int)nudB.Value);
            if (suspendColorChanged) return;
            OnColorChanged(e);
        }

        private void panColor_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            if (panel.BackColor != Color)
            {
                suspendColorChanged = true;
                try
                {
                    nudR.Value = panel.BackColor.R;
                    nudG.Value = panel.BackColor.G;
                    nudB.Value = panel.BackColor.B;
                }
                finally
                {
                    suspendColorChanged = false;
                }
                OnColorChanged(EventArgs.Empty);
            }
        }
        /// <summary>When true <see cref="OnColorChanged"/> is not called when value of <see cref="NumericUpDown"/> changes</summary>
        private bool suspendColorChanged = false;
        /// <summary>Raises the <see cref="ColorChanged"/> event</summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnColorChanged(EventArgs e)
        {
            if (ColorChanged != null) ColorChanged(this, e);
        }
        /// <summary>Raised when value of the <see cref="Color"/> property changes</summary>
        public event EventHandler ColorChanged;
        /// <summary>Gets or sets color selected in control</summary>
        public Color Color
        {
            get { return panColor.BackColor; }
            set
            {
                if (value != Color)
                {
                    suspendColorChanged = true;
                    try
                    {
                        nudR.Value = value.R;
                        nudG.Value = value.G;
                        nudB.Value = value.B;
                    }
                    finally
                    {
                        suspendColorChanged = false;
                    }
                    OnColorChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>Gets value indicatiing wheather value of the <see cref="Color"/> property differes form its default value and thus should be serialized</summary>
        /// <returns>True when <see cref="Color"/> differs from <see cref="System.Drawing.Color.White"/></returns>
        /// <remarks>Athought private, used by PropertyGrid</remarks>
        private bool ShouldSerializeColor() { return Color != Color.White; }
        /// <summary>Resets value of the <see cref="Color"/> property to its default value (<see cref="System.Drawing.Color.White"/>)</summary>
        /// <remarks>Athought private, used by PropertyGrid</remarks>
        private void ResetColor() { Color = Color.White; }
    }
}
