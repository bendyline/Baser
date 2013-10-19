using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public interface ICommand
    {
        String Id
        {
            get; 
        }

        void OutputHelp();
        bool Validate();
        void Execute();
        void Dispose();
    }
}
