/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public static class JsonUtilities
    {
        public static String Encode(String value)
        {
            if (value == null)
            {
                return null;
            }

            value = value.Replace("\"", "\\\"");

            return value;
        }
    }
}
