/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace Bendyline.Base
{
    public delegate void LoginEventHandler(object sender, LoginEventArgs e);

    public class LoginEventArgs : EventArgs
    {
        private String baseUrl;
        private AuthenticatorStatus status;

        public String BaseUrl
        {
            get
            {
                return this.baseUrl;
            }

            set
            {
                this.baseUrl = value;
            }
        }


        public AuthenticatorStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
            }
        }

        public LoginEventArgs(String baseUrl, AuthenticatorStatus status)
        {
            this.baseUrl = baseUrl;
            this.status = status;
        }
    }
}
