using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bendyline.Base;

namespace Bendyline.Base
{
    public class AuthenticationResult : SerializableObject
    {
        private String cookieName;
        private int errorCode;
        private int timeoutSeconds;
        private String cookie;

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
