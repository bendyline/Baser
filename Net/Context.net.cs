using System;
using System.Configuration;
using System.Diagnostics;

namespace Bendyline.Base
{
    public partial class Context
    {
        private void LoadFromConfiguration()
        {
            if (ConfigurationManager.AppSettings["feedbackUrl"] != null)
            {
                feedbackUrl = ConfigurationManager.AppSettings["feedbackUrl"];
            }

            if (ConfigurationManager.AppSettings["secondaryFeedbackUrl"] != null)
            {
       //         secondaryFeedbackUrl = ConfigurationManager.AppSettings["secondaryFeedbackUrl"];
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

    }
}
