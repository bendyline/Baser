/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Html;
using System.Net;

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
        private String userUniqueKey;
        private String feedbackUrl;
        private String secondaryFeedbackUrl;
        private int expId;
        private Nullable<long> userId;
        private long tokenId;
        private String scriptLibraryTemplate;

        private String mapKey = "pk.eyJ1IjoiYmVuZHltaWtlIiwiYSI6ImVkNmNlMzAyNjJiMzE2NzFiZTA1ODY5ZTQzYWJiOTgwIn0.9BmBOyDQplc6-XsttHIzgA";
        private String mapId = "bendymike.a26dbfd9";

        private DevicePlatform devicePlatform;

        private bool isSmallFormFactor = false;
        private bool isHostedInApp = false;
        private bool isTouchOnly = false;
        private bool isTablet = false;
        private Nullable<bool> isFullScreenWebApp;

        private String resourceBasePath = null;
        private String imageResourceSubPath = null;
        private String webServiceBasePath = null;
        private String userContentBasePath = null;
        private User user;
        private IAppObjectProvider objectProvider;
        private String initialHash = null;
        private String activeHash = null;
        private Operation userSignoutOperation = null;

        private int lastNavigateTime = -1;
        private int onScreenKeyboardHeight = 0;
        private bool isAtRoot = true;
        public event PropertyChangedEventHandler UserChanged;
        public event StringEventHandler InternalNavigationChanged;

        private PropertyChangedEventHandler userPropertyChanged; 

        public int OnScreenKeyboardHeight
        {
            get
            {
                return this.onScreenKeyboardHeight;
            }

            set
            {
                this.onScreenKeyboardHeight = value;
            }
        }

        public bool IsOnscreenKeyboardDevice
        {
            get
            {
                return this.IsTouchOnly;
            }
        }

        public String MapKey
        {
            get
            {
                return this.mapKey;
            }

            set
            {
                this.mapKey = value;
            }
        }

        public String MapId
        {
            get
            {
                return this.mapId;
            }

            set
            {
                this.mapId = value;
            }
        }

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

                if (this.user != null)
                {
                    UserManager.Current.AddUser(this.user);

                    this.User.PropertyChanged += this.userPropertyChanged;
                }

                if (this.UserChanged != null)
                {
                    this.UserChanged(this, PropertyChangedEventArgs.All);
                }
            }
        }

        public String ScriptLibraryTemplate
        {
            get
            {
                return this.scriptLibraryTemplate;
            }

            set
            {
                this.scriptLibraryTemplate = value;
            }
        }

        public bool IsTablet
        {
            get
            {
                return this.isTablet;
            }
        }

        public int FullScreenTopBufferHeight
        {
            get
            {
                if (this.IsFullScreenWebApp)
                {
                    return 18;
                }

                return 0;
            }
        }

        public bool IsFullScreenWebApp
        {
            get
            {
                if (this.isFullScreenWebApp == null)
                {
                    if (Context.Current.IsHostedInApp)
                    {
                        this.isFullScreenWebApp = true;
                    }

                    Script.Literal("{0}=((\"standalone\" in window.navigator) && window.navigator.standalone)", this.isFullScreenWebApp);
                }

                return (bool)isFullScreenWebApp;
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
                return (Context.Current.BrowserInnerHeight >  Context.Current.BrowserInnerWidth);
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

        public String ImageResourceSubPath
        {
            get
            {
                if (this.imageResourceSubPath == null)
                {
                    this.imageResourceSubPath = String.Empty;
                }

                return this.imageResourceSubPath;
            }

            set
            {
                this.imageResourceSubPath = value;
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

        public String SecondaryFeedbackUrl
        {
            get
            {
                if (this.secondaryFeedbackUrl == null)
                {
                    this.secondaryFeedbackUrl = String.Empty;
                }

                return this.secondaryFeedbackUrl;
            }

            set
            {
                this.secondaryFeedbackUrl = value;
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

        public int BrowserInnerHeight
        {
            get
            {       
                return Math.Min(Window.Screen.Height, Window.InnerHeight);
            }
        }
        public int BrowserInnerWidth
        {
            get
            {
                return Math.Min(Window.Screen.Width, Window.InnerWidth);
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

        public long TokenId
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

        public String UserUniqueKey
        {
            get
            {
                if (this.user != null)
                {
                    return this.user.UniqueKey;
                }

                return this.userUniqueKey;
            }

            set
            {
                if (this.userUniqueKey == value)
                {
                    return;
                }

                this.userUniqueKey = value;

                if ((this.user == null && this.userUniqueKey != null) || (this.user != null && this.userUniqueKey != this.user.UniqueKey))
                {
                    if (this.userUniqueKey == null)
                    {
                        this.User = null;
                    }
                    else
                    {
                        this.EnsureUserByUniqueKey(this.userUniqueKey);
                    }
                }
            }
        }

        public Nullable<long> UserId
        {
            get
            {
                return this.userId;
            }

            set
            {
                if (this.userId== value)
                {
                    return;
                }

                this.userId= value;
            }
        }

        public Context()
        {
            Window.AddEventListener("hashchange", this.HandleHashChange);

            if (this.initialHash == null)
            {
                this.initialHash = Window.Location.Hash.Substring(1, Window.Location.Hash.Length).ToLowerCase();
            }

            if (!String.IsNullOrEmpty(Window.Location.Hash))
            {
                this.isAtRoot = false;
            }

            this.userPropertyChanged = new PropertyChangedEventHandler(this.HandleUserPropertyChanged);

            this.ParseUserAgent();
        }


        public void SignoutUser(AsyncCallback callback, object state)
        {
            bool isNew = false;

            if (this.userSignoutOperation == null)
            {
                this.userSignoutOperation = new Operation();
                isNew = true;
            }

            this.userSignoutOperation.CallbackStates.Add(CallbackState.Wrap(callback, state));

            if (isNew)
            {
                XmlHttpRequest xhr = new XmlHttpRequest();

                WebRequest.SendWithCredentials(xhr);

                this.userSignoutOperation.Tag = xhr;
                String endpoint = UrlUtilities.EnsurePathEndsWithSlash(Context.Current.WebServiceBasePath) + "api/signout/";

                xhr.Open("POST", endpoint);
                xhr.SetRequestHeader("Accept", "application/json");
                xhr.SetRequestHeader("Content-Type", "application/json");

                xhr.OnReadyStateChange = new Action(this.SignoutUserContinue);
                xhr.Send("");
            }
        }

        private void SignoutUserContinue()
        {
            XmlHttpRequest xhr = (XmlHttpRequest)this.userSignoutOperation.Tag;

            if (xhr != null && xhr.ReadyState == ReadyState.Loaded)
            {
                this.TokenId = -1;
                this.userUniqueKey = null;
                this.User = null;

                this.userSignoutOperation.CompleteAsAsyncDone(this);
                this.userSignoutOperation = null;
            }
        }

        public void NavigateToHome()
        {
            int now = Date.Now.GetTime();

            if (now > this.lastNavigateTime + 400)
            {
                if (Context.Current.UsingBrowserStackNavigation)
                {
                    if (!this.isAtRoot)
                    {
                        this.isAtRoot = true;
                        Window.History.Go(-1);
                    }
                }
                else
                {
                    this.NavigateInternal(null);
                }

                this.lastNavigateTime = now;
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
                String internalDestination = this.GetInternalDestination();

                if (internalDestination != null && internalDestination.Length > 2)
                {
                    this.isAtRoot = false;
                }

                StringEventArgs sea = new StringEventArgs(internalDestination);

                this.InternalNavigationChanged(this, sea);
            }
        }

        private void ParseUserAgent()
        {
            this.parsedUserAgent = Window.Navigator.UserAgent;

            String userAgent = this.parsedUserAgent.ToLowerCase();

            if (userAgent.IndexOf("iphone") >= 0 || userAgent.IndexOf("ipad") >= 0)
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

                this.onScreenKeyboardHeight = Window.InnerHeight / 2;

                this.isTouchOnly = true;
            }
            // identify android tablets via user agent containing android but NOT mobile. 
            else if ((userAgent.IndexOf("android") >= 0 || userAgent.IndexOf("silk") >= 0) && userAgent.IndexOf("mobi") < 0)
            {
                this.isTablet = true;

                this.onScreenKeyboardHeight = Window.InnerHeight / 3;

                this.isTouchOnly = true;
            }

            if (Window.InnerWidth < 760 || Window.InnerHeight < 470)
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
            int now = Date.Now.GetTime();

            if (now > this.lastNavigateTime + 400)
            {
                this.lastNavigateTime = now;

                String hash = internalDestination;

                if (hash != null)
                {
                    hash = "!" + hash;
                }

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

            if (hashCanon.CharAt(0) == '!')
            {
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

        public User EnsureUserByUniqueKey(String userId)
        {
            if (this.User == null)
            {
                User user = new User();

                user.UniqueKey = userId;
                this.userUniqueKey = userId;

                this.User = user;
            }

            return this.User;
        }


        public static void SetSite(String resourceBasePath, String webServiceBasePath, String userContentBasePath, String versionHash, String feedbackUrl, String secondaryFeedbackUrl, String scriptLibraryTemplate)
        {
            Context pc = Context.Current;

            pc.ResourceBasePath = resourceBasePath;
            pc.WebServiceBasePath = webServiceBasePath;
            pc.UserContentBasePath = userContentBasePath;
            pc.VersionToken = versionHash;
            pc.FeedbackUrl = feedbackUrl;
            pc.SecondaryFeedbackUrl = secondaryFeedbackUrl;
            pc.ScriptLibraryTemplate = scriptLibraryTemplate;

            // Script.Literal("requirejs.config({{paths:{{bl:{0} }} }});", resourceBasePath);
        }

        public static void SetSession(int tokenId, int expId, String userKey, Nullable<long> userId)
        {
            Context pc = Context.Current;

            pc.ExpId = expId;
            pc.TokenId = tokenId;
            pc.UserUniqueKey = userKey;
            pc.UserId = userId;
        }
    }
}
