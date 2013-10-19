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
