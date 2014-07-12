using System;
using System.Runtime.CompilerServices;

namespace BL
{
    /// <summary>
    /// Specifies an abstract, implementation agnostic definition of a reference to a user.
    /// </summary>
    public class UserReference : SerializableObject
    {
        private String id;
        private String nickName;

        [ScriptName("s_id")]
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
        [ScriptName("s_nickName")]
        public String NickName
        {
            get
            {
                return this.nickName;
            }
            set
            {
                this.nickName = value;
            }
        }
    }
}
