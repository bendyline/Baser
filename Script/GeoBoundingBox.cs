﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Runtime.CompilerServices;

namespace BL
{
    /// <summary>
    /// Simple object definition of a geopgraphic bounding box.
    /// </summary>
    [Imported]
    [IgnoreNamespace]
    [ScriptName("Object")]    
    public class GeoBoundingBox
    {
        public double NorthLatitude;
        public double SouthLatitude;
        public double WestLongitude;
        public double EastLongitude;
    }
}
