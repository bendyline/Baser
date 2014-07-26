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
