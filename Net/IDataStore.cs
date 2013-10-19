using System;
using System.Collections.Generic;

namespace Bendyline.Base
{
    public interface IDataStore
    {
        ICollection<IDataStoreList> Lists { get; }
        String Name { get; }
        String Location { get; set; }
        bool RequiresAuthentication { get; set; }

        IDataStoreList AssumeList(String listName);
    }
}
