using System;
using System.Runtime.CompilerServices;

namespace Bendyline.Base
{
    public class UserReferenceSet : SerializableObject
    {
        private UserReferenceCollection userReferences;

        [ScriptName("p_userReferences")]
        public UserReferenceCollection UserReferences
        {
            get
            {
                return this.userReferences;
            }
        }

        public UserReferenceSet()
        {
            this.userReferences = new UserReferenceCollection();
        }
    }
}
