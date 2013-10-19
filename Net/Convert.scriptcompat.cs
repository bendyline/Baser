using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public static class Convert
    {
        public static String ToString(Int32 intNumber)
        {
            String s = null;

            Script.Literal(@"s = intNumber + """";");

            return s;
        }

        public static int ToInt32(String numberVal)
        {
            Int32 i = -1;

            Script.Literal(@"i = parseInt(numberVal);");

            return i;
        }

        public static byte ToByte(Int32 intNumber)
        {
            byte b = 0;

            Script.Literal(@"b = intNumber;");

            return b;
        }

        public static char ToChar(Int32 intNumber)
        {
            char c = ' ';

            Script.Literal(@"c = String.fromCharCode(intNubmer);");

            return c;
        }
    }
}
