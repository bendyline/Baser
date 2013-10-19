using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Bendyline.Base
{
    public abstract class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection, INotifyCollectionChanged
        where TValue: class
    {
        [IgnoreDataMember]
        private readonly Dictionary<TKey, TValue> dict;

        [IgnoreDataMember]
        private readonly List<TValue> items;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        [IgnoreDataMember]
        public ICollection<TKey> Keys
        {
            get
            {
                return this.dict.Keys;
            }
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        [IgnoreDataMember]
        public ICollection<TValue> Values
        {
            get
            {
                return this.items;
            }
        }

        public TValue this[int index]
        {
            get
            {
                return this.items[index];
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.dict[key];
            }

            set
            {
                TValue oldItem = default(TValue);

                if (this.dict.ContainsKey(key))
                {
                    oldItem = this.dict[key];
                }

                if (oldItem == value)
                {
                    return;
                }

                this.dict[key] = value;

                int index = this.items.IndexOf(oldItem);

                if (index >= 0)
                {
                    this.items[index] = value;
                }

                if (this.CollectionChanged != null)
                {
                    NotifyCollectionChangedEventArgs nccea = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index);

                    this.CollectionChanged(this, nccea);
                }
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public ObservableDictionary()
        {
            this.items = new List<TValue>();
            this.dict = new Dictionary<TKey, TValue>();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)this.items).GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool Insert(int i, object value)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            return this.items.Contains((TValue)value);
        }

        public virtual int Add(object value)
        {
            if (!(value is TValue))
            {
                throw new InvalidOperationException();
            }

            this.Add((TValue)value);

            return this.Count;
        }

        public abstract void Add(TValue value);

        public void Add(TKey key, TValue value)
        {
            this.dict.Add(key, value);
            this.items.Add(value);

            if (this.CollectionChanged != null)
            {
                NotifyCollectionChangedEventArgs nccea = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, this.items.Count - 1);

                this.CollectionChanged(this, nccea);
            }
        }

        public bool ContainsKey(TKey key)
        {
            return this.dict.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            if (this.dict.ContainsKey(key))
            {
                TValue value = this.dict[key];

                int index = this.items.IndexOf(value);

                bool result = this.dict.Remove(key);
                this.items.Remove(value);

                if (result && value != null && index >= 0)
                {
                    if (this.CollectionChanged != null)
                    {
                        NotifyCollectionChangedEventArgs nccea = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value, index);

                        this.CollectionChanged(this, nccea);
                    }
                }

                return result;
            }

            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.dict.TryGetValue(key, out value);
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.dict).Add(item);

            TValue value = item.Value;

            this.items.Add(value);

            if (this.CollectionChanged != null)
            {
                NotifyCollectionChangedEventArgs nccea = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, this.items.Count - 1);

                this.CollectionChanged(this, nccea);
            }
        }
        public void Clear()
        {
            this.items.Clear();
            this.dict.Clear();

            if (this.CollectionChanged != null)
            {
                NotifyCollectionChangedEventArgs nccea = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);

                this.CollectionChanged(this, nccea);
            }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return this.dict.GetEnumerator();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)this.dict).Contains(item);
        }
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)this.dict).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Remove(item.Key);
        }

    }
}
