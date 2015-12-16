using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{  
    public interface INotifyCollectionAndStateChanged
    {
        event Bendyline.Base.NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
