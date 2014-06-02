using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;

namespace BL
{
    public delegate void PageErrorEventHandler(object sender, PageErrorEventArgs e);

    public class PageErrorEventArgs : EventArgs
    {
        private PageError pageError;

        public PageError PageError
        {
            get
            {
                return this.pageError;
            }
            set
            {
                this.pageError = value;
            }
        }

        public PageErrorEventArgs(PageError pageError)
        {
            this.pageError = pageError;
        }
    }
}
