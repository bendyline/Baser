/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    public delegate void StringEventHandler(object sender, StringEventArgs e);

    public class StringEventArgs : EventArgs
    {
        private String stringValue;

        public String Value
        {
            get
            {
                return this.stringValue;
            }
            set
            {
                this.stringValue = value;
            }
        }

        public StringEventArgs(String stringValue)
        {
            this.stringValue = stringValue;
        }
    }
}
