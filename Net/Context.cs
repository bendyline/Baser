using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public class Context
    {
        private String feedbackUrl;
        private String secondaryFeedbackUrl;
        private String contentBasePath;
        private String resourceBasePath;
        private String webServiceBasePath;
        private String userContentBasePath;

        private static Context current;

        public static Context Current
        {
            get
            {
                return current;
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

        private void LoadFromConfiguration()
        {
            if (ConfigurationManager.AppSettings["feedbackUrl"] != null)
            {
                feedbackUrl = ConfigurationManager.AppSettings["feedbackUrl"];
            }

            if (ConfigurationManager.AppSettings["secondaryFeedbackUrl"] != null)
            {
                secondaryFeedbackUrl = ConfigurationManager.AppSettings["secondaryFeedbackUrl"];
            }

            if (ConfigurationManager.AppSettings["webServiceBasePath"] != null)
            {
                webServiceBasePath = ConfigurationManager.AppSettings["webServiceBasePath"];
            }

            if (ConfigurationManager.AppSettings["userContentBasePath"] != null)
            {
                userContentBasePath = ConfigurationManager.AppSettings["userContentBasePath"];
            }

            if (ConfigurationManager.AppSettings["contentBasePath"] != null)
            {
                contentBasePath = ConfigurationManager.AppSettings["contentBasePath"];
            }

            if (ConfigurationManager.AppSettings["resourceBasePath"] != null)
            {
                resourceBasePath = ConfigurationManager.AppSettings["resourceBasePath"];
            }

            if (ConfigurationManager.AppSettings["assetsBaseUrl"] != null)
            {
                resourceBasePath = ConfigurationManager.AppSettings["assetsBaseUrl"];
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
