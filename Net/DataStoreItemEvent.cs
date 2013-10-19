using System;

namespace Bendyline.Base
{
    public delegate void DataStoreItemEventHandler(object sender, DataStoreItemEventArgs e);

    public class DataStoreItemEventArgs : EventArgs
    {
        private IDataStoreItem item;

        public IDataStoreItem Item
        {
            get
            {
                return this.item;
            }

            set
            {
                this.item = value;
            }
        }

        public DataStoreItemEventArgs(IDataStoreItem item)
        {
            this.item = item;
        }
    }
}
