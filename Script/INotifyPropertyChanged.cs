using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
}
