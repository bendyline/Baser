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


        /// <summary>
        /// NOTE NOTE: On the server, Id is a long.  But on the client, it's a string comprised of UniqueKey.
        /// The client should never see longs.
        /// </summary>
        [ScriptName("s_id")]
        public String Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.id == value)
                {
                    return;
                }

                this.id = value;

                this.NotifyPropertyChanged("Id");
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
                if (this.nickName == value)
                {
                    return;
                }

                this.nickName = value;

                this.NotifyPropertyChanged("NickName");
            }
        }
    }
}
