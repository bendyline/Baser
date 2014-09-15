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

    /// <summary>
    /// Provides a general static function for logging client side behaviors. (Log.Message, Log.Error, etc.)
    /// Note that you still need a logging implementation that hooks ItemAdded events and actually persists them to 
    /// a logging store of sort.
    /// </summary>
    public static partial class Log
    {
        private static List<LogItem> logItems;
        private static Dictionary<int, Date> scopeStarts;

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
            scopeStarts = new Dictionary<int, Date>();
        }

        [Conditional("DEBUG")]
        public static void EnterScope(int eventId)
        {
            EnterScopeMessage(eventId, null);
        }

        [Conditional("DEBUG")]
        public static void EnterScopeMessage(int eventId, String message)
        {
            scopeStarts[eventId] = Date.Now;

            if (message != null)
            {
                Full(eventId, message, LogStatus.ScopeStart);
            }
        }


        [Conditional("DEBUG")]
        public static void EndScope(int eventId, String message)
        {
            Date now = Date.Now;

            String timeSummary = "";
            double timeTakenMs = -1;

            if (scopeStarts.ContainsKey(eventId))
            {
                /*TimeSpan timeTaken = now.Subtract(scopeStarts[id]);

                timeSummary = Convert.ToInt32(timeTaken.TotalMilliseconds) + "ms";
                timeTakenMs = timeTaken.TotalMilliseconds;*/
            }
            else
            {
                timeSummary = "unk";
            }

            scopeStarts[eventId] = Date.Now;

            if (message != null)
            {
                FullTime(eventId, String.Format(message, timeSummary), LogStatus.ScopeEnd, timeTakenMs, null);
            }
        }

        [Conditional("DEBUG")]
        public static void DebugMessageStatus(String message, LogStatus status)
        {
            Full(-1, message, status);
        }

        [Conditional("DEBUG")]
        public static void DebugMessage(String message)
        {
            FullTime(-1, message, LogStatus.Verbose, -1, null);
        }

        public static void DetailedMessage(String message, object details)
        {
            FullTime(-1, message, LogStatus.Normal, -1, details);
        }

        public static void Event(int eventId)
        {
            FullTime(eventId, null, LogStatus.Normal, -1, null);
        }

        public static void DetailedEvent(int eventId, String message)
        {
            FullTime(eventId, null, LogStatus.Normal, -1, message);
        }

        public static void Message(String message)
        {
            FullTime(-1, message, LogStatus.Verbose, -1, null);
        }

        public static void Error(String message)
        {
            FullTime(-1, message, LogStatus.UnexpectedError, -1, null);
        }

        public static void Full(int eventId, String message, LogStatus status)
        {
            FullTime(eventId, message, status, -1, null);
        }

        public static void FullTime(int eventId, String message, LogStatus status, double timeTaken, object details)
        {
            Debug.WriteLine("(Log) - " + eventId + " - " + message + " " + details);

            LogItem item = new LogItem(eventId, message, status, timeTaken, null);

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
