/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    /// <summary>
    /// Standard callback for callbacks based on the .net IAsync pattern.
    /// </summary>
    /// <param name="result">
    /// Derived class that contains both standard result data plus any custom additional data. 
    /// </param>
    public delegate void AsyncCallback(IAsyncResult result);
}
