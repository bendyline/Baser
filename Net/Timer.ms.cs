﻿using System;

namespace Bendyline.Base
{
    public class Timer
    {
        private Windows.UI.Xaml.DispatcherTimer timer;
        private bool allowMultipleOutstandingTicks = false;
        private bool isInTick = false;
        public event EventHandler Tick;

        public bool AllowMultipleOutstandingTicks
        {
            get { return this.allowMultipleOutstandingTicks; }
            set { this.allowMultipleOutstandingTicks = value; }
        }

        public bool IsEnabled
        {
            get { return this.timer.IsEnabled; }
        }

        public TimeSpan Interval
        {
            get { return this.timer.Interval; }
            set { this.timer.Interval = value; }
        }

        public Timer()
        {
            this.timer = new Windows.UI.Xaml.DispatcherTimer();


            this.timer.Tick += new Windows.UI.Xaml.EventHandler(timer_Tick);
        }

        public void timer_Tick(object sender, object e)
        {
            if (this.isInTick && !allowMultipleOutstandingTicks)
            {
                return;
            }

            this.isInTick = true;

            if (this.Tick != null)
            {
                this.Tick(this, EventArgs.Empty);
            }

            this.isInTick = false;
        }

        public void Start()
        {
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }
    }
}
