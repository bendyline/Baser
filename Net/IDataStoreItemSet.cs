using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public interface IDataStoreItemSet
    {
        IList<IDataStoreItem> Items
        {
            get;
        }

        IDataStoreList List
        {
            get;
        }

        DataStoreQuery Query
        {
            get;
        }

        event DataStoreItemSetEventHandler ItemSetChanged;

        IDataStoreItem GetItemById(String id);
        void BeginRetrieve(AsyncCallback callback, object state);
        ICollection<IDataStoreItem> EndRetrieve(IAsyncResult result);
    }
}
