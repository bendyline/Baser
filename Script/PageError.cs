using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;

namespace BL
{
   
    public class PageError
    {
        private String message;
        private String url;
        private String line;
        private String trace;

        public static event PageErrorEventHandler Occurred;

        public PageError(String message, String url, String line, String trace)
        {
            this.message = message;
            this.url = url;
            this.line = line;
            this.trace = trace;
        }

        public String GetHtml()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<h3>");
            sb.Append("Error: " + message);
            sb.Append("</h3>");

            sb.Append("<h4>");
            sb.Append("Stack: " + trace);
            sb.Append("</h4>");

            return sb.ToString();
        }

        public static void Attach()
        {
            Window.Onerror = HandleError;
        }

        public static bool HandleError(string message, string url, int line)
        {
            String stack = null;

            Script.Literal("{0}=BL.PageError.HandleError(arguments.callee.caller)", stack);

            return true;
        }

        public static String GetTrace(Function f)
        {
            Script.Literal(@" if ({0} == null) {{ return []; }} else {{ return BL.PageError.GetTrace({0}.caller).concat([{0}.toString().split('(')[0].substring(9) + '(' + {0}.arguments.join(',') + ')']); }}", f);

            return String.Empty;
        }
    }
}
