/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Timers;

namespace Bendyline.Base
{
    public class Timer
    {
        private System.Timers.Timer timer;
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
            get { return this.timer.Enabled; }
        }

        public TimeSpan Interval
        {
            get { return new TimeSpan(0,0,0,0, Convert.ToInt32(this.timer.Interval)); }
            set { this.timer.Interval = value.TotalMilliseconds; }
        }

        public Timer()
        {
            this.timer = new System.Timers.Timer();


            this.timer.Elapsed += new ElapsedEventHandler(timer_Tick);
        }

        public void timer_Tick(object sender, ElapsedEventArgs e)
        {
            if (this.isInTick && !allowMultipleOutstandingTicks)
            {
                return;
            }

            this.isInTick = true;

            if (this.Tick != null)
            {
                this.Tick(this, e);
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
