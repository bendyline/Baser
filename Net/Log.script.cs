// Log.cs
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using DateTime = System.Date;

namespace Bendyline.Base
{
    public static partial class Log
    {

        [Conditional("DEBUG")]
        public static void EndScope(String id, String message)
        {
            DateTime now = DateTime.Now;

            String timeSummary = "";
            double timeTakenMs = -1;

            if (scopeStarts.ContainsKey(id))
            {
                /*TimeSpan timeTaken = now.Subtract(scopeStarts[id]);

                timeSummary = Convert.ToInt32(timeTaken.TotalMilliseconds) + "ms";
                timeTakenMs = timeTaken.TotalMilliseconds;*/
            }
            else
            {
                timeSummary = "unk";
            }

            scopeStarts[id] = DateTime.Now;

            if (message != null)
            {
                MessageStatusAndTime(String.Format(message, timeSummary), LogStatus.ScopeEnd, timeTakenMs);
            }
        }
    }
}
