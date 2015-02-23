﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Html;

namespace BL
{
    public enum DevicePlatform
    {
        Unknown = 0,
        iOS = 1
    }

    /// <summary>
    /// Provides all general, ambient, app agnostic app state, plus helper functions to determine properties of the browser.
    /// </summary>
    public class Context 
    {
        private String parsedUserAgent = null;
        private static Context current = new Context();
        private String versionHash;
        private String userName;
        private String feedbackUrl;
        private int expId;
        private int tokenId;

        private DevicePlatform devicePlatform;

        private bool isSmallFormFactor = false;
        private bool isHostedInApp = false;
        private bool isTouchOnly = false;
        private bool isTablet = false;

        private String resourceBasePath = null;
        private String webServiceBasePath = null;
        private String userContentBasePath = null;
        private User user;
        private IAppObjectProvider objectProvider;
        private String initialHash = null;
        private String activeHash = null;

        public event PropertyChangedEventHandler UserChanged;
        public event StringEventHandler InternalNavigationChanged;

        private PropertyChangedEventHandler userPropertyChanged; 


        public DevicePlatform DevicePlatform
        {
            get
            {
                return this.devicePlatform;
            }
        }

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

        public bool IsTablet
        {
            get
            {
                return this.isTablet;
            }
        }
        public bool IsTouchOnly
        {
            get
            {
                // this is a hack for the HTML5Assist debugger. It boots the page with one user agent but then switches it
                // to the emulated UA later.  So if this looks like a mobile device but our initial UA didn't indicate that, then
                // try evaluating the UA one more time, since it may have changed.
                if (this.parsedUserAgent != Window.Navigator.UserAgent)
                {
                    this.ParseUserAgent();
                }

                return this.isTouchOnly;
            }

            // note: this is set in JS in the app.
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

            // note: this is set in JS in the app.
            set
            {
                this.isHostedInApp = value;
            }
        }

        public bool UsingBrowserStackNavigation
        {
            get
            {
                // using browser-based navigation on iOS causes left-page-swipe and right-page-swipe behaviors to 
                // show up so we don't want to use it there.
                return Context.Current.DevicePlatform != DevicePlatform.iOS;
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

            // note: this is set in JS in the app.
            set
            {
                this.isSmallFormFactor = value;
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

        public String FeedbackUrl
        {
            get
            {
                if (this.feedbackUrl == null)
                {
                    this.feedbackUrl = String.Empty;
                }

                return this.feedbackUrl;
            }

            set
            {
                this.feedbackUrl = value;
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
            Window.AddEventListener("hashchange", this.HandleHashChange);

            if (this.initialHash == null)
            {
                this.initialHash = Window.Location.Hash.Substring(1, Window.Location.Hash.Length).ToLowerCase();
            }

            this.userPropertyChanged = new PropertyChangedEventHandler(this.HandleUserPropertyChanged);

            this.ParseUserAgent();
        }

        public void NavigateToHome()
        {
            if (Context.Current.UsingBrowserStackNavigation)
            {
                Window.History.Go(-1);
            }
            else
            {
                this.NavigateInternal(null);
            }
        }

        private void HandleHashChange(ElementEvent e)
        {
            this.FireInternalNavigationEvent();
        }

        private void FireInternalNavigationEvent()
        {
            if (this.InternalNavigationChanged != null)
            {
                StringEventArgs sea = new StringEventArgs(this.GetInternalDestination());

                this.InternalNavigationChanged(this, sea);
            }
        }

        private void ParseUserAgent()
        {
            this.parsedUserAgent = Window.Navigator.UserAgent;

            String userAgent = this.parsedUserAgent.ToLowerCase();

            if (userAgent.IndexOf("iphone") >= 0)
            {
                this.devicePlatform = DevicePlatform.iOS;
            }

            // per comment at https://developer.mozilla.org/en-US/docs/Browser_detection_using_the_user_agent
            if (userAgent.IndexOf("mobi") >= 0)
            {
                if (userAgent.IndexOf("ipad") >= 0 || userAgent.IndexOf("tablet") >= 0)
                {
                    this.isTablet = true;
                }
                else
                {
                    this.isSmallFormFactor = true;
                }

                this.isTouchOnly = true;
            }
            // identify android tablets via user agent containing android but NOT mobile. 
            else if ((userAgent.IndexOf("android") >= 0 || userAgent.IndexOf("silk") >= 0) && userAgent.IndexOf("mobi") < 0)
            {
                this.isTablet = true;
                this.isTouchOnly = true;
            }
            else if (Window.InnerWidth < 600 || Window.InnerHeight < 400)
            {
                this.isSmallFormFactor = true;
            }
        }

        public bool IsHomeNavigation()
        {
            String currentHash = this.GetActiveInternalDestination();

            if (String.IsNullOrEmpty(currentHash) || currentHash == initialHash.ToLowerCase())
            {            
                return true;
            }

            return false;
        }

        public void NavigateInternal(String internalDestination)
        {
            String hash = internalDestination;

            if (hash != null && initialHash.ToLowerCase() == hash.ToLowerCase())
            {
                hash += ".1";
            }

            if (!Context.Current.UsingBrowserStackNavigation)
            {
                activeHash = hash;

                this.FireInternalNavigationEvent();
            }
            else
            {
                activeHash = null;

                // this should implicitly fire the hash change event, and therefore the internal navigation event.
                Window.Location.Hash = hash;
            }
        }
        public String GetActiveInternalDestination()
        {
            String hashCanon = null;

            if (this.activeHash != null)
            {
                hashCanon = this.activeHash.ToLowerCase();
            }
            else
            {
                hashCanon = Window.Location.Hash.ToLowerCase();

                hashCanon = hashCanon.Substring(1, hashCanon.Length);
            }

            return hashCanon;
        }


        public String GetInternalDestination()
        {
            String hashCanon = this.GetActiveInternalDestination();

            int lastPeriod = hashCanon.LastIndexOf(".");

            if (lastPeriod > 0)
            {
                hashCanon = hashCanon.Substring(0, lastPeriod);
            }

            return hashCanon;
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

        public static void SetSite(String resourceBasePath, String webServiceBasePath, String userContentBasePath, String versionHash, String feedbackUrl)
        {
            Context pc = Context.Current;

            pc.ResourceBasePath = resourceBasePath;
            pc.WebServiceBasePath = webServiceBasePath;
            pc.UserContentBasePath = userContentBasePath;
            pc.VersionToken = versionHash;
            pc.FeedbackUrl = feedbackUrl;
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
