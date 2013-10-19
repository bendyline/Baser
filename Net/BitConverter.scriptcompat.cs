using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public class BitConverter
    {
        public static Int32 ToInt32(byte[] bytes, int vala)
        {
            int val = 0;

            val += (bytes[0] * 16777216) + (bytes[1] * 65536) + (bytes[2] * 256) + bytes[3];

            return val;
        }
    }
}
