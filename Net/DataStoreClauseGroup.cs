using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public class DataStoreClauseGroup
    {
        private List<DataStoreClause> clauses;
        private List<DataStoreClauseGroup> groups;

        public IList<DataStoreClauseGroup> Groups
        {
            get
            {
                return this.groups;
            }
        }

        public IList<DataStoreClause> Clauses
        {
            get
            {
                return this.clauses;
            }
        }

        public DataStoreClauseGroup()
        {
            this.clauses = new List<DataStoreClause>();
            this.groups = new List<DataStoreClauseGroup>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            foreach (DataStoreClause dsc in this.clauses)
            {

            }

            foreach (DataStoreClauseGroup dsg in this.groups)
            {
                sb.Append(dsg.ToString());
            }

            sb.Append("]");
            return sb.ToString();
        }
    }
}
