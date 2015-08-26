// SensorSystemCollection.cs
//

using System;
using System.Collections.Generic;
using System.Collections;
using BL;

namespace BL
{
    public class UserReferenceCollection : ISerializableCollection, IEnumerable, INotifyCollectionAndStateChanged
    {
        private ArrayList userReferences;
        private Dictionary<String, UserReference> userReferencesByUniqueKey;
        
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ArrayList UserReferences
        {
            get
            {
                return this.userReferences;
            }
        }

        public UserReference this[int index]
        {
            get
            {
                return (UserReference)this.userReferences[index];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.userReferences.GetEnumerator();
        }

        public UserReferenceCollection()
        {
            this.userReferences = new ArrayList();
            this.userReferencesByUniqueKey = new Dictionary<String, UserReference>();
        }

        public void RemoveById(String id)
        {
            UserReference uref = this.userReferencesByUniqueKey[id];

            this.Remove(uref);
        }

        public void Remove(UserReference userReference)
        {
            if (userReference != null)
            {
                this.userReferencesByUniqueKey[userReference.UniqueKey] = null;

                for (int i=0; i<this.userReferences.Count; i++)
                {
                    UserReference ur = (UserReference)this.userReferences[i];

                    if (ur.Id == userReference.Id || ur.UniqueKey == userReference.UniqueKey)
                    {
                        this.userReferences.Remove(ur);
                    }
                }
            }

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, NotifyCollectionChangedEventArgs.ItemRemoved(userReference));
            }
        }

        public UserReference GetById(String id)
        {
            return this.userReferencesByUniqueKey[id];
        }

        public void Clear()
        {
            this.userReferences.Clear();
            this.userReferencesByUniqueKey.Clear();
        }

        public SerializableObject Create()
        {
            UserReference sens = new UserReference();

            return sens;
        }

        public void Add(SerializableObject userReference)
        {
            this.userReferences.Add(userReference);

            this.userReferencesByUniqueKey[((UserReference)userReference).UniqueKey] = (UserReference)userReference;

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, NotifyCollectionChangedEventArgs.ItemAdded(userReference));
            }
        }
    }
}
