/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace Bendyline.Base
{
    public delegate void GeoLocationEvent(object sender, GeoLocationEventArgs e);

    public class GeoLocationEventArgs : EventArgs
    {
        private GeoLocation geopointValue;

        public GeoLocation Value
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

        public GeoLocationEventArgs(GeoLocation geopointValue)
        {
            this.geopointValue = geopointValue;
        }
    }
}
