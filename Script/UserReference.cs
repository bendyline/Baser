using System;
using System.Runtime.CompilerServices;

namespace BL
{
    public enum UserReferenceType
    {
        StructuredUser = 0,
        Adhoc = 1
    }

    /// <summary>
    /// Specifies an abstract, implementation agnostic definition of a reference to a user.
    /// </summary>
    public class UserReference : SerializableObject
    {
        private Nullable<long> id;
        private String uniqueKey;
        private String nickName;
        private UserReferenceType type;

        [ScriptName("i_type")]
        public UserReferenceType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
      
        public Nullable<long> Id
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

        [ScriptName("s_uniqueKey")]
        public String UniqueKey
        {
            get
            {
                return this.uniqueKey;
            }
            set
            {
                if (this.uniqueKey == value)
                {
                    return;
                }

                this.uniqueKey = value;

                this.NotifyPropertyChanged("UniqueKey");
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
