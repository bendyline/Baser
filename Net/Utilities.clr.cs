/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public static partial class Utilities
    {
        public static String AssemblyDetails
        {
            get
            {
                return ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=7791b3b09e7c4049";
            }
        }

        public static bool IsPhone
        {
            get
            {
                return false;
            }
        }

        public static String SharePointClientAssemblyName
        {
            get
            {
                return "Bendyline.Base.SharePointClient.net" + Utilities.AssemblyDetails;
            }
        }

        public static String SharePointClientTypeSuffix
        {
            get
            {
                return ", " + SharePointClientAssemblyName;
            }
        }

        public static String AssemblyName
        {
            get
            {
                return "Bendyline.Base.Server" + Utilities.AssemblyDetails;
            }
        }

        public static String TypeSuffix
        {
            get
            {
                return ", " + AssemblyName;
            }
        }

    }
}
