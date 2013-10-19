using System;

namespace Bendyline.Base
{
    public delegate void IdEventHandler(object sender, IdEventArgs e);

    public class IdEventArgs : EventArgs
    {
        private String id;

        public String Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        public IdEventArgs(String id)
        {
            this.id = id;
        }
    }
}
