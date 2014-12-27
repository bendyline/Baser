/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    public class PropertyChangedEventArgs : EventArgs
    {
        private String propertyName;
        private object previousValue;
        private object newValue;

        public object PreviousValue
        {
            get
            {
                return this.previousValue;
            }

            set
            {
                this.previousValue = value;
            }
        }

        public object NewValue
        {
            get
            {
                return this.newValue;
            }

            set
            {
                this.newValue = value;
            }
        }

        public String PropertyName
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        public PropertyChangedEventArgs(String propertyName)
        {
            this.propertyName = propertyName;
        }       

        public static PropertyChangedEventArgs All
        {
            get
            {
                return new PropertyChangedEventArgs("*");
            }
        }
    }
}
