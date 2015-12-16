/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace Bendyline.Base
{
    /// <summary>
    /// Result and state of an authentication request.
    /// </summary>
    public class AuthenticationResult : SerializableObject
    {
        private String cookieName;
        private int errorCode;
        private int timeoutSeconds;
        private String cookie;

        /// <summary>
        /// Name of the authentication cookie received from authentication.
        /// </summary>
        public String CookieName
        {
            get
            {
                return this.cookieName;
            }

            set
            {
                this.cookieName = value;
            }
        }

        /// <summary>
        /// Error code, if received, for an authentication request.
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return this.errorCode; 
            }

            set
            {
                this.errorCode = value;
            }
        }

        /// <summary>
        /// Cookie timeout, in seconds. (or less than zero of no explicit timeout.)
        /// </summary>
        public int TimeoutSeconds
        {
            get
            {
                return this.timeoutSeconds;
            }

            set
            {
                this.timeoutSeconds = value;
            }
        }

        /// <summary>
        /// Value of the authentication cookie received.
        /// </summary>
        public String Cookie
        {
            get
            {
                return this.cookie;
            }

            set
            {
                this.cookie = value;
            }
        }

        /// <summary>
        /// Initializes serializable properties.
        /// </summary>
        protected override void InitializeForSerialization()
        {
            base.InitializeForSerialization();

            this.SerializableType.EnsureString("Cookie", "Cookie");
            this.SerializableType.EnsureString("CookieName", "CookieName");
            this.SerializableType.EnsureInteger("ErrorCode", "ErrorCode");
            this.SerializableType.EnsureInteger("TimeoutSeconds", "TimeoutSeconds");
        }
    }
}
