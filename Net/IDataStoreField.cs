using System;

namespace Bendyline.Base
{
    public interface IDataStoreField
    {
        String Name { get; }
        DataStoreFieldType Type { get; }
    }
}
