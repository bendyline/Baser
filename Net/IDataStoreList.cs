using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bendyline.Base
{
    public interface IDataStoreList
    {
        ICollection<IDataStoreField> Fields { get; }
        ICollection<IDataStoreItem> AllLocalItems{ get; }
        String Name { get; }

        IDataStoreField AssumeField(String name, DataStoreFieldType fieldType);

        IDataStoreItem CreateItem();

        IDataStoreItemSet EnsureAllItemsSet();
        IDataStoreItemSet EnsureItemSet(DataStoreQuery query);
       
        void BeginUpdate(AsyncCallback callback, object asyncState);
    }
}
