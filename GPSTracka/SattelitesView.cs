using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GPSTracka.Gps;

namespace GPSTracka
{
    /// <summary>View state of satellites</summary>
    public partial class SatellitesView : Form
    {
        /// <summary>GPS provider</summary>
        private GpsProvider gps;

        /// <summary>When last position was got</summary>
        private DateTime lastGpsSatellite = DateTime.MinValue;

        /// <summary>The satellites</summary>
        private List<GpsSatellite> satellites;

        /// <summary>Satellite IDs</summary>
        private Label[] lblId;

        /// <summary>Satellite SNRs</summary>
        private Label[] lblSnr;

        /// <summary>CTor</summary>
        /// <param name="gps">GPS provider</param>
        public SatellitesView(GpsProvider gps)
        {
            if (gps == null)
            {
                throw new ArgumentNullException("gps");
            }

            this.gps = gps;
            this.gps.Start();
            InitializeComponent();
            gps.Satellite += GpsSatellite;
        }

        /// <summary>Gets value indicating if this object has already been disposed</summary>
        public bool IsDisposed { get; private set; }

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
            IsDisposed = true;
        }

        private void mniClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SatellitesView_Closed(object sender, EventArgs e)
        {
            if(gps != null)
            {
                gps.Satellite -= GpsSatellite;
                gps = null;
            }
  
        }

        private void GpsSatellite(GpsProvider sender, GpsSatelliteEventArgs e)
        {
            //Don't want the form to lock up if nothing's happening.
            System.Threading.Thread.Sleep(200);
            if (this.IsDisposed) return;
            if (InvokeRequired)
            {
                if ((DateTime.Now - lastGpsSatellite).TotalMilliseconds < 200)
                {
                    return;
                }

                BeginInvoke(new GpsSatelliteEventHandler(GpsSatellite), sender, e);
                return;
            }

            lastGpsSatellite = DateTime.Now;

            List<GpsSatellite> satl = new List<GpsSatellite>();
            foreach (GpsSatellite sat in e.Satellites)
            {
                if (sat.ID != 0)
                {
                    satl.Add(sat);
                }
            }

            if ((lblId == null) || (lblId.Length != satl.Count))
            {
                CreateLabels(satl.Count);
            }

            int i = 0;
            lvwSatellites.Items.Clear();
            foreach (var sat in satl)
            {
                lblId[i].Text = sat.ID.ToString();
                lblSnr[i].Tag = sat.SignalToNoiseRatio;
                lblSnr[i].Text = sat.SignalToNoiseRatio.ToString();
                lblSnr[i].BackColor = GetColorFromSnr(sat.SignalToNoiseRatio, sat.Active);
                lblSnr[i].ForeColor = Color.FromArgb((byte)~lblSnr[i].BackColor.R, (byte)~lblSnr[i].BackColor.G, (byte)~lblSnr[i].BackColor.B);

                ListViewItem itm = new ListViewItem(new[]{
                    sat.ID.ToString(),
                    sat.SignalToNoiseRatio.ToString(),
                    sat.Active.ToString(),
                    sat.Azimuth.ToString(),
                    sat.Elevation.ToString()
                });
                lvwSatellites.Items.Add(itm);

                ++i;
            }

            PosLabels();
            satellites = satl;
            panPosition.Invalidate();
        }

        /// <summary>Gets color representation of satellite snr</summary>
        /// <param name="snr">Signal to noise ration</param>
        /// <param name="active">Is satellite used by device?</param>
        /// <returns>Color representation of satellite state.</returns>
        private static Color GetColorFromSnr(int snr, bool active)
        {
            if (snr == 0) return Color.Black;
            if (!active || snr < 10) return Color.Red;
            if (snr <= 20) return Color.Yellow;
            return Color.FromArgb(0, 255, 0);
        }

