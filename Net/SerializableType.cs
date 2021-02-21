/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public partial class SerializableType
    {
        private readonly Type type;
        private readonly Dictionary<String, SerializableProperty> properties;
        private readonly Dictionary<String, SerializableProperty> propertiesBySerializationName;
        private String tagName;
        private SerializableTypeManager typeManager;

        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        public String TagName
        {
            get
            {
                if (tagName != null)
                {
                    return tagName;
                }

                return type.FullName;
            }

            set
            {
                this.tagName = value;
            }
        }


        public SerializableProperty this[String value]
        {
            get
            {
                if (this.properties.ContainsKey(value))
                {
                    return this.properties[value];
                }

                return null;
            }
        }

        internal SerializableType(SerializableTypeManager stm, Type t)
        {
            this.typeManager = stm;
            this.type = t;
            this.properties = new Dictionary<string, SerializableProperty>();
            this.propertiesBySerializationName = new Dictionary<string, SerializableProperty>();
        }

        public bool HasProperty(String propertyName)
        {
            return this.properties.ContainsKey(propertyName);
        }


        public SerializableProperty GetPropertyBySerializationName(String serializationName)
        {
            if (this.propertiesBySerializationName.ContainsKey(serializationName))
            {
                return this.propertiesBySerializationName[serializationName];
            }

            return null;
        }

        internal void EnsureBaseProperties(SerializableObject so)
        {
            throw new NotImplementedException();
        }

        public void SetProperty(SerializableObject so, String propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public SerializableProperty EnsureCollection(String propertyName, String itemName, String containerNodeName, String propertyToAddObjectTo, String itemTypeName)
        {
            if (this.properties.ContainsKey(propertyName))
            {
                return this.properties[propertyName];
            }

            if (itemName == null)
            {
                itemName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.IsAttribute = false;
            sp.Type = SerializablePropertyType.Collection;
            sp.ContainerNodeName = containerNodeName;
            sp.PropertyToAddObjectTo = propertyToAddObjectTo;
            sp.ItemName = itemName;
            sp.ItemType = Type.GetType(itemTypeName);

            this.EnsurePropertyLocal(sp);
            this.properties[propertyName] = sp;
            this.propertiesBySerializationName[propertyName] = sp;

            return sp;
        }

        public SerializableProperty EnsureObject(String propertyName, String serializationName)
        {
            if (this.properties.ContainsKey(propertyName))
            {
                return this.properties[propertyName];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.Type = SerializablePropertyType.Object;
            sp.SerializationName = serializationName;
            sp.IsAttribute = false;
            this.EnsurePropertyLocal(sp);
            this.properties[propertyName] = sp;
            this.propertiesBySerializationName[serializationName] = sp;

            return sp;
        }

        public SerializableProperty EnsureEnum(String propertyName, String serializationName)
        {
            if (this.properties.ContainsKey(propertyName))
            {
                return this.properties[propertyName];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.SerializationName = serializationName; 
            sp.IsAttribute = true;
            sp.Type = SerializablePropertyType.Enum;
            this.EnsurePropertyLocal(sp);
            this.properties[propertyName] = sp;
            this.propertiesBySerializationName[serializationName] = sp;

            return sp;
        }

        public SerializableProperty EnsureInteger(String propertyName, String serializationName)
        {
            if (this.properties.ContainsKey(propertyName))
            {
                return this.properties[propertyName];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.SerializationName = serializationName; 
            sp.IsAttribute = true;
            sp.Type = SerializablePropertyType.Integer;
            this.EnsurePropertyLocal(sp);
            this.properties[propertyName] = sp;
            this.propertiesBySerializationName[serializationName] = sp;

            return sp;
        }

        public SerializableProperty EnsureDouble(String propertyName, String serializationName)
        {
            if (this.properties.ContainsKey(propertyName))
            {
                return this.properties[propertyName];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.SerializationName = serializationName; 
            sp.IsAttribute = true;
            sp.Type = SerializablePropertyType.Double;
            this.EnsurePropertyLocal(sp);
            this.properties[propertyName] = sp;
            this.propertiesBySerializationName[serializationName] = sp;

            return sp;
        }

        public SerializableProperty EnsureBoolean(String propertyName, String serializationName)
        {
            if (this.properties.ContainsKey(propertyName))
            {
                return this.properties[propertyName];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.SerializationName = serializationName; 
            sp.IsAttribute = true;
            this.EnsurePropertyLocal(sp);
            this.properties[propertyName] = sp;
            this.propertiesBySerializationName[serializationName] = sp;

            return sp;
        }

        public SerializableProperty EnsureString(String propertyName, String serializationName)
        {
            if (this.properties.ContainsKey(propertyName))
            {
                return this.properties[propertyName];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.SerializationName = serializationName; 
            sp.IsAttribute = true;
            this.EnsurePropertyLocal(sp);
            this.properties[propertyName] = sp;
            this.propertiesBySerializationName[serializationName] = sp;

            return sp;
        }

        public SerializableProperty EnsureProperty(String propertyName, String serializationName)
        {
            if (this.properties.ContainsKey(propertyName))
            {
                return this.properties[propertyName];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.SerializationName = serializationName; 
            sp.Name = propertyName;
            this.EnsurePropertyLocal(sp);
            this.properties[propertyName] = sp;
            this.propertiesBySerializationName[serializationName] = sp;

            return sp;
        }
    }
}
