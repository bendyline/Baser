/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Html;

namespace BL
{
    public class Context 
    {
        private static Context current = new Context();
        private String versionHash;
        private String userName;
        private int expId;
        private int tokenId;

        private bool isSmallFormFactor = false;
        private bool isHostedInApp = false;
        private bool isTouchOnly = false;

        private String resourceBasePath = null;
        private String webServiceBasePath = null;
        private String userContentBasePath = null;
        private User user;
        private IAppObjectProvider objectProvider;

        public event PropertyChangedEventHandler UserChanged;

        private PropertyChangedEventHandler userPropertyChanged; 

        public IAppObjectProvider ObjectProvider
        {
            get
            {
                return this.objectProvider;
            }

            set
            {
                this.objectProvider = value;
            }
        }

        public User User
        {
            get
            {
                return this.user;
            }

            set
            {
                if (this.user == value)
                {
                    return;
                }
                
                if (this.user != null)
                {
                    this.user.PropertyChanged -= this.userPropertyChanged;
                }

                this.user = value;

                if (this.user != null && !String.IsNullOrEmpty(this.user.NickName))
                {
                    this.user.IsLoaded = true;
                }

                UserManager.Current.AddUser(this.user);

                this.User.PropertyChanged += this.userPropertyChanged;

                if (this.UserChanged != null)
                {
                    this.UserChanged(this, PropertyChangedEventArgs.All);
                }
            }
        }


        public bool IsTouchOnly
        {
            get
            {
                return this.isTouchOnly;
            }

            set
            {
                this.isTouchOnly = value;
            }
        }

        public bool IsHostedInApp
        {
            get
            {
                return this.isHostedInApp;
            }

            set
            {
                this.isHostedInApp = true;
            }
        }

        public bool IsPortrait
        {
            get
            {
                return (Window.InnerHeight > Window.InnerWidth);
            }
        }

        public bool IsSmallFormFactor
        {
            get
            {
                return this.isSmallFormFactor;
            }

            set
            {
                this.isSmallFormFactor = true;
            }
        }

        public String ResourceBasePath
        {
            get
            {
                if (this.resourceBasePath == null)
                {
                    this.resourceBasePath = String.Empty;
                }

                return this.resourceBasePath;
            }

            set
            {
                this.resourceBasePath = value;
            }
        }
        public String VersionToken
        {
            get
            {
                if (this.versionHash == null)
                {
                    this.versionHash = String.Empty;
                }

                return this.versionHash;
            }

            set
            {
                this.versionHash = value;
            }
        }

        public String WebServiceBasePath
        {
            get
            {
                if (this.webServiceBasePath == null)
                {
                    this.webServiceBasePath = String.Empty;
                }

                return this.webServiceBasePath;
            }

            set
            {
                this.webServiceBasePath = value;
            }
        }

        public String UserContentBasePath
        {
            get
            {
                if (this.userContentBasePath == null)
                {
                    this.userContentBasePath = String.Empty;
                }

                return this.userContentBasePath;
            }

            set
            {
                this.userContentBasePath = value;
            }
        }

        public int Tokenid
        {
            get
            {
                return this.tokenId;
            }

            set
            {
                this.tokenId = value;
            }
        }

        public int ExpId
        {
            get
            {
                return this.expId;
            }

            set
            {
                this.expId = value;
            }
        }

        public static Context Current
        {
            get
            {
                return current;
            }
        }

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

        public Context()
        {
            String userAgent = Window.Navigator.UserAgent.ToLowerCase();

            this.userPropertyChanged = new PropertyChangedEventHandler(this.HandleUserPropertyChanged);

            // per comment at https://developer.mozilla.org/en-US/docs/Browser_detection_using_the_user_agent
            if (userAgent.IndexOf("mobi") >= 0)
            {

                if (userAgent.IndexOf("ipad") >= 0 || userAgent.IndexOf("tablet") >= 0)
                {
                    
                }
                else
                {
                    this.isSmallFormFactor = true;
                }
                this.isTouchOnly = true;
            }
        }

        private void HandleUserPropertyChanged(object sender, PropertyChangedEventArgs pchea)
        {
            if (this.UserChanged != null)
            {
                this.UserChanged(sender, pchea);
            }
        }

        public User EnsureUser()
        {
            if (this.User == null)
            {
                this.User = new User();
            }

            return this.User;
        }

        public static void SetSite(String resourceBasePath, String webServiceBasePath, String userContentBasePath, String versionHash)
        {
            Context pc = Context.Current;

            pc.ResourceBasePath = resourceBasePath;
            pc.WebServiceBasePath = webServiceBasePath;
            pc.UserContentBasePath = userContentBasePath;
            pc.VersionToken = versionHash;
        }

        public static void SetSession(int tokenId, int expId, String userName)
        {
            Context pc = Context.Current;

            pc.ExpId = expId;
            pc.Tokenid = tokenId;
            pc.UserName = userName;
        }
    }
}
