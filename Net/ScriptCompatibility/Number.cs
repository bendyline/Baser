using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bendyline.Base.ScriptCompatibility
{
    public class Number
    {

        public static Int64 Parse(String s)
        {
            return Convert.ToInt64(s);
        }
    }
}
