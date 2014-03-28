/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    public delegate void IntegerEventHandler(object sender, IntegerEventArgs e);

    public class IntegerEventArgs : EventArgs
    {
        private Int32 selectedInt;

        public Int32 SelectedInt
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

        public IntegerEventArgs(Int32 number)
        {
            this.selectedInt =  number;
        }
    }
}
