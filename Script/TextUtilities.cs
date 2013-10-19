using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public static class TextUtilities
    {

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
