/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BL
{
    public enum LogStatus
    {
        Critical = 1,
        Normal = 2,
        Verbose = 4,
        UnexpectedError = 8,
        ScopeStart = 16,
        ScopeEnd = 32
    }

    public static partial class Log
    {
        private static List<LogItem> logItems;
        private static Dictionary<string, Date> scopeStarts;

        public static ICollection<LogItem> Items
        {
            get
            {
                return logItems;
            }
        }

        public static event LogItemEventHandler ItemAdded;

        static Log()
        {
            logItems = new List<LogItem>();
            scopeStarts = new Dictionary<string, Date>();
        }

        [Conditional("DEBUG")]
        public static void EnterScope(String id)
        {
            EnterScopeMessage(id, null);
        }

        [Conditional("DEBUG")]
        public static void EnterScopeMessage(String id, String message)
        {
            scopeStarts[id] = Date.Now;

            if (message != null)
            {
                Full(id, message, LogStatus.ScopeStart);
            }
        }


        [Conditional("DEBUG")]
        public static void EndScope(String id, String message)
        {
            Date now = Date.Now;

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

            scopeStarts[id] = Date.Now;

            if (message != null)
            {
                FullTime(id, String.Format(message, timeSummary), null, LogStatus.ScopeEnd, timeTakenMs, null);
            }
        }

        [Conditional("DEBUG")]
        public static void DebugMessageStatus(String message, LogStatus status)
        {
            Full(null, message, status);
        }

        [Conditional("DEBUG")]
        public static void DebugMessage(String message)
        {
            FullTime(null, message, null, LogStatus.Verbose, -1, null);
        }

        public static void DetailedMessage(String message, object details)
        {
            FullTime(null, message, null, LogStatus.Normal, -1, details);
        }

        public static void DetailedDocument(RichContentDocument document, object details)
        {
            FullTime(null, null, document, LogStatus.Normal, -1, details);
        }

        public static void Message(String message)
        {
            FullTime(null, message, null, LogStatus.Verbose, -1, null);
        }

        public static void Error(String message)
        {
            FullTime(null, message, null, LogStatus.UnexpectedError, -1, null);
        }

        public static void Full(String id, String message, LogStatus status)
        {
            FullTime(id, message, null, status, -1, null);
        }

        public static void FullTime(String id, String message, RichContentDocument document, LogStatus status, double timeTaken, object details)
        {
            LogItem item = new LogItem(message, document, status, timeTaken, null);
            item.Id = id;
            item.Details = details;
            logItems.Add(item);
            
            if (ItemAdded != null)
            {
                LogItemEventArgs liea = new LogItemEventArgs(item);

                ItemAdded(item, liea);
            }
        }
    }
}
