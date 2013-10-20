/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public class DataStoreQuery : DataStoreClauseGroup
    {
        private int rowLimit = 1000;

        public int RowLimit
        {
            get
            {
                return this.rowLimit;
            }

            set
            {
                this.rowLimit = value;
            }
        }
    }
}
