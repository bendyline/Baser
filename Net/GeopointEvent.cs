/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace Bendyline.Base
{
    public delegate void GeopointEventHandler(object sender, GeopointEventArgs e);

    public class GeopointEventArgs : EventArgs
    {
        private Geopoint geopointValue;

        public Geopoint Value
        {
            get
            {
                return this.geopointValue;
            }
            set
            {
                this.geopointValue = value;
            }
        }

        public GeopointEventArgs(Geopoint geopointValue)
        {
            this.geopointValue = geopointValue;
        }
    }
}
