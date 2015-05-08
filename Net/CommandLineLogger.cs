/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public class CommandLineLogger
    {
        private static CommandLineLogger current;
        private bool initialized = false;
        private StringBuilder logRecords;
        private bool hasErrors = false;
        private StringBuilder logHtmlRecords;

        public bool HasErrors
        {
            get
            {
                return this.hasErrors;
            }
        }

        public String LogContent
        {
            get
            {
                return logRecords.ToString();
            }
        }

        public String LogHtmlContent
        {
            get
            {
                return logHtmlRecords.ToString();
            }
        }
        
        public static CommandLineLogger Current
        {
            get
            {
                if (current == null)
                {
                    current = new CommandLineLogger();
                }

                return current;
            }
        }

        public CommandLineLogger()
        {
            this.logRecords = new StringBuilder();
            this.logHtmlRecords = new StringBuilder();
        }

        public void Initialize()
        {
            if (initialized)
            {
                return;
            }

            Log.ItemAdded += new LogItemEventHandler(Log_ItemAdded);

            initialized = true;
        }

        private void Log_ItemAdded(object sender, LogItemEventArgs e)
        {
            bool setColor = false;
            String prefix = String.Empty;

            String htmlPrefix = "<div style=\"";

            if (e.Item.Status == LogStatus.UnexpectedError)
            {
                prefix = "*** ERROR ***: ";
                htmlPrefix += "color:red; backround-color:#303030;";
                hasErrors = true;
                Console.ForegroundColor = ConsoleColor.Red;
                setColor = true;
            }
            else if (e.Item.Status == LogStatus.Critical)
            {
                prefix = "*** CRITICAL ***: ";
                htmlPrefix += "color:yellow; backround-color:#303030;";

                hasErrors = true; 
                Console.ForegroundColor = ConsoleColor.Yellow;
                setColor = true;
            }
            else if (e.Item.Status == LogStatus.Verbose)
            {
                htmlPrefix += "color:#606060;";
                Console.ForegroundColor = ConsoleColor.Gray;
                setColor = true;
            }

            htmlPrefix += "\">";

            logRecords.AppendLine(prefix + e.Item.Message);

            logHtmlRecords.AppendLine(htmlPrefix + e.Item.Message + "</div>");

            Console.WriteLine(prefix + e.Item.Message);

            if (setColor)
            {
                Console.ResetColor();
            }
        }
    }
}
