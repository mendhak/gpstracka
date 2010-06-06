using System;

using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;

namespace GPSTracka
{
    public partial  class TrackerForm
    {
        /// <summary>Holds GPS statistic data</summary>
        private class GpsStatistics
        {
            public GpsStatistics(TrackerForm form, DateTime startTime, GpsPoint startPosition)
            {
                if (form == null)
                {
                    throw new ArgumentNullException("form");
                }

                StartTime = CurrentTime = startTime;
                startPos = CurrentPos = startPosition;
                this.form = form;
                ShowValues(ValueKind.All);
            }

            /// <summary>Owning form</summary>
            private TrackerForm form;

            private decimal actualSpeed;
            /// <summary>Actual speed in km/h</summary>
            public decimal ActualSpeed
            {
                get { return actualSpeed; }
                set
                {
                    actualSpeed = value;
                    ShowValues(ValueKind.ActualSpeed);
                }
            }

            /// <summary>Time when logging started</summary>
            public DateTime StartTime { get; set; }

            private DateTime currentTime;
            /// <summary>Current time</summary>
            public DateTime CurrentTime
            {
                get { return currentTime; }
                set
                {
                    currentTime = value;
                    ShowValues(ValueKind.Avg | ValueKind.Time);
                }
            }
            private TimeSpan pauseTime;
            /// <summary>Total pause time</summary>
            public TimeSpan PauseTime
            {
                get { return pauseTime; }
                set
                {
                    pauseTime = value;
                    ShowValues(ValueKind.StopTime);
                }
            }
            private GpsPoint startPos;
            /// <summary>Start position</summary>
            public GpsPoint StartPos { get { return startPos; } }
            private GpsPoint currentPos;
            /// <summary>Current position</summary>
            public GpsPoint CurrentPos
            {
                get { return currentPos; }
                set
                {
                    currentPos = value;
                    ShowValues(ValueKind.Aerial);
                }
            }
            private decimal sumLength;
            /// <summary>Total track length in km</summary>
            public decimal SumLength
            {
                get { return sumLength; }
                set
                {
                    sumLength = value;
                    ShowValues(ValueKind.Avg | ValueKind.Distance);
                }
            }
            private decimal sumElePlus;
            /// <summary>Positive elevation</summary>
            public decimal SumElePlus
            {
                get { return sumElePlus; }
                set
                {
                    sumElePlus = value;
                    ShowValues(ValueKind.ElePlus);
                }
            }
            private decimal sumEleMinus;
            /// <summary>Negative elevation</summary>
            public decimal SumEleMinus
            {
                get { return sumEleMinus; }
                set
                {
                    sumEleMinus = value;
                    ShowValues(ValueKind.EleMinus);
                }
            }
            private int pointsTotal;
            /// <summary>Total points count</summary>
            public int PointsTotal
            {
                get { return pointsTotal; }
                set
                {
                    pointsTotal = value;
                    ShowValues(ValueKind.Points);
                }
            }
            private int currentSats;
            /// <summary>Current number of satellites in view</summary>
            public int CurrentSats
            {
                get { return currentSats; }
                set
                {
                    currentSats = value;
                    ShowValues(ValueKind.Sats);
                }
            }
            /// <summary>Average speed in km/h</summary>
            public decimal AverageSpeed
            {
                get
                {
                    return (CurrentTime - StartTime - PauseTime == TimeSpan.Zero) ? 0 : SumLength / (decimal)(CurrentTime - StartTime - PauseTime).TotalHours;
                }
            }
            /// <summary>Gets aerial distance of start and current point in km.</summary>
            public decimal AerialDistance { get { return CurrentPos - StartPos; } }
            /// <summary>Gets time of moving (excluding pauses)</summary>
            public TimeSpan Time { get { return CurrentTime - StartTime - PauseTime; } }
            /// <summary>Shows values to the user</summary>
            /// <param name="vk">Which values to show</param>
            public void ShowValues(ValueKind vk)
            {
                if (!AdvancedConfig.InfoPane)
                {
                    return;
                }

                if (form == null)
                {
                    return;
                }

                if (form.InvokeRequired)
                {
                    form.BeginInvoke(new Action<ValueKind>(ShowValues), vk);
                    return;
                }

                if (!form.panInfoPane.Visible)
                {
                    return;
                }
                if ((vk & ValueKind.ActualSpeed) != 0)
                {
                    form.lblSpeed.Text = (ActualSpeed * AdvancedConfig.SpeedMultiplier).ToString("0.0") +
                                         AdvancedConfig.SpeedUnitName;
                }
                if ((vk & ValueKind.Avg) != 0)
                {
                    form.lblAverage.Text = (AverageSpeed * AdvancedConfig.SpeedMultiplier).ToString("0.0") +
                                           AdvancedConfig.SpeedUnitName;
                }
                if ((vk & ValueKind.Distance) != 0)
                {
                    form.lblDistance.Text = (SumLength * AdvancedConfig.DistanceMultiplier).ToString("0.000") +
                                            AdvancedConfig.DistanceUnitName;
                }
                if ((vk & ValueKind.Aerial) != 0)
                {
                    form.lblAerial.Text = (AerialDistance * AdvancedConfig.DistanceMultiplier).ToString("0.000") +
                                          AdvancedConfig.DistanceUnitName;
                }
                if ((vk & ValueKind.ElePlus) != 0)
                {
                    form.lblElevation.Text = (SumElePlus * AdvancedConfig.ElevationMultiplier).ToString("0") +
                                             AdvancedConfig.ElevationUnitName;
                }
                if ((vk & ValueKind.EleMinus) != 0)
                {
                    form.lblElevationMinus.Text = (SumEleMinus * AdvancedConfig.ElevationMultiplier).ToString("0") +
                                                  AdvancedConfig.ElevationUnitName;
                }
                if ((vk & ValueKind.Time) != 0)
                {
                    form.lblTime.Text = string.Format("{1:0}{0}{2:00}{0}{3:00}",
                                                      CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator,
                                                      Math.Floor(Time.TotalHours), Math.Abs(Time.Minutes),
                                                      Math.Abs(Time.Seconds));
                }
                if ((vk & ValueKind.StopTime) != 0)
                {
                    form.lblStopTime.Text = string.Format("{1:0}{0}{2:00}{0}{3:00}",
                                                          CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator,
                                                          Math.Floor(PauseTime.TotalHours), Math.Abs(PauseTime.Minutes),
                                                          Math.Abs(PauseTime.Seconds));
                }
                if ((vk & ValueKind.Points) != 0)
                {
                    form.lblPoints.Text = PointsTotal.ToString();
                }
                if ((vk & ValueKind.Sats) != 0)
                {
                    form.lblSatellites.Text = CurrentSats.ToString();
                }
            }
            /// <summary>Saves current statistics to file</summary>
            /// <param name="path">File name</param>
            public void Save(string path)
            {
                using (var file = System.IO.File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (var w = new System.IO.StreamWriter(file))
                    {
                        w.WriteLine(Properties.Resources.stat_StartTime, StartTime);
                        w.WriteLine(Properties.Resources.stat_AverageSpeed, AverageSpeed * AdvancedConfig.SpeedMultiplier, AdvancedConfig.SpeedUnitName);
                        w.WriteLine(Properties.Resources.stat_Distance, SumLength * AdvancedConfig.DistanceMultiplier, AdvancedConfig.DistanceUnitName);
                        w.WriteLine(Properties.Resources.stat_AerialDistance, AerialDistance * AdvancedConfig.ElevationMultiplier, AdvancedConfig.DistanceUnitName);
                        w.WriteLine(Properties.Resources.stat_PositiveElevation, SumElePlus * AdvancedConfig.ElevationMultiplier, AdvancedConfig.ElevationUnitName);
                        w.WriteLine(Properties.Resources.stat_NegativeElevation, SumEleMinus * AdvancedConfig.ElevationMultiplier, AdvancedConfig.ElevationUnitName);
                        w.WriteLine(Properties.Resources.stat_MoveTime, Time);
                        w.WriteLine(Properties.Resources.stat_StopTime, PauseTime);
                        w.WriteLine(Properties.Resources.stat_Points, PointsTotal);
                        w.WriteLine(Properties.Resources.stat_Saved, DateTime.Now);
                    }
                }
            }
            /// <summary>Shows all-zero values</summary>
            /// <param name="form">Form holding the data</param>
            public static void ClearAll(TrackerForm form)
            {
                if (!AdvancedConfig.InfoPane)
                {
                    return;
                }

                GpsStatistics st = new GpsStatistics(form, DateTime.Now, new GpsPoint(0, 0, 0));
                st.ShowValues(ValueKind.All);
            }
            /// <summary>Identifies values shown on form</summary>
            [Flags]
            public enum ValueKind
            {
                ActualSpeed = 1,
                Avg = 2,
                SumLength = 4,
                Distance = 8,
                Aerial = 16,
                ElePlus = 32,
                EleMinus = 64,
                Time = 128,
                StopTime = 256,
                Points = 512,
                Sats = 1024,
                All = 2047
            }
        }
    }
}