        /// <summary>Creates labels representing satellites signal strength</summary>
        private void CreateLabels(int count)
        {
            panSatellites.Controls.Clear();
            lblId = new Label[count];
            lblSnr = new Label[count];
            for (int i = 0; i < count; i++)
            {
                panSatellites.Controls.Add(lblId[i] = new Label { TextAlign = ContentAlignment.TopCenter, Text = "" });
                panSatellites.Controls.Add(lblSnr[i] = new Label { Tag = 0, Text = "", TextAlign = ContentAlignment.TopCenter });
            }
            PosLabels();
        }

        /// <summary>Places and sizes labels representing satellites signal streangth</summary>
        private void PosLabels()
        {
            if (lblId == null) return;
            for (int i = 0; i < lblId.Length; i++)
            {
                lblId[i].Left = lblSnr[i].Left = (panSatellites.ClientSize.Width / lblId.Length) * i;
                lblSnr[i].Width = lblId[i].Width = (panSatellites.ClientSize.Width / lblId.Length) - 1;
                lblId[i].Height = 32;
                lblId[i].Top = panSatellites.ClientRectangle.Bottom - lblId[i].Height;
                lblSnr[i].Height = (int)(((float)((panSatellites.ClientSize.Height - 3 - 10 - lblId[i].Height) * (int)lblSnr[i].Tag)) / 100) + 10;
                lblSnr[i].Top = lblId[i].Top - lblSnr[i].Height - 3;
            }
        }

        private void panSatellites_Resize(object sender, EventArgs e)
        {
            PosLabels();
        }

        private void mniView_Click(object sender, EventArgs e)
        {
            if (tabSatellites.SelectedIndex == tabSatellites.TabPages.Count - 1)
            {
                tabSatellites.SelectedIndex = 0;
            }
            else
            {
                tabSatellites.SelectedIndex++;
            }
        }

        private void panPosition_Paint(object sender, PaintEventArgs e)
        {
            //Paint the big circle
            Rectangle rect;

            if (panPosition.ClientRectangle.Width > panPosition.ClientRectangle.Height)
            {
                rect = new Rectangle(
                    (panPosition.ClientRectangle.Left + panPosition.ClientRectangle.Right) / 2 -
                    panPosition.ClientRectangle.Height / 2,
                    panPosition.ClientRectangle.Top, panPosition.ClientRectangle.Height,
                    panPosition.ClientRectangle.Height);
            }
            else
            {
                rect = new Rectangle(panPosition.ClientRectangle.Left,
                                     (panPosition.ClientRectangle.Top + panPosition.ClientRectangle.Bottom) / 2 -
                                     panPosition.ClientRectangle.Width / 2,
                                     panPosition.ClientRectangle.Width, panPosition.ClientRectangle.Width);
            }

            e.Graphics.DrawEllipse(new Pen(Color.Black), rect);

            if (satellites == null)
            {
                return;
            }

            //Small circles for satellites
            foreach (var sat in satellites)
            {
                const int minRadius = 10;
                const int maxRadius = 25;
                float elevationPx = (90 - (float)sat.Elevation) / 90 * rect.Width / 2;
                float azimuthRad = (float)sat.Azimuth * (float)Math.PI / 180;
                float xPx = elevationPx * (float)Math.Sin(azimuthRad);
                float yPx = elevationPx * (float)Math.Cos(azimuthRad);
                int x = rect.Left + rect.Width / 2 + (int)xPx;
                int y = rect.Top + rect.Height / 2 - (int)yPx;
                int radius = minRadius + (int)((float)maxRadius * ((float)sat.SignalToNoiseRatio / (float)100));
                if (sat.SignalToNoiseRatio == 0)
                {
                    e.Graphics.DrawEllipse(new Pen(Color.Black),
                                           x - radius, y - radius, 2 * radius, 2 * radius);
                }
                else
                {
                    e.Graphics.FillEllipse(new SolidBrush(GetColorFromSnr(sat.SignalToNoiseRatio, sat.Active)),
                                           x - radius, y - radius, 2 * radius, 2 * radius);
                }
                e.Graphics.DrawString(sat.ID.ToString(), Font, new SolidBrush(Color.Black),
                    new Rectangle(x - radius, y - radius, 2 * radius, 2 * radius),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap });
            }
        }
    }
}