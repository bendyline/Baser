using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bendyline.Base
{
    public class Timer
    {
        private DispatcherTimer timer;
        private bool allowMultipleOutstandingTicks = false;
        private bool isInTick = false;
        public event EventHandler Tick;

        public bool AllowMultipleOutstandingTicks
        {
            get
            {
                return this.allowMultipleOutstandingTicks;
            }
            set
            {
                this.allowMultipleOutstandingTicks = value;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.timer.IsEnabled;
            }
        }

        public TimeSpan Interval
        {
            get
            {
                return this.timer.Interval;
            }
            set
            {
                this.timer.Interval = value;
            }
        }

        public Timer()
        {
            this.timer = new DispatcherTimer();

            this.timer.Tick += new EventHandler(timer_Tick);
        }

        public void timer_Tick(object sender, EventArgs e)
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
