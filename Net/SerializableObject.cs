/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Bendyline.Base
{
    public partial class SerializableObject
    {
        private SerializableType serializableType;
        protected bool isInitializedForSerialization;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual String TagName
        {
            get
            {
                return null;
            }
        }

        protected SerializableType SerializableType
        {
            get
            {
                return this.serializableType;
            }
        }

        protected virtual void InitializeForSerialization()
        {

        }

        private void EnsureInitializedForSerialization()
        {
            if (!isInitializedForSerialization)
            {
                isInitializedForSerialization = true;

                Type t = this.GetType();

                this.serializableType = SerializableTypeManager.Current.GetTypeByName(t.FullName);

                if (this.serializableType == null)
                {
                    this.serializableType = SerializableTypeManager.Current.Ensure(t);
                    this.serializableType.TagName = this.TagName;
                    this.serializableType.BeginInit();
                    this.InitializeForSerialization();
                    this.serializableType.EndInit();
                }
            }
        }


        protected void NotifyPropertyChangedWithValues(String propertyName, object previousValue, object newValue)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChangedEventArgs pcea = new PropertyChangedEventArgs(propertyName);
          //      pcea.PreviousValue = previousValue;
            //    pcea.NewValue = newValue;
                this.PropertyChanged(this, pcea);
            }
        }

        protected virtual void OnPropertyChanged(String propertyName)
        {

            throw new NotImplementedException();
        }

        public void ApplyString(String stringSerialization)
        {

            throw new NotImplementedException();
        }

        public String ToJson()
        {
            throw new NotImplementedException();
        }

        public void ApplyObject(object objectToCopy)
        {
            throw new NotImplementedException();
        }

        public void CopyFromObject(object objectToCopy)
        {
            throw new NotImplementedException();
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            this.OnPropertyChanged(propertyName);

            if (this.PropertyChanged != null)
            {
                PropertyChangedEventArgs pcea = new PropertyChangedEventArgs(propertyName);

                this.PropertyChanged(this, pcea);
            }
        }
    }
}
