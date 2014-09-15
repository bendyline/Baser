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
            if (e.Item.Status == LogStatus.UnexpectedError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (e.Item.Status == LogStatus.Critical)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (e.Item.Status == LogStatus.Verbose)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ResetColor();
            }

            Console.WriteLine(e.Item.Message);
        }
    }
}
