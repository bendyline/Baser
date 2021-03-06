﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using Bendyline.Base.ScriptCompatibility;
using System;
using System.Net;

namespace Bendyline.Base
{
    public static partial class JsonUtilities
    {
        public static String Encode(String value)
        {
            if (value == null)
            {
                return null;
            }

            value = value.Replace("\\", "\\\\");
            value = value.Replace("\"", "\\\"");

            return value;
        }
    }
}
