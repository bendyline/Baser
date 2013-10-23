/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
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
