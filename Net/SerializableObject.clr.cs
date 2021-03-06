﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Bendyline.Base
{
    public partial class SerializableObject : INotifyPropertyChanged, ISerializableObject
    {

        [XmlIgnore]
        public String Xml
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                XmlWriterSettings xws = new XmlWriterSettings()
                {
                Indent = false,
                NewLineHandling = NewLineHandling.None,
                NewLineOnAttributes = false,
                OmitXmlDeclaration = true
                };

                using (XmlWriter xw = XmlWriter.Create(sb, xws))
                {
                    this.WriteXml(xw);

               //     xw.Close();
                }

                return sb.ToString();
            }

            set
            {
                Debug.Assert(!String.IsNullOrEmpty(value), "value is null or empty.");

                using (StringReader sr = new StringReader(value))
                {
                    using (XmlReader xr = XmlReader.Create(sr))
                    {
                        XmlUtilities.ReadToInterior(0, xr);
                        this.ReadXml(xr);
                    }
                }
            }
        }

        public SerializableObject()
        {
            this.EnsureInitializedForSerialization();
        }

        public void ReadXml(XmlReader reader)
        {
            this.EnsureInitializedForSerialization();

            int initialDepth = reader.Depth;

            this.ReadAttributeXml(reader, initialDepth);

            XmlUtilities.ReadToInterior(initialDepth, reader);

            this.ReadInnerXml(reader, initialDepth);

            this.OnPostSerialization();
        }

        protected virtual void ReadAttributeXml(XmlReader reader, int initialDepth)
        {
            this.serializableType.ReadAttributeXml(this, reader, initialDepth);
        }

        protected virtual void ReadInnerXml(XmlReader reader, int initialDepth)
        {
            this.serializableType.ReadInnerXml(this, reader, initialDepth);
        }

        public void WriteXml(XmlWriter writer)
        {
            this.EnsureInitializedForSerialization();

            this.serializableType.WriteXml(this, writer);
        }

        public void WriteXmlWithTagName(XmlWriter writer, String tagName)
        {
            this.EnsureInitializedForSerialization();

            this.serializableType.WriteXml(this, writer, tagName);
        }


        protected virtual void OnPostSerialization()
        {

        }

        public override String ToString()
        {
            return this.Xml;
        }
    }
}
