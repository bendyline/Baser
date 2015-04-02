/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bendyline.Base
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
        private static Dictionary<String, DateTime> scopeStarts;

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
            scopeStarts = new Dictionary<string, DateTime>();
        }

        [Conditional("DEBUG")]
        public static void EnterScope(String id)
        {
            EnterScopeMessage(id, null);
        }

        [Conditional("DEBUG")]
        public static void EnterScopeMessage(String id, String message)
        {
            scopeStarts[id] = DateTime.Now.ToUniversalTime();

            if (message != null)
            {
                IdMessageStatus(id, message, LogStatus.ScopeStart);
            }
        }

        [Conditional("DEBUG")]
        public static void DebugMessageStatus(String message, LogStatus status)
        {
            MessageStatus(message, status);
        }

        [Conditional("DEBUG")]
        public static void DebugMessage(String message)
        {
            MessageDocumentStatusTimeAndDetails(message, null, LogStatus.Verbose, -1, null);
        }

        [Conditional("DEBUG")]
        public static void DebugMessage(String message, params object[] arguments)
        {
            message = String.Format(message, arguments);

            MessageDocumentStatusTimeAndDetails(message, null, LogStatus.Verbose, -1, null);
        }

        public static void DetailedMessage(String message, object details)
        {
            MessageDocumentStatusTimeAndDetails(message, null, LogStatus.Normal, -1, details);
        }

        public static void DetailedDocument(RichContentDocument document, object details)
        {
            MessageDocumentStatusTimeAndDetails(null, document, LogStatus.Normal, -1, details);
        }

        public static void Message(String message)
        {
            MessageDocumentStatusTimeAndDetails(message, null, LogStatus.Verbose, -1, null);
        }

        public static void Message(String message, params object[] arguments)
        {
            message = String.Format(message, arguments);

            MessageDocumentStatusTimeAndDetails(message, null, LogStatus.Verbose, -1, null);
        }

        public static void Error(String message)
        {
            MessageDocumentStatusTimeAndDetails(message, null, LogStatus.UnexpectedError, -1, null);
        }

        public static void Error(String message, params object[] arguments)
        {
            message = String.Format(message, arguments);

            MessageDocumentStatusTimeAndDetails(message, null, LogStatus.UnexpectedError, -1, null);
        }

        public static void MessageStatus(String message, LogStatus status)
        {
            MessageDocumentStatusTimeAndDetails(message, null, status, -1, null);
        }

        public static void IdMessageStatus(String id, String message, LogStatus status)
        {
            IdMessageDocumentStatusTimeAndDetails(id, message, null, status, -1, null);
        }

        public static void IdMessageStatusAndTime(String id, String message, LogStatus status, double timeTaken)
        {
            IdMessageDocumentStatusTimeAndDetails(id, message, null, status, timeTaken, null);
        }
        public static void MessageDocumentStatusTimeAndDetails(String message, RichContentDocument document, LogStatus status, double timeTaken, object details)
        {
            IdMessageDocumentStatusTimeAndDetails(null, message, document, status, timeTaken, details);
        }

        public static void IdMessageDocumentStatusTimeAndDetails(String id, String message, RichContentDocument document, LogStatus status, double timeTaken, object details)
        {
            LogItem item = new LogItem(message, document, status, timeTaken, null);
            item.Id = id;
            item.Details = details;
            //lock (logItems)
            //{
            logItems.Add(item);
            //}

            if (ItemAdded != null)
            {
                LogItemEventArgs liea = new LogItemEventArgs(item);

                ItemAdded(item, liea);
            }
        }
    }
}
