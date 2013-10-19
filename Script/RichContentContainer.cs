using System;
using System.Collections.Generic;

namespace BL
{
    public abstract class RichContentContainer : RichContentElement
    {
        private readonly List<RichContentElement> elements;
        private bool isUniform = false;
        private bool uniformityDetermined = false;
        private String text;

        public List<RichContentElement> Elements
        {
            get
            {
                return this.elements;
            }
        }

        public override String Text
        {
            get
            {
                if (this.text == null)
                {
                    StringBuilder textBuilder = new StringBuilder();

                    foreach (RichContentElement element in this.elements)
                    {
                        textBuilder.Append(element.Text);
                    }

                    this.text = textBuilder.ToString();
                }

                return this.text;
            }

            set
            {
                this.elements.Clear();

                RichContentRun run = new RichContentRun(this);
                run.Text = value;

                this.elements.Add(run);
            }
        }

        public bool IsUniform
        {
            get
            {
                if (!this.uniformityDetermined)
                {
                    String prevailingFormatHash = null;

                    this.isUniform = true;

                    foreach (RichContentElement element in this.elements)
                    {
                        if (element is RichContentContainer)
                        {
                            this.isUniform = false;
                            break;
                        }
                        else if (element is RichContentRun)
                        {
                            RichContentRun runToCompare = (RichContentRun)element;

                            if (prevailingFormatHash == null)
                            {
                                prevailingFormatHash = runToCompare.FormatHash;
                            }
                            else if (runToCompare.FormatHash != prevailingFormatHash)
                            {
                                this.isUniform = false;
                                break;
                            }
                        }
                    }

                    this.uniformityDetermined = true;
                }

                return this.isUniform;
            }
        }

        public RichContentContainer(RichContentContainer container)
            : base(container)
        {
            this.elements = new List<RichContentElement>();
        }

        public RichContentRun AddText(String text)
        {
            RichContentRun rcr = new RichContentRun(this);
            
            rcr.Text = text;

            this.elements.Add(rcr);

            return rcr;
        }

        public RichContentRun AddLink(String text, String linkId)
        {
            RichContentRun rcr = new RichContentRun(this);
            
            rcr.Text = text;
            rcr.IsLink = true;
            rcr.LinkId = linkId;
            
            this.elements.Add(rcr);

            return rcr;
        }
        /*
        protected override void ReadInnerXml(System.Xml.XmlReader reader, int initialDepth)
        {
            this.elements.Clear();
            this.uniformityDetermined = false;
            this.text = null;

            while (reader.Depth > initialDepth && !reader.EOF)
            {
                bool readNode = false;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    String nodeName = reader.Name;

                    switch (nodeName.ToLowerCase())
                    {
                        case "Section":
                            RichContentSection rcs = new RichContentSection(this);
                            rcs.ReadXml(reader);
                            this.elements.Add(rcs);
                            readNode = true;
                            break;

                        case "Run":
                            RichContentRun rcr = new RichContentRun(this);
                            rcr.ReadXml(reader);
                            this.elements.Add(rcr);
                            readNode = true;
                            break;
                    }
                }
                else if (reader.NodeType == XmlNodeType.Text)
                {
                    RichContentRun rcr = new RichContentRun(this);

                    rcr.Text = reader.Value;

                    this.elements.Add(rcr);
                }

                if (!readNode)
                {
                    reader.Read();
                }
            }
        }*/
    }
}
