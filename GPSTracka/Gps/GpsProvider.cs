using System;   
using System.Collections.Generic;
using System.Text;
using GPSTracka;

namespace GPSTracka.Gps
{
    /// <summary>Base class for Gps driver providers</summary>
    public abstract class GpsProvider :IDisposable 
    {
        /// <summary>When overriden in derived class starts GPS polling</summary>
        public abstract void Start();
        /// <summary>When overriden in derived class stops GPS polling</summary>
        public abstract void Stop();
        /// <summary>Raised when event occurs</summary>
        public event GpsErrorEventHandler Error;
        /// <summary>Raises the <see cref="Error"/> event</summary>
        /// <param name="message">Error message</param>
        /// <param name="ex">Exception itself. May be null.</param>
        protected virtual void OnError(string message, Exception ex)
        {
            if (Error != null) Error(this, message, ex);
        }
        /// <summary>Raised when GPS satellite information is received</summary>
        public event GpsSatelliteEventHandler Satellite;
        /// <summary>Raises the <see cref="Satellite"/> event</summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnSatellite(GpsSatelliteEventArgs e)
        {
            if (Satellite != null) Satellite(this, e);
        }
        /// <summary>Raised when GPS position information is received</summary>
        public event GpsPositionEventHandler Position;
        /// <summary>Raises the <see cref="Position"/> event</summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnPosition(GpsPositionEventArgs e)
        {
            if (Position != null) Position(this, e);
        }
        /// <summary>Raised when GPS state changes</summary>
        public event GpsStateEventHandler StateChanged;
        /// <summary>Raises the <see cref="StateChanged"/> event</summary>
        /// <param name="state">A new state</param>
        protected virtual void OnStateChanged(GpsState state)
        {
            if (StateChanged != null) StateChanged(this, state);
        }
        /// <summary>Raised whenever raw GPS sentence is received</summary>
        /// <remarks>Derived class may or may not raise this event depending on if it has access to raw GPS data</remarks>
        public event GpsSentenceEventHandler GpsSentence;
        /// <summary>Raises the <see cref="GpsSentence"/> event</summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnGpsSentence(GpsSentenceEventArgs e)
        {
            if (GpsSentence != null) GpsSentence(this, e);
        }
        /// <summary>When overriden in derived class gets current GPS state</summary>
        public abstract GpsState State { get; }
        /// <summary>Raised when GPS movement data are received</summary>
        public event MovementEventHandler Movement;
        /// <summary>Raises the <see cref="Movement"/> event</summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnMovement(GpsMovementEventArgs e)
        {
            if (Movement != null) Movement.Invoke(this, e);
        }

        /// <summary>Disposes the object</summary>
        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                if (State != GpsState.Closed) Stop();
                Movement = null;
                GpsSentence = null;
                StateChanged = null;
                Position = null;
                Satellite = null;
                Error = null;
            }
            IsDisposed = true;
        }
        /// <summary>Gets or sets value indicating if this instance is disposed</summary>
        public bool IsDisposed { get; private set; }
    }

#region Event delegates
    /// <summary>Event handler for the <see cref="GpsProvider.Error"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="message">Error message</param>
    /// <param name="ex">Exception itself. May be null.</param>
    public delegate void GpsErrorEventHandler(GpsProvider sender, string message, Exception ex);

    /// <summary>Event handler for the <see cref="GpsProvider.Satellite"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">Event arguments</param>
    public delegate void GpsSatelliteEventHandler(GpsProvider sender, GpsSatelliteEventArgs e);

    /// <summary>Event handler for the <see cref="GpsProvider.Position"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">Event arguments</param>
    public delegate void GpsPositionEventHandler(GpsProvider sender, GpsPositionEventArgs e);

    /// <summary>Event handler for the <see cref="GpsProvider.StateChanged"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="state">A new state</param>
    public delegate void GpsStateEventHandler(GpsProvider sender, GpsState state);

    /// <summary>Event handler for the <see cref="GpsProvider.GpsSentence"/> event</summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">Event arguments</param>
    public delegate void GpsSentenceEventHandler(GpsProvider sender, GpsSentenceEventArgs e);

    /// <summary>Delegate for the <see cref="GpsProvider.Movement"/> event</summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">Event arguments</param>
    public delegate void MovementEventHandler(GpsProvider sender, GpsMovementEventArgs e);

#endregion
#region EventArgs
    /// <summary>Event arguments of the <see cref="GpsProvider.Satellite"/> event</summary>
    public class GpsSatelliteEventArgs
    {
        /// <summary>Contains value of the <see cref="Satellites"/> property</summary>
        private GpsSatellite[] satellites;
        /// <summary>Gets information about GPS satellites</summary>
        public GpsSatellite[] Satellites
        {
            get
            {
                return satellites;
            }
        }
        /// <summary>CTor</summary>
        /// <param name="satellites">Information about GPS satellites</param>
        /// <exception cref="ArgumentNullException"><paramref name="satellites"/> is null</exception>
        public GpsSatelliteEventArgs(params GpsSatellite[] satellites)
        {
            if (satellites == null) throw new ArgumentNullException("satellites");
            this.satellites = satellites;
        }
    }

    /// <summary>Arguments of the <see cref="GpsProvider.Position"/> event</summary>
    public class GpsPositionEventArgs
    {
        /// <summary>Contains value of the <see cref="Position"/> property</summary>
        private GpsPosition position;
        /// <summary>CTor</summary>
        /// <param name="position">Current GPS posotion</param>
        public GpsPositionEventArgs(GpsPosition position) { this.position = position; }
        /// <summary>Gets current GPS position infromation</summary>
        public GpsPosition Position
        {
            get { return position; }
        }
    }

    /// <summary>Arguments of the <see cref="GpsProvider.GpsSentence"/> event</summary>
    public class GpsSentenceEventArgs
    {
        /// <summary>CTor</summary>
        /// <param name="sentence">GPS sentence string</param>
        /// <param name="counter">GPS sentence counter</param>
        public GpsSentenceEventArgs(int counter, string sentence) { this.counter = counter; this.sentence = sentence; }
        /// <summary>Contains value of the sentence property</summary>
        private string sentence;
        /// <summary>Contains value of the <see cref="Counter"/> property</summary>
        private int counter;
        /// <summary>Gets GPS sentence counter value</summary>
        public int Counter { get { return counter; } }
        /// <summary>Gets raw GPS sentence data</summary>
        public string Sentence
        {
            get { return sentence; }
        }
    }

    /// <summary>Gps movement event arguments</summary>
    public class GpsMovementEventArgs : EventArgs
    {
        /// <summary>Movement speed (in km/h)</summary>
        public decimal Speed { get; set; }
        /// <summary>CTor</summary>
        /// <param name="speed">Speed in km/h</param>
        public GpsMovementEventArgs(decimal speed)
        {
            Speed = speed;
        }
    }
#endregion

    /// <summary>States of GPS device</summary>
    public enum GpsState
    {
        /// <summary>Device is closed - not running, not raising events</summary>
        Closed,
        /// <summary>Device is opened - running, raising events</summary>
        Open,
        /// <summary>Device is opening - preparing to run</summary>
        Opening,
        /// <summary>Device is being closed (stopping)</summary>
        Closing
    }
}