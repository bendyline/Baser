﻿using System;
using System.Collections.Generic;

namespace Bendyline.Base
{
    public delegate void DataStoreItemSetEventHandler(object sender, DataStoreItemSetEventArgs e);

    public class DataStoreItemSetEventArgs : EventArgs
    {
        private IDataStoreItemSet itemSet;
        private List<IDataStoreItem> addedItems;
        private List<IDataStoreItem> removedItems;
        private List<IDataStoreItem> changedItems;

        public IList<IDataStoreItem> AddedItems
        {
            get
            {
                return this.addedItems;
            }
        }

        public IList<IDataStoreItem> RemovedItems
        {
            get
            {
                return this.removedItems;
            }
        }
        
        public IList<IDataStoreItem> ChangedItems
        {
            get
            {
                return this.changedItems;
            }
        }

        public IDataStoreItemSet ItemSet
        {
            get
            {
                return this.itemSet;
            }

            set
            {
                this.itemSet = value;
            }
        }

        public DataStoreItemSetEventArgs(IDataStoreItemSet itemSet)
        {
            this.itemSet = itemSet;
            this.addedItems = new List<IDataStoreItem>();
            this.removedItems = new List<IDataStoreItem>();
            this.changedItems = new List<IDataStoreItem>();
        }
    }
}
