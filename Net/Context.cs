using System;
using System.Diagnostics;

namespace Bendyline.Base
{
    public partial class Context
    {
        private String feedbackUrl;
        private String secondaryFeedbackUrl;
        private String contentBasePath;
        private String resourceBasePath;
        private String secondaryResourceBasePath;
        private String webServiceBasePath;
        private String userContentBasePath;
        private long tokenId;
        private long userId;
        private bool isAuthenticated = false;
        private String versionToken;
        private static Context current;

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

        public long UserId
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
        public String SecondaryFeedbackUrl
        {
            get
            {
                return secondaryFeedbackUrl;
            }
        }

        public Context()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.InitializeDebug();

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
