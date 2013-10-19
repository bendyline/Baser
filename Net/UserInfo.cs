using System;

namespace Bendyline.Base
{
    public class UserInfo
    {
        private String loginName;
        private String name;
        private int id;

        public int Id
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

        public String Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        public String LoginName
        {
            get
            {
                return this.loginName;
            }

            set
            {
                this.loginName = value;
            }
        }
    }
}
