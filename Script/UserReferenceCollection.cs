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
        private Dictionary<String, UserReference> userReferencesById;
        
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
            this.userReferencesById = new Dictionary<String, UserReference>();
        }

        public void RemoveById(String id)
        {
            UserReference uref = this.userReferencesById[id];

            this.Remove(uref);
        }

        public void Remove(UserReference userReference)
        {
            if (userReference != null)
            {
                this.userReferencesById[userReference.UniqueKey] = null;

                for (int i=0; i<this.userReferences.Count; i++)
                {
                    UserReference ur = (UserReference)this.userReferences[i];

                    if (ur.UniqueKey == userReference.UniqueKey)
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
            return this.userReferencesById[id];
        }

        public void Clear()
        {
            this.userReferences.Clear();
            this.userReferencesById.Clear();
        }

        public SerializableObject Create()
        {
            UserReference sens = new UserReference();

            return sens;
        }

        public void Add(SerializableObject userReference)
        {
            this.userReferences.Add(userReference);

            this.userReferencesById[((UserReference)userReference).UniqueKey] = (UserReference)userReference;

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, NotifyCollectionChangedEventArgs.ItemAdded(userReference));
            }
        }
    }
}
