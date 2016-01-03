/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace Bendyline.Base
{
    public class User : SerializableObject
    {
        private String loginName;
        private String name;
        private String uniqueKey;
        private String nickName;
        private long? id;

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

        public bool IsLoaded
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public String UniqueKey
        {
            get
            {
                return this.uniqueKey;
            }

            set
            {
                this.uniqueKey = value;
            }
        }

        public long? Id
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
