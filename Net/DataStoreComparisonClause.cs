using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public abstract class DataStoreComparisonClause : DataStoreClause
    {
        private String fieldName;
        private object value;

        public String FieldName
        {
            get
            {
                return this.fieldName; 
            }

            set
            {
                this.fieldName = value;
            }
        }

        public object Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }

        }

        protected String GetStringValue()
        {
            if (value is String)
            {
                return String.Format("\"{0}\"", value);
            }

            return value.ToString();
        }
    }
}
