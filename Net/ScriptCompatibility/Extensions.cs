using System;

namespace Bendyline.Base.ScriptCompatibility
{
    public static class Extensions
    {
        public static int NextInRange(this Random random, int max)
        {
            return random.Next(max);
        }

        public static char CharAt(this String str, int position)
        {
            return str[position];
        }
    }
}
