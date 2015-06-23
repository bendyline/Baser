/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    public delegate void Int64EventHandler(object sender, Int64EventArgs e);

    public class Int64EventArgs : EventArgs
    {
        private Int64 selectedInt;

        public Int64 SelectedInt
        {
            get
            {
                return this.selectedInt;
            }
            set
            {
                this.selectedInt = value;
            }
        }

        public Int64EventArgs(Int64 number)
        {
            this.selectedInt =  number;
        }
    }
}
