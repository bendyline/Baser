using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public interface ISerializableCollection 
    {
        void Clear();
        SerializableObject Create();
        void Add(SerializableObject o);
    }
}
