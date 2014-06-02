/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    public delegate void LongEventHandler(object sender, LongEventArgs e);

    public class LongEventArgs : EventArgs
    {
        private long selected;

        public long Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }

        public LongEventArgs(long number)
        {
            this.selected =  number;
        }
    }
}
