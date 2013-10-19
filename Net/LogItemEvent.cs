using System;
using System.Net;

namespace Bendyline.Base
{
    public delegate void LogItemEventHandler(object sender, LogItemEventArgs e);

    public class LogItemEventArgs : EventArgs
    {
        private LogItem item;

        public LogItem Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        public LogItemEventArgs(LogItem item)
        {
            this.item = item;
        }
    }
}
