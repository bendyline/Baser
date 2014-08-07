/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    public delegate void ObjectEventHandler(object sender, ObjectEventArgs e);

    public class ObjectEventArgs : EventArgs
    {
        private object objectValue;

        public object Value
        {
            get
            {
                return this.objectValue;
            }
            set
            {
                this.objectValue = value;
            }
        }

        public ObjectEventArgs(object objectValue)
        {
            this.objectValue = objectValue;
        }
    }
}
