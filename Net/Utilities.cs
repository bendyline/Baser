using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bendyline.Base
{
    public static partial class Utilities
    {
        private readonly static Random random = new Random();
        private static String versionHash;
        private static String feedbackServicesBaseUrl;
        private static String nationeerBaseUrl;
        private static String nationeerServicesBaseUrl;
        private static String contentBaseUrl;
        private static bool initialized;
        private static IAuthenticator currentAuthenticator;


        public static IAuthenticator CurrentAuthenticator
        {
            get
            {
                return currentAuthenticator;
            }

            set
            {
                currentAuthenticator = value;
            }
        }


        public static void Initialize()
        {
            if (initialized)
            {
                return;
            }

            InitializeDebug();

            if (versionHash == null)
            {
                versionHash = String.Format("{0}x{1}x{0}x{2}", AssemblyInfo.BuildNumber, AssemblyInfo.MajorVersion, AssemblyInfo.RevisionNumber);
            }

            if (feedbackServicesBaseUrl == null)
            {
                feedbackServicesBaseUrl = "http://feedback.nationeer.com";
            }

            if (nationeerServicesBaseUrl == null)
            {
                nationeerServicesBaseUrl = "http://services.nationeer.com";
            }

            if (contentBaseUrl == null)
            {
                contentBaseUrl = "http://bendyline.com";
            }

            if (nationeerBaseUrl == null)
            {
                nationeerBaseUrl = "http://nationeer.com";
            }

            initialized = true;
        }

        [Conditional("DEBUG")]
        private static void InitializeDebug()
        {
            versionHash = Utilities.CreateRandomId();
            feedbackServicesBaseUrl = "http://feedback.nationeerdev.com";
            nationeerBaseUrl = "http://nationeerdev.com";
            nationeerServicesBaseUrl = "http://services.nationeerdev.com";
            contentBaseUrl = "http://nationeerdev.com";

                        Log.ItemAdded += new LogItemEventHandler(Log_LogItemAdded);
        }
        
        public static void Log_LogItemAdded(object sender, LogItemEventArgs e)
        {
            Debug.WriteLine(e.Item.Message);
        }
        

        public static String FeedbackServicesBaseUrl
        {
            get
            {
                if (feedbackServicesBaseUrl == null)
                {
                    Initialize();
                }

                return feedbackServicesBaseUrl;
            }
        }

        public static String NationeerServicesBaseUrl
        {
            get
            {
                if (nationeerServicesBaseUrl == null)
                {
                    Initialize();
                }

                return nationeerServicesBaseUrl;
            }
        }

        public static String NationeerBaseUrl
        {
            get
            {
                if (nationeerBaseUrl == null)
                {
                    Initialize();
                }

                return nationeerBaseUrl;
            }
        }

        public static String ContentBaseUrl
        {
            get
            {
                return contentBaseUrl;
            }
        }

        public static String VersionHash
        {
            get
            {
                if (versionHash == null)
                {
                    Initialize();
                }

                return versionHash;
            }
        }


        public static String GetMonthName(int monthId)
        {
            switch (monthId)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    throw new Exception();
            }
        }

        public static String CreateRandomId()
        {
            String id = String.Empty;

            for (int i = 0; i < 8; i++)
            {
                int typeSwitch = random.NextInRange(5);

                if (typeSwitch <= 1)
                {
                    id += Convert.ToChar(random.NextInRange(26) + 65);
                }
                else
                    if (typeSwitch <= 3)
                    {
                        id += Convert.ToChar(random.NextInRange(26) + 97);
                    }
                    else
                    {
                        id += Convert.ToChar(random.NextInRange(10) + 48);
                    }
            }

            return id;
        }


        public static String PrePad(String str, char c, int length)
        {
            while (str.Length < length)
            {
                str = c + str;
            }

            return str;
        }

        public static String GetHMSMFromDateTime(DateTime dt)
        {
            String hms = String.Empty;

            hms += PrePad(dt.GetHours().ToString(), '0', 2) + ":";
            hms += PrePad(dt.GetMinutes().ToString(), '0', 2) + ":";
            hms += PrePad(dt.GetSeconds().ToString(), '0', 2) + ":";
            hms += PrePad(dt.GetMilliseconds().ToString(), '0', 3);

            return hms;
        }

        public static byte ByteSubtract(byte start, byte valueToSubtract)
        {
            int temp = start - valueToSubtract;

            if (temp < 0)
            {
                temp = 0;
            }

            return Convert.ToByte(temp);
        }

        public static byte ByteAdd(byte start, byte valueToAdd)
        {
            int temp = start + valueToAdd;

            if (temp > 255)
            {
                temp = 255;
            }

            return Convert.ToByte(temp);
        }

    }
}
