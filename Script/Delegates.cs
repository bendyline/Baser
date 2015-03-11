/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BL
{
    [IgnoreNamespace]
    [Imported]
    public delegate String StringTransformerAction(String input);

    [IgnoreNamespace]
    [Imported]
    public delegate String TwoStringTransformerAction(String inputA, String inputB);

}
