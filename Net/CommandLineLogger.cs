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
            Console.WriteLine(e.Item.Message);
        }
    }
}
