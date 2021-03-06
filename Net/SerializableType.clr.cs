﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Diagnostics;
using System.Collections;

namespace Bendyline.Base
{
    public partial class SerializableType
    {

        private void EnsurePropertyLocal(SerializableProperty sp)
        {
            PropertyInfo pi = this.type.GetProperty(sp.Name);

            if (pi == null)
            {
                throw new InvalidOperationException(String.Format("Unsupported property {0} found", sp.Name));
            }

            sp.PropertyInfo = pi;
        }
        /// <summary>
        /// Begins initialization of the serializable type.  Should be called before properties are ensured.
        /// </summary>
        public void BeginInit()
        {

        }
        /// <summary>
        /// Initializes the serializable type and loads attributes from the actual type instance.  Should be called
        /// after all types have been initialized.
        /// </summary>
        public void EndInit()
        {

        }

        public void EnsureBaseProperties(SerializableObject serializableObject)
        {
            Type t = serializableObject.GetType();

            PropertyInfo[] props = t.GetProperties();

            foreach (PropertyInfo propertyInfo in props)
            {
                object[] attributes = propertyInfo.GetCustomAttributes(true);

                foreach (object o in attributes)
                {
                    if (o is SerializablePropertyAttribute)
                    {
                        SerializablePropertyAttribute spa = (SerializablePropertyAttribute)o;

                        String propertyName = propertyInfo.Name;

                        if (spa != null)
                        {
                            propertyName = spa.Name;
                        }

                        Type propertyType = propertyInfo.PropertyType;

                        switch (propertyType.Name)
                        {
                            case "String":
                                this.EnsureString(propertyInfo.Name, propertyName);
                                break;

                            default:
                                throw new NotImplementedException();
                                break;
                        }
                    }
                }
            }
        }

        internal void ReadXml(object so, XmlReader reader)
        {
            int initialDepth = reader.Depth;

            this.ReadAttributeXml(so, reader, initialDepth);

            XmlUtilities.ReadToInterior(initialDepth, reader);

            this.ReadInnerXml(so, reader, initialDepth);
        }

        internal void ReadAttributeXml(object so, XmlReader reader, int initialDepth)
        {
            foreach (SerializableProperty sp in this.properties.Values)
            {
                if (sp.IsAttribute && !sp.IsComplex)
                {
                    if (reader.MoveToAttribute(sp.SerializationName))
                    {
                        String val = reader.Value;

                        this.SetProperty(so, sp, val);
                    }
                }
            }
        }

        internal void ReadInnerXml(object so, XmlReader reader, int initialDepth)
        {
            object activeContainer = null;
            SerializableProperty activeContainerProperty = null;
            String activeItemName = null;

            while (reader.Depth > initialDepth && !reader.EOF)
            {
                bool readNode = false;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    String nodeName = reader.Name;                   

                    if (this.properties.ContainsKey(nodeName))
                    {
                        SerializableProperty sp = this.properties[nodeName];

                        if (sp.Type == SerializablePropertyType.Collection)
                        {
                            activeContainerProperty = sp;
                            activeItemName = sp.ItemName;

                            if (sp.PropertyToAddObjectTo != null)
                            {
                                activeContainer = sp.PropertyInfo.GetValue(so, null);
                            }
                            else
                            {
                                activeContainer = so;
                            }
                        }
                        else if (!sp.IsAttribute)
                        {
                            if (sp.IsSerializableObject)
                            {
                                object item = sp.PropertyInfo.GetValue(so, null);

                                if (item is ISerializableObject)
                                {
                                    ((SerializableObject)item).ReadXml(reader);
                                    readNode = true;
                                }

                            }
                        }
                    }
                    else if (activeItemName == nodeName)
                    {
                        object o = Activator.CreateInstance(activeContainerProperty.ItemType);

                        if (o is ISerializableObject)
                        {
                            ((ISerializableObject)o).ReadXml(reader);
                            readNode = true;
                        }

                        Type t = activeContainer.GetType();

                        MethodInfo mi = t.GetMethod("Add", new Type[] { activeContainerProperty.ItemType });

                        Debug.Assert(mi != null, "Could not find an Add method for a collection.");

                        if (mi != null)
                        {
                            mi.Invoke(activeContainer, new object[] { o });
                        }
                    }

                }

                if (!readNode)
                {
                    reader.Read();
                }
            }

        }

        internal void WriteXml(object so, XmlWriter writer)
        {
            this.WriteXml(so, writer, this.type.Name);
        }

