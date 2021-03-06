﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Xml;

namespace Bendyline.Base
{
    public interface ISerializableObject
    {
        String Xml { get; set; }

        void ReadXml(XmlReader reader);
        void WriteXml(XmlWriter writer);
        void WriteXmlWithTagName(XmlWriter writer, String tagName);
    }
}
