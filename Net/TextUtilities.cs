/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public static class TextUtilities
    {

        public static String StripTags(String text)
        {
            StringBuilder sb = new StringBuilder();

            int lastStart = 0;
            int nextSlash = text.IndexOf("<");

            while (nextSlash > 0)
            {
                sb.Append(text.Substring(lastStart, nextSlash - lastStart));

                int end = text.IndexOf(">", nextSlash + 1);

                if (end >= 0)
                {
                    lastStart = end + 1;

                    nextSlash = text.IndexOf("<", lastStart);
                }
                else
                {
                    lastStart = text.Length;
                    nextSlash = -1;
                }
            }

            sb.Append(text.Substring(lastStart, text.Length - lastStart));

            return sb.ToString();
        }

        public static String GetContentsInBetween(String source, String start, String end)
        {
            int firstIndex = source.IndexOf(start);

            if (firstIndex >= 0)
            {
                int lastIndex = source.IndexOf(end, firstIndex + 1);

                if (lastIndex >= 0)
                {
                    return source.Substring(firstIndex + start.Length, lastIndex - (firstIndex + start.Length));
                }
            }

            return null;
        }
    }
}
