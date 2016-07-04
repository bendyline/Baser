/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Runtime.CompilerServices;

namespace Bendyline.Base
{
    /// <summary>
    /// Simple description of a point on the earth (note: this object does not include altitude.)
    /// </summary>
    public class GeoLocation
    {
        public double latitude;
        public double longitude;

        public double Latitude
        {
            get
            {
                return this.latitude;
            }

            set
            {
                this.latitude = value;
            }
        }

        public double Longitude
        {
            get
            {
                return this.longitude;
            }

            set
            {
                this.longitude = value;
            }
        }

        public static bool operator !=(GeoLocation x, GeoLocation y)
        {
            return !(x.latitude == y.latitude && x.longitude == y.longitude);
        }

        public static bool operator ==(GeoLocation x, GeoLocation y)
        {
            return (x.latitude == y.latitude && x.longitude == y.longitude);
        }
    }
}
