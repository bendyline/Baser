using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public partial class SerializableType
    {
        private readonly Type type;
        private readonly Dictionary<String, SerializableProperty> properties;
        private readonly List<SerializableProperty> propertiesAsList;
        private String tagName;
        private SerializableTypeManager typeManager;


        public ICollection<SerializableProperty> Properties
        {
            get
            {
                return this.propertiesAsList;
            }
        }

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

        internal SerializableType(SerializableTypeManager stm, Type t)
        {
            this.typeManager = stm;
            this.type = t;
            this.properties = new Dictionary<string, SerializableProperty>();
            this.propertiesAsList = new List<SerializableProperty>();
        }

        public void EnsurePropertyLocal(SerializableProperty sp)
        {

        }

        public void BeginInit()
        {

        }

        public void EndInit()
        {

        }

        public SerializableProperty EnsureCollection(String propertyName, String itemName, String containerNodeName, String propertyToAddObjectTo, String itemTypeName)
        {
            String propNameCanon = propertyName.ToLowerCase();

            if (this.properties.ContainsKey(propNameCanon))
            {
                return this.properties[propNameCanon];
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

            this.AddProperty(sp);

            return sp;
        }

        public void AddProperty(SerializableProperty sp)
        {
            this.EnsurePropertyLocal(sp);

            this.properties[sp.Name.ToLowerCase()] = sp;
            this.propertiesAsList.Add(sp);
        }

        public SerializableProperty EnsureObject(String propertyName, String serializationName)
        {
            String propNameCanon = propertyName.ToLowerCase();

            if (this.properties.ContainsKey(propNameCanon))
            {
                return this.properties[propNameCanon];
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.Type = SerializablePropertyType.Object;
            sp.IsAttribute = false;
            this.AddProperty(sp);

            return sp;
        }

        public SerializableProperty EnsureEnum(String propertyName, String serializationName)
        {
            String propNameCanon = propertyName.ToLowerCase();

            if (this.properties.ContainsKey(propNameCanon))
            {
                return this.properties[propNameCanon];
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
             sp.IsAttribute = true;
            sp.Type = SerializablePropertyType.Choice;
            this.AddProperty(sp);

            return sp;
        }

        public SerializableProperty EnsureInteger(String propertyName, String serializationName)
        {
            String propNameCanon = propertyName.ToLowerCase();

            if (this.properties.ContainsKey(propNameCanon))
            {
                return this.properties[propNameCanon];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
             sp.IsAttribute = true;
            sp.Type = SerializablePropertyType.Integer;
            this.AddProperty(sp);

            return sp;
        }

        public SerializableProperty EnsureDouble(String propertyName, String serializationName)
        {
            String propNameCanon = propertyName.ToLowerCase();

            if (this.properties.ContainsKey(propNameCanon))
            {
                return this.properties[propNameCanon];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.IsAttribute = true;
            sp.Type = SerializablePropertyType.Number;
            this.AddProperty(sp);

            return sp;
        }

        public SerializableProperty EnsureBoolean(String propertyName, String serializationName)
        {
            String propNameCanon = propertyName.ToLowerCase();

            if (this.properties.ContainsKey(propNameCanon))
            {
                return this.properties[propNameCanon];
            }

            if (serializationName == null)
            {
                serializationName = propertyName;
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.IsAttribute = true;
            this.AddProperty(sp);

            return sp;
        }

        public SerializableProperty EnsureString(String propertyName, String serializationName)
        {
            String propNameCanon = propertyName.ToLowerCase();

            if (this.properties.ContainsKey(propNameCanon))
            {
                return this.properties[propNameCanon];
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            sp.IsAttribute = true;
            this.AddProperty(sp);

            return sp;
        }

        public SerializableProperty GetProperty(String propertyName)
        {
            String propNameCanon = propertyName.ToLowerCase();

            if (this.properties.ContainsKey(propNameCanon))
            {
                return this.properties[propNameCanon];
            }

            return null;
        }

        public SerializableProperty EnsureProperty(String propertyName, String serializationName)
        {
            String propNameCanon = propertyName.ToLowerCase();

            if (this.properties.ContainsKey(propNameCanon))
            {
                return this.properties[propNameCanon];
            }

            SerializableProperty sp = new SerializableProperty();
            sp.Name = propertyName;
            this.AddProperty(sp);

            return sp;
        }
    }
}
