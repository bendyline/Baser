/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Reflection;

namespace Bendyline.Base
{
    public partial class SerializableProperty
    {
        private PropertyInfo propertyInfo;

        internal PropertyInfo PropertyInfo
        {
            get
            {
                return this.propertyInfo;
            }

            set
            {
                this.propertyInfo = value;
                this.name = propertyInfo.Name;

                this.Initialize();
            }
        }

        private void Initialize()
        {
            Type type = this.propertyInfo.PropertyType;


            if (type.IsSubclassOf(typeof(SerializableObject)))
            {
                this.isComplex = true;
                this.isSerializableObject = true;

                return;
            }

            this.isComplex = !(type.IsPrimitive || type.IsEnum || type.FullName == "System.String");
        }

        internal void SetToObject(object so, object value)
        {
            this.propertyInfo.SetValue(so, value, null);
        }

        internal object GetFromObject(object so)
        {
            return this.propertyInfo.GetValue(so, null);
        }
    }
}
