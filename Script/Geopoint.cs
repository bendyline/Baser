/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Runtime.CompilerServices;

namespace BL
{
    /// <summary>
    /// Simple description of a point on the earth (note: this object does not include altitude.)
    /// </summary>
    [Imported]
    [IgnoreNamespace]
    [ScriptName("Object")]    
    public class Geopoint
    {
        public double Latitude;
        public double Longitude;
    }
}
