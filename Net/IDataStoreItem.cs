using System;

namespace Bendyline.Base
{
    public interface IDataStoreItem
    {
        event DataStoreItemEventHandler ItemChanged;

        string Id { get; }

        object GetValue(String name);
        void SetValue(String name, object value);

        String GetStringValue(String name);
        void SetStringValue(String name, String value);
    }
}