        internal void WriteXml(object so, XmlWriter writer, String tagName)
        {
            writer.WriteStartElement(tagName);

            this.WriteAttributeXml(so, writer);
            this.WriteInnerXml(so, writer);

            writer.WriteEndElement();
        }

        internal void WriteAttributeXml(object so, XmlWriter writer)
        {
            foreach (SerializableProperty sp in this.properties.Values)
            {
                if (sp.IsAttribute && !sp.IsComplex)
                {
                    writer.WriteAttributeString(sp.SerializationName, this.GetPropertyAsString(so, sp));
                }
            }
        }

        internal void WriteInnerXml(object so, XmlWriter writer)
        {
            foreach (SerializableProperty sp in this.properties.Values)
            {
                Type propertyType = sp.PropertyInfo.PropertyType;

                if (sp.IsSerializableObject)
                {
                    // writer.WriteStartElement(sp.Name);

                    object result = sp.PropertyInfo.GetValue(so, null);

                    if (result is ISerializableObject)
                    {
                        ((ISerializableObject)result).WriteXmlWithTagName(writer, sp.SerializationName);
                    }

                    // writer.WriteEndElement();
                }
                else
                    if (sp.IsComplex)
                    {
                        if (propertyType.GetInterface("IEnumerable", false) != null)
                        {
                            if (sp.ContainerNodeName != null)
                            {
                                writer.WriteStartElement(sp.ContainerNodeName);
                            }

                            object result = sp.PropertyInfo.GetValue(so, null);

                            if (result != null && result is IEnumerable)
                            {
                                IEnumerator ie = ((IEnumerable)result).GetEnumerator();

                                while (ie.MoveNext())
                                {
                                    object item = ie.Current;

                                    if (item is ISerializableObject)
                                    {
                                        ((ISerializableObject)item).WriteXml(writer);
                                    }
                                }
                            }

                            if (sp.ContainerNodeName != null)
                            {
                                writer.WriteEndElement();
                            }
                        }

                    }
                    else
                        if (!sp.IsAttribute)
                        {
                            writer.WriteElementString(sp.SerializationName, this.GetPropertyAsString(so, sp));
                        }
            }
        }

        internal void SetProperty(object so, String property, object value)
        {
            if (this.properties.ContainsKey(property))
            {
                this.SetProperty(so, this.properties[property], value);
                return;
            }
            else if (this.propertiesBySerializationName.ContainsKey(property))
            {
                this.SetProperty(so, this.propertiesBySerializationName[property], value);
                return;
            }

            Debug.Assert(false, String.Format("Expected property '{0}' not found.", property));
        }

        internal void SetProperty(object so, SerializableProperty property, object value)
        {
            if (value is String)
            {
                String strValue = (String)value;

                if (property.PropertyInfo.PropertyType.IsEnum)
                {
                    property.SetToObject(so, Enum.Parse(property.PropertyInfo.PropertyType, strValue, true));
                }
                else
                {
                    switch (property.PropertyInfo.PropertyType.Name)
                    {
                        case "Boolean":
                            property.SetToObject(so, Convert.ToBoolean(strValue));
                            break;

                        case "Int32":
                            property.SetToObject(so, Convert.ToInt32(strValue));
                            break;

                        case "Double":
                            property.SetToObject(so, Convert.ToDouble(strValue));
                            break;

                        case "String":
                            property.SetToObject(so, value);
                            break;

                        default:
                            Debug.Assert(false, "Unsupported type being set:" + property.PropertyInfo.PropertyType.Name);
                            break;
                    }
                }
            }
            else
            {
                Debug.Assert(false, "Unsupported property type set.");
            }
        }

        private object GetPropertyAsString(object so, String property)
        {
            object result = GetProperty(so, property);

            return this.GetStringFromObject(result);
        }

        private object GetProperty(object so, String property)
        {
            if (this.properties.ContainsKey(property))
            {
                return this.GetProperty(so, this.properties[property]);
            }

            Debug.Assert(false, String.Format("Expected property '{0}' not found.", property));

            return null;
        }

        private String GetPropertyAsString(object so, SerializableProperty property)
        {
            object result = property.GetFromObject(so);

            return this.GetStringFromObject(result);
        }

        private String GetStringFromObject(object o)
        {
            if (o == null)
            {
                return String.Empty;
            }

            return o.ToString();
        }

        private object GetProperty(object so, SerializableProperty property)
        {
            return property.GetFromObject(so);
        }

    }
}
