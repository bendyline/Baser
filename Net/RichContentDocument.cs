using System;

namespace Bendyline.Base
{
    public class RichContentDocument : RichContentSection
    {
        public RichContentDocument() : base(null)
        {
            this.SetDocument(this);
        }
    }
}
