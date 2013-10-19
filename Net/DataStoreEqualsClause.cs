using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public class DataStoreEqualsClause : DataStoreComparisonClause
    {

        public override string ToString()
        {
            return "{" + this.FieldName + "=" + this.GetStringValue() + "}";
        }
    }
}
