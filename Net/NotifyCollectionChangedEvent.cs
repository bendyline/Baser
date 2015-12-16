/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using Bendyline.Base.ScriptCompatibility;
using System;
using System.Collections;

namespace Bendyline.Base
{
    public delegate void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e);

    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        private NotifyCollectionChangedAction action;
        private IEnumerable newItems;
        private IEnumerable oldItems;
        private IEnumerable changedItems;
        private String stateChangePropertyName;

        public String StateChangePropertyName
        {
            get
            {
                return this.stateChangePropertyName;
            }
            
            set
            {
                this.stateChangePropertyName = value;
            }
        }

        public NotifyCollectionChangedAction Action
        {
            get
            {
                return this.action;
            }
        }

        public IEnumerable ChangedItems
        {
            get
            {
                return this.changedItems;
            }

            set
            {
                this.changedItems = value;
            }
        }

        public IEnumerable NewItems
        {
            get
            {
                return this.newItems;
            }

            set
            {
                this.newItems = value;
            }
        }

        public IEnumerable OldItems
        {
            get
            {
                return this.oldItems;
            }

            set
            {
                this.oldItems = value;
            }
        }


        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
        {
            this.action = action;
        }


        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newValue, int index)
        {
            this.action = action;

            ArrayList al = new ArrayList();
            al.Add(newValue);

            this.newItems = al;
        }
    

        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newValue, object oldValue, int index)
        {
            this.action = action;


            ArrayList al = new ArrayList();
            al.Add(newValue);

            this.newItems = al;

            al = new ArrayList();
            al.Add(oldValue);

            this.oldItems = al;
        }



        public static NotifyCollectionChangedEventArgs ItemAdded(object item)
        {
            NotifyCollectionChangedEventArgs ncea = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add);

            ArrayList al = new ArrayList();
            al.Add(item);

            ncea.NewItems = al;

            return ncea;
        }

        public static NotifyCollectionChangedEventArgs ItemStateChange(object item, String propertyName)
        {
            NotifyCollectionChangedEventArgs ncea = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemStateChanged);

            ArrayList al = new ArrayList();
            al.Add(item);

            ncea.ChangedItems = al;
            ncea.StateChangePropertyName = propertyName;

            return ncea;
        }

        public static NotifyCollectionChangedEventArgs ItemRemoved(object item)
        {
            NotifyCollectionChangedEventArgs ncea = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove);

            ArrayList al = new ArrayList();
            al.Add(item);

            ncea.OldItems = al;

            return ncea;
        }

    }
}
