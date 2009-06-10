using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GPSTracka.Gps;

namespace GPSTracka {
    /// <summary>View state of sattelites</summary>
    public partial class SattelitesView : Form {
        /// <summary>GPS provider</summary>
        private GpsProvider gps;
         /// <summary>CTor</summary>
         /// <param name="gps">GPS provider</param>
         public SattelitesView(GpsProvider gps) {
            if (gps == null) throw new ArgumentNullException("gps");
            this.gps = gps;
            InitializeComponent();
            gps.Sattelite +=gps_Satellite;
        }                                  

        private void mniClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void SattelitesView_Closed(object sender, EventArgs e) {
            gps.Sattelite -=  gps_Satellite;
            gps = null;
        }
        /// <summary>When last position was got</summary>
        private DateTime LastGPSSattelite = DateTime.MinValue;
        private void gps_Satellite(GpsProvider sender,GpsSatteliteEventArgs e){
            if (this.InvokeRequired) {
                if ((DateTime.Now - LastGPSSattelite).TotalMilliseconds < 200) return;
                this.BeginInvoke(new GpsSatteliteEventHandler(gps_Satellite), sender, e);
                return;
            }
            List<GpsSattelite> satl = new List<GpsSattelite>();
            foreach(GpsSattelite sat in e.Sattelites) if(sat.ID!=0) satl.Add(sat);
            if(lblID == null || lblID.Length != satl.Count) CreateLabels(satl.Count);
            int i = 0;
            lvwSattelites.Items.Clear();
            foreach (var sat in satl) {
                lblID[i].Text = sat.ID.ToString();
                lblSNR[i].Tag = sat.SignalToNoiseRatio;
                lblSNR[i].BackColor = GetColorFromSNR(sat.SignalToNoiseRatio, sat.Active);
                lblSNR[i].ForeColor = Color.FromArgb((byte)~lblSNR[i].BackColor.R, (byte)~lblSNR[i].BackColor.G, (byte)~lblSNR[i].BackColor.B);

                ListViewItem itm = new ListViewItem(new string[]{
                    sat.ID.ToString(),
                    sat.SignalToNoiseRatio.ToString(),
                    sat.Active.ToString(),
                    sat.Azimuth.ToString(),
                    sat.Elevation.ToString()/*,
                    sat.Channel.ToString()*/
                });
                lvwSattelites.Items.Add(itm);

                ++i; 
            }
            PosLabels();
            this.sattelites = satl;
            panPosition.Invalidate();
        }
        /// <summary>Gets color representation of satellite SNR</summary>
        /// <param name="SNR">Signal to noise ration</param>
        /// <param name="active">Is satellite used by device?</param>
        /// <returns>Color representation of satellite state.</returns>
        private Color GetColorFromSNR(int SNR, bool active) {
            if (SNR == 0) return Color.Black;
            if (!active || SNR < 10) return Color.Red;
            if (SNR < 37) return Color.Yellow;
            return Color.FromArgb(0, 255, 0);
        }
        /// <summary>Sattelite IDs</summary>
        private Label[] lblID;
        /// <summary>Sattelite SNRs</summary>
        private Label[] lblSNR;
        /// <summary>Creates labels representing sattelites signal strength</summary>
        private void CreateLabels(int count) {
            panSattelites.Controls.Clear();
            lblID = new Label[count];
            lblSNR = new Label[count];
            for (int i = 0; i < count; i++) {
                panSattelites.Controls.Add(lblID[i] = new Label() { TextAlign = ContentAlignment.TopCenter, Text = "" });
                panSattelites.Controls.Add(lblSNR[i] = new Label() {Tag=0, Text="" });
            }
            PosLabels();
        }
        /// <summary>Places and sizes labels representing sattelites signal streangth</summary>
        private void PosLabels() {
            if (lblID == null) return;
            for (int i = 0; i < lblID.Length; i++) {
                lblID[i].Left = lblSNR[i].Left = (panSattelites.ClientSize.Width / lblID.Length) * i;
                lblSNR[i].Width = lblID[i].Width = (panSattelites.ClientSize.Width / lblID.Length)-1;
                lblID[i].Height = 32;
                lblID[i].Top = panSattelites.ClientRectangle.Bottom - lblID[i].Height;
                lblSNR[i].Height = (int)(((float)((panSattelites.ClientSize.Height - 3 - 10 - lblID[i].Height) * (int)lblSNR[i].Tag)) / 100) + 10;
                lblSNR[i].Top = lblID[i].Top - lblSNR[i].Height - 3;
            }
        }

        private void panSattelites_Resize(object sender, EventArgs e) {
            PosLabels();
        }

        private void mniView_Click(object sender, EventArgs e) {
            if (tabSattelites.SelectedIndex == tabSattelites.TabPages.Count - 1) tabSattelites.SelectedIndex = 0;
            else tabSattelites.SelectedIndex++;
        }
        /// <summary>The sattelites</summary>
        private List<GpsSattelite> sattelites;
        private void panPosition_Paint(object sender, PaintEventArgs e) {
            Rectangle rect = panPosition.ClientRectangle.Width > panPosition.ClientRectangle.Height ?
                new Rectangle(
                    (panPosition.ClientRectangle.Left + panPosition.ClientRectangle.Right) / 2 - panPosition.ClientRectangle.Height / 2,
                    panPosition.ClientRectangle.Top, panPosition.ClientRectangle.Height, panPosition.ClientRectangle.Height) :
               new Rectangle(panPosition.ClientRectangle.Left,
                   (panPosition.ClientRectangle.Top + panPosition.ClientRectangle.Bottom) / 2 - panPosition.ClientRectangle.Width / 2,
                   panPosition.ClientRectangle.Width, panPosition.ClientRectangle.Width);
            e.Graphics.DrawEllipse(new Pen(Color.Black), rect);
            if(sattelites==null)return;
            foreach(var sat in sattelites){
                float elevationPx = (90 - (float)sat.Elevation) / 90 * rect.Width / 2;
                float azimuthRad = (float)sat.Azimuth * (float)Math.PI / 180;
                float xPx = elevationPx * (float)Math.Sin(azimuthRad);
                float yPx = elevationPx * (float)Math.Cos(azimuthRad);
                int x = rect.Left + rect.Width / 2 + (int)xPx;
                int y = rect.Top + rect.Height / 2 - (int)yPx;
                int radius = 10 + 20 * sat.SignalToNoiseRatio / 100;
                if(sat.SignalToNoiseRatio==0)
                    e.Graphics.DrawEllipse(new Pen(Color.Black),
                        x - radius, y - radius, 2*radius, 2*radius);
                else
                    e.Graphics.FillEllipse(new SolidBrush(GetColorFromSNR(sat.SignalToNoiseRatio,sat.Active)),
                        x - radius, y - radius, 2*radius, 2*radius);
                e.Graphics.DrawString(sat.ID.ToString(), this.Font, new SolidBrush(Color.Black),
                    new Rectangle(x - radius, y - radius, 2*radius, 2*radius), 
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap });
            }
        }


    }
}