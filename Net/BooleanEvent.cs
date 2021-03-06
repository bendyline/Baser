﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace Bendyline.Base
{
    public delegate void BooleanEventHandler(object sender, BooleanEventArgs e);

    public class BooleanEventArgs : EventArgs
    {
        private bool boolValue;

        public bool Value
        {
            get
            {
                return this.boolValue;
            }
            set
            {
                this.boolValue = value;
            }
        }

        public BooleanEventArgs(bool boolValue)
        {
            this.boolValue = boolValue;
        }
    }
}
