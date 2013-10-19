using System;

namespace BL
{
    public class RichContentDocument : RichContentSection
    {
        public RichContentDocument() : base(null)
        {
            this.SetDocument(this);
        }
    }
}
