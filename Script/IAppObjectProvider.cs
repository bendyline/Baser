/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;

namespace BL
{
    /// <summary>
    /// Provides a basic interface to describe "object factories" -- for letting app implementations choose what kind
    /// of object to create.
    /// </summary>
    public interface IAppObjectProvider
    {
        object CreateObject(String objectName);
    }
}
