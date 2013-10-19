using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            return url.ToLower();
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
                path = path.Substring(1);
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
