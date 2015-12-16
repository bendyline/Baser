/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Runtime.CompilerServices;

namespace Bendyline.Base
{
    /// <summary>
    /// Simple object definition of a geopgraphic bounding box.
    /// </summary>
    public class GeoBoundingBox
    {
        public double NorthLatitude;
        public double SouthLatitude;
        public double WestLongitude;
        public double EastLongitude;
    }
}
