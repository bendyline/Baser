/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;

namespace BL
{
    public partial class SerializableProperty
    {
        private bool isAttribute = true;
        private String name;
        private String propertyToAddObjectTo;
        private String containerNodeName;
        private String itemName;
        private SerializablePropertyType propertyType = SerializablePropertyType.String;
        private bool isComplex = false;
        private bool isSerializableObject = false;
        private Type itemType;
        
        public Type ItemType
        {
            get
            {
                return this.itemType;
            }

            set
            {
                this.itemType = value;
            }
        }

        public String ContainerNodeName
        {
            get
            {
                return this.containerNodeName;
            }

            set
            {
                this.containerNodeName = value;
            }
        }

        public String PropertyToAddObjectTo
        {
            get
            {
                return this.propertyToAddObjectTo;
            }
            set
            {
                this.propertyToAddObjectTo = value;
            }
        }

        public SerializablePropertyType Type
        {
            get
            {
                return this.propertyType;
            }

            set
            {
                this.propertyType = value;
            }
        }

        public String ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
            }
        }

        public bool IsSerializableObject
        {
            get
            {
                return isSerializableObject;
            }
        }

        /// <summary>
        /// Gets/sets whether this serializable property should be serialized as an attribute on a node.
        /// </summary>
        public bool IsAttribute
        {
            get
            {
                return this.isAttribute;
            }
            set
            {
                this.isAttribute = value;
            }
        }

        /// <summary>
        /// Indicates whether the property is 'complex' -- i.e., is a complex type or a collection of items.
        /// </summary>
        public bool IsComplex
        {
            get
            { 
                return this.isComplex;
            }
        }

        /// <summary>
        /// Name MUST be equivalent to its JavaScript serialization and set_ property name.  It should always be camel cased.
        /// </summary>
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private void Initialize()
        {
        }

        public char GetShortTypeName()
        {
            switch (this.propertyType)
            {
                case SerializablePropertyType.String:
                    return 's';

                case SerializablePropertyType.Date:
                    return 'd';

                case SerializablePropertyType.SmallInteger:
                    return 'y';

                case SerializablePropertyType.Integer:
                    return 'i';

                case SerializablePropertyType.Object:
                    return 'o';

                case SerializablePropertyType.Bool:
                    return 'b';

                case SerializablePropertyType.Number:
                    return 'n';

                case SerializablePropertyType.UrlString:
                    return 'u';

            }

            return '@';
        }

        public void SetTypeByShortName(char shortName)
        {
            switch (shortName)
            {
                case 's':
                    this.propertyType = SerializablePropertyType.String;
                    break;
                case 'u':
                    this.propertyType = SerializablePropertyType.UrlString;
                    break;
                case 'i':
                    this.propertyType = SerializablePropertyType.Integer;
                    break;
                case 'y':
                    this.propertyType = SerializablePropertyType.SmallInteger;
                    break;
                case 'd':
                    this.propertyType = SerializablePropertyType.Date;
                    break;
                case 'n':
                    this.propertyType = SerializablePropertyType.Number;
                    break;
                case 'o':
                    this.propertyType = SerializablePropertyType.Object;
                    break;
                case 'b':
                    this.propertyType = SerializablePropertyType.Bool;
                    break;
            }
        }

        internal void SetToObject(object so, object value)
        {
        }

        internal object GetFromObject(object so)
        {
            return null;
        }
    }
}
