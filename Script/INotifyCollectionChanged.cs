using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public interface INotifyCollectionChanged
    {
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
