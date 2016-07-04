using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Bendyline.Base
{
    public partial class Context
    {
        private Dictionary<String, User> usersByUniqueKey;
        private Dictionary<long, User> usersById;

        private String feedbackUrl;
        private String contentBasePath;
        private String resourceBasePath;
        private String secondaryResourceBasePath;
        private String webServiceBasePath;
        private String userUniqueKey;
        private String userContentBasePath;
        private User user;
        private long tokenId;
        private IAppObjectProvider objectProvider;
        private long? userId;
        private bool isAuthenticated = false;
        private String versionToken;
        private static Context current;
        private String imageResourceSubPath;
        public event PropertyChangedEventHandler UserChanged;

        private PropertyChangedEventHandler userPropertyChanged;


        public String ImageResourceSubPath
        {
            get
            {
                return this.imageResourceSubPath;
            }

            set
            {
                this.imageResourceSubPath = value;
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

        public long? UserId
        {
            get
            {
                return this.userId;
            }

            set
            {
                this.userId = value;
            }
        }

        public String VersionToken
        {
            get
            {
                return this.versionToken;
            }
        }

        public static Context Current
        {
            get
            {
                return current;
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
                        this.User = this.EnsureUserByUniqueKey(this.userUniqueKey);
                    }
                }
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
                    this.UserChanged(this, Utilities.AllProperties);
                }
            }
        }

        public event BooleanEventHandler AuthenticatedChanged;

        public bool IsAuthenticated
        {
            get
            {
                return this.isAuthenticated;
            }

            set
            {
                if (this.isAuthenticated == value)
                {
                    return;
                }

                this.isAuthenticated = value;

                if (this.AuthenticatedChanged != null)
                {
                    BooleanEventArgs bea = new BooleanEventArgs(value);

                    this.AuthenticatedChanged(this, bea);
                }
            }
        }


        public String ResourceBasePath
        {
            get
            {
                String path = this.resourceBasePath;

                if (path == null)
                {
                    return null;
                }

                String version = AssemblyInfo.BuildNumber.ToString();

                while (version.Length < 4)
                {
                    version = "0" + version;
                }

                path = path.Replace("%SCRIPTBUILD%", "b" + version);

                return path;
            }

            set
            {
                this.resourceBasePath = value;
            }
        }

        public String SecondaryResourceBasePath
        {
            get
            {
                String path = this.secondaryResourceBasePath;

                if (path == null)
                {
                    return null;
                }

                String version = AssemblyInfo.BuildNumber.ToString();

                while (version.Length < 4)
                {
                    version = "0" + version;
                }

                path = path.Replace("%SCRIPTBUILD%", "b" + version);

                return path;
            }

            set
            {
                this.secondaryResourceBasePath = value;
            }
        }

        public String EffectiveSecondaryResourceBasePath
        {
            get
            {
                if (this.secondaryResourceBasePath == null)
                {
                    return this.ResourceBasePath;
                }

                return this.SecondaryResourceBasePath;
            }
        }

        public String ContentBasePath
        {
            get
            {
                return this.contentBasePath;
            }

            set
            {
                this.contentBasePath = value;
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

        public String FeedbackUrl
        {
            get
            {
                return feedbackUrl;
            }
        }

        public Context()
        {
            this.Initialize();
        }

        public void SetUserUniqueKeyAndId(String uniqueKey, Nullable<long> id)
        {
            this.userUniqueKey = uniqueKey;
            this.userId = id;

            if (id != null || uniqueKey != null)
            {
                User u = this.EnsureUserByUniqueKey(this.userUniqueKey);
                u.Id = id;

                if (id != null)
                {
                    if (this.usersById[(long)id] == null)
                    {
                        this.usersById[(long)id] = this.User;
                    }
                }

                this.User = u;
            }
            else
            {
                this.User = null;
            }
        }

        public User EnsureUserByUniqueKey(String userUniqueKey)
        {
            if (this.usersByUniqueKey[userUniqueKey] == null)
            {
                User user = new User();

                user.UniqueKey = userUniqueKey;

                this.usersByUniqueKey[userUniqueKey] = user;

                return user;
            }

            return this.usersByUniqueKey[userUniqueKey];
        }

        public User EnsureUserById(long userId)
        {
            if (this.usersById[userId] == null)
            {
                User user = new User();

                user.Id = userId;

                this.usersById[userId] = user;

                return user;
            }

            return this.usersById[userId];
        }


        private void Initialize()
        {
            this.InitializeDebug();

            this.versionToken = null;

            this.usersByUniqueKey = new Dictionary<string, User>();
            this.usersById = new Dictionary<long, User>();

            this.LoadFromConfiguration();

            if (feedbackUrl == null)
            {
                feedbackUrl = String.Empty;
            }

            if (contentBasePath == null)
            {
                contentBasePath = String.Empty;
            }
        }

        [Conditional("DEBUG")]
        private void InitializeDebug()
        {
            contentBasePath = "http://localhost:120";
        }
        
        internal static void InitializeCurrent()
        {
            current = new Context();
        }
    }
}
