using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public enum NotifyCollectionChangedAction
    {
        Add = 1,
        Move = 2,
        Remove = 3,
        Replace = 4,
        Reset = 5,
        ItemStateChanged = 6
    }
}
