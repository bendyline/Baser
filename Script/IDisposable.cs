/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;

namespace BL
{
    /// <summary>
    /// Standard interface to indicate that an object supports explicit Dispose semantics, where it will
    /// clean itself up (e.g., unhook events, remove references to objects, etc.)
    /// </summary>
    interface IDisposable
    {
        void Dispose();
    }
}
