/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

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
