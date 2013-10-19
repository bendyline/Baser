using System;
using System.Net;

namespace Bendyline.Base
{
    public static partial class Utilities
    {
        public static Random CreateRandomWithSeed(int seed)
        {
            return new Random(seed);
        }

        public static int NextInRange(this System.Random random, int rangeMaximum)
        {
            return random.Next(rangeMaximum);
        }

        public static int GetMinutes(this DateTime d)
        {
            return d.Minute;
        }

        public static int GetHours(this DateTime d)
        {
            return d.Hour;
        }

        public static int GetSeconds(this DateTime d)
        {
            return d.Second;
        }

        public static int GetMilliseconds(this DateTime d)
        {
            return d.Millisecond;
        }

        public static String ToLowerCase(this String str)
        {
            return str.ToLower();
        }

        public static String ToUpperCase(this String str)
        {
            return str.ToUpper();
        }
    }
}
