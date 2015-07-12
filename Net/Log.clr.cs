/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bendyline.Base
{
    public static partial class Log
    {

        [Conditional("DEBUG")]
        public static void EndScope(String id, String message)
        {
            DateTime now = DateTime.UtcNow;

            String timeSummary = "";
            double timeTakenMs = -1;

            if (scopeStarts.ContainsKey(id))
            {
                TimeSpan timeTaken = now.Subtract(scopeStarts[id]);

                timeSummary = Convert.ToInt32(timeTaken.TotalMilliseconds) + "ms";
                timeTakenMs = timeTaken.TotalMilliseconds;
            }
            else
            {
                timeSummary = "unk";
            }

            scopeStarts[id] = DateTime.UtcNow;

            if (message != null)
            {
                IdMessageStatusAndTime(id, String.Format(message, timeSummary), LogStatus.ScopeEnd, timeTakenMs);
            }
        }
    }
}
