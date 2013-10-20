/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public class LoginData : SerializableObject
    {
        private String userName;
        private String password;

        public String UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                this.userName = value;
            }
        }

        public String Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
            }
        }

        protected override void InitializeForSerialization()
        {
            base.InitializeForSerialization();

            this.SerializableType.EnsureString("UserName", "UserName");
            this.SerializableType.EnsureString("Password", "Password");
        }

    }
}
