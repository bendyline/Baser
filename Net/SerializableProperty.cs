/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public partial class SerializableProperty
    {
        private bool isAttribute;
        private String name;
        private String propertyToAddObjectTo;
        private String serializationName;
        private String containerNodeName;
        private String itemName;
        private SerializablePropertyType propertyType = SerializablePropertyType.String;
        private bool isComplex = false;
        private bool isSerializableObject = false;
        private Type itemType;

        public String SerializationName
        {
            get
            {
                if (this.serializationName == null)
                {
                    return this.name;
                }

                return this.serializationName;
            }

            set
            {
                this.serializationName = value;
            }
        }

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
    }
}
