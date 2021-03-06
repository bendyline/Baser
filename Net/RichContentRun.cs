﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Text;

namespace Bendyline.Base
{
    public class RichContentRun : RichContentElement
    {
        private bool isLink;
        private String linkId;
        private String text;

        public override String Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

        public bool IsLink
        {
            get
            {
                return this.isLink;
            }

            set
            {
                this.isLink = value;
            }
        }

        public String LinkId
        {
            get
            {
                return this.linkId;
            }

            set
            {
                this.linkId = value;
            }
        }

        public String FormatHash
        {
            get
            {
                StringBuilder formatHash = new StringBuilder();

                formatHash.Append("<F");

                if (this.linkId != null)
                {
                    formatHash.Append(" LinkId=\"" + this.linkId + "\"");
                }

                if (this.isLink == true)
                {
                    formatHash.Append(" IsLink=\"true\"");
                }

                formatHash.Append(">");

                if (this.Foreground != null)
                {
                    formatHash.Append(this.Foreground.Xml);
                }

                if (this.Background != null)
                {
                    formatHash.Append(this.Background.Xml);
                }

                formatHash.Append("</F>");

                return formatHash.ToString();
            }
        }

        public RichContentRun(RichContentContainer container) : base(container)
        {
        }

        /*
        protected override void ReadInnerXml(System.Xml.XmlReader reader, int initialDepth)
        {
            this.text = String.Empty;

            while (reader.Depth > initialDepth && !reader.EOF)
            {
                if (reader.NodeType == System.Xml.XmlNodeType.Text || reader.NodeType == System.Xml.XmlNodeType.SignificantWhitespace)
                {
                    this.text += reader.Value;
                }

                reader.Read();
            }
        }*/

    }
}
