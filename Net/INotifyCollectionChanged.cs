using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public interface INotifyCollectionChanged
    {
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
