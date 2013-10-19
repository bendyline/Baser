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
            PropertyInfo pi = this.type.GetTypeInfo().GetDeclaredProperty(sp.Name);

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
                    if (reader.MoveToAttribute(sp.Name))
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
                        else
                            if (!sp.IsAttribute)
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
                    else
                        if (activeItemName == nodeName)
                        {
                            object o = Activator.CreateInstance(activeContainerProperty.ItemType);

                            if (o is ISerializableObject)
                            {
                                ((ISerializableObject)o).ReadXml(reader);
                                readNode = true;
                            }

                            Type t = activeContainer.GetType();

                            MethodInfo mi = t.GetTypeInfo().GetDeclaredMethod("Add");

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
            this.WriteXml(so, writer, this.type.GetTypeInfo().Name);
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
                    writer.WriteAttributeString(sp.Name, this.GetPropertyAsString(so, sp));
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
                        ((ISerializableObject)result).WriteXmlWithTagName(writer, sp.Name);
                    }

                    // writer.WriteEndElement();
                }
                else
                    if (sp.IsComplex)
                    {
                        throw new NotImplementedException();
                      //  if (propertyType.GetTypeInfo().GetInterface("IEnumerable", false) != null)
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
                            writer.WriteElementString(sp.Name, this.GetPropertyAsString(so, sp));
                        }
            }
        }

        private void SetProperty(object so, String property, object value)
        {
            if (this.properties.ContainsKey(property))
            {
                this.SetProperty(so, this.properties[property], value);
            }

            Debug.Assert(false, String.Format("Expected property '{0}' not found.", property));
        }

        private void SetProperty(object so, SerializableProperty property, object value)
        {
            if (value is String)
            {
                String strValue = (String)value;

                switch (property.PropertyInfo.PropertyType.GetTypeInfo().Name)
                {
                    case "Boolean":
                        property.SetToObject(so, Convert.ToBoolean(strValue));

                        break;

                    case "Int32":
                        property.SetToObject(so, Convert.ToInt32(strValue));
                        break;

                    case "String":
                        property.SetToObject(so, value);
                        break;

                    default:
                        Debug.Assert(false, "Unsupported type being set:" + property.PropertyInfo.PropertyType.GetTypeInfo().Name);
                        break;
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
