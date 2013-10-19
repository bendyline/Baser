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

            throw new NotImplementedException();
          //  if (type.GetTypeInfo().IsSubclassOf(typeof(SerializableObject)))
            {
                this.isComplex = true;
                this.isSerializableObject = true;

                return;
            }

            this.isComplex = !(type.GetTypeInfo().IsPrimitive || type.FullName == "System.String");
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
