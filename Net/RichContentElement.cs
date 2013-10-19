using System;
using System.Net;


namespace Bendyline.Base
{
    public abstract class RichContentElement : SerializableObject
    {
        private ColorDefinition foreground;
        private ColorDefinition background;
        private RichContentContainer container;
        private RichContentDocument document;


        public abstract String Text
        {
            get;
            set;
        }
        
        public ColorDefinition Foreground
        {
            get
            {
                return this.foreground;
            }
            set
            {
                this.foreground = value;
            }
        }

        public ColorDefinition Background
        {
            get
            {
                return this.background;
            }
            set
            {
                this.background = value;
            }
        }

        public RichContentContainer Container
        {
            get
            {
                return this.container;
            }
        }

        public RichContentDocument Document
        {
            get
            {
                if (this.document == null)
                {
                    this.document = this.container.Document;
                }

                return this.document;
            }
        }

        public RichContentElement(RichContentContainer container)
        {
            this.container = container;
        }

        internal void SetDocument(RichContentDocument document)
        {
            this.document = document;
        }

        protected override void InitializeForSerialization()
        {
            base.InitializeForSerialization();

            this.SerializableType.EnsureObject("Foreground", null);
            this.SerializableType.EnsureObject("Background", null);
        }
    }
}
