using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public interface ISerializableCollection 
    {
        void Clear();
        object Create();
        void Add(object o);
    }
}
