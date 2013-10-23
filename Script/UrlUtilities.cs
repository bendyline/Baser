﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;

namespace Bendyline.Base
{
    public static class UrlUtilities
    {
        public static String EnsurePathDoesNotEndWithSlash(String path)
        {
            if (path.EndsWith("/"))
            {
                path = path.Substring(0, path.Length - 1);
            }

            return path;
        }

        public static String GetCanonicalBaseUrl(String url)
        {
            int nextSlash = url.IndexOf("/", 8);

            if (nextSlash >= 0)
            {
                url = url.Substring(0, nextSlash + 1);
            }

            return url.ToLowerCase();
        }

        public static String EnsurePathEndsWithSlash(String path)
        {
            if (!path.EndsWith("/"))
            {
                path += "/";
            }

            return path;
        }


        public static String EnsurePathDoesNotStartWithSlash(String path)
        {
            if (path.StartsWith("/"))
            {
                path = path.Substring(1, path.Length);
            }

            return path;
        }

        public static String EnsurePathStartsWithSlash(String path)
        {
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }

            return path;
        }
    }
}
