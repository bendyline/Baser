/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public abstract class CommandBase : ICommand
    {
        public abstract bool Validate();
        public abstract CommandResult Execute();
        public abstract void LoadFromSettings(Dictionary<String, String> settings);
        
        public abstract String Id
        {
            get;
        }

        public CommandBase()
        {
        }
        
        public virtual void OutputHelp()
        {
            this.Output(@"No help is available for this command.");
        }

        protected void Output(String message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public virtual void Dispose()
        {

        }
    }
}
