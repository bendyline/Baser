using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public abstract class DataStoreItemSet : IDataStoreItemSet
    {
        private IDataStoreList list;
        private DataStoreQuery query;

        public abstract IList<IDataStoreItem> Items { get; }

        public event DataStoreItemSetEventHandler ItemSetChanged;

        public DataStoreQuery Query
        {
            get
            {
                return this.query;
            }
        }

        public IDataStoreList List
        {
            get
            {
                return this.list;
            }
        }

        public DataStoreItemSet(IDataStoreList list, DataStoreQuery query)
        {
            this.list = list;
            this.query = query;
        }

        public abstract IDataStoreItem GetItemById(String id);
        public abstract void BeginRetrieve(AsyncCallback callback, object state);
        public abstract ICollection<IDataStoreItem> EndRetrieve(IAsyncResult result);
    }
}
