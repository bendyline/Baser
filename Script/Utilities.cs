/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace BL
{
    public static partial class Utilities
    {
        private static String versionHash;
    /*    private static String feedbackServicesBaseUrl;
        private static String nationeerBaseUrl;
        private static String nationeerServicesBaseUrl;
        private static String contentBaseUrl;*/
        private static bool initialized;

        public static String GetString(object value)
        {
            return value.ToString();
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
                versionHash = "1.0.0.0";
            }
            /*
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
                contentBaseUrl = "http://BL.com";
            }

            if (nationeerBaseUrl == null)
            {
                nationeerBaseUrl = "http://nationeer.com";
            }*/

            initialized = true;
        }

        [Conditional("DEBUG")]
        private static void InitializeDebug()
        {
            versionHash = Utilities.CreateRandomId();
      /*      feedbackServicesBaseUrl = "http://feedback.nationeerdev.com";
            nationeerBaseUrl = "http://nationeerdev.com";
            nationeerServicesBaseUrl = "http://services.nationeerdev.com";
            contentBaseUrl = "http://nationeerdev.com";
            */
         //               Log.ItemAdded += new LogItemEventHandler(Log_LogItemAdded);
        }
  /*      
        public static void Log_LogItemAdded(object sender, LogItemEventArgs e)
        {
            Debug.WriteLine(e.Item.Message);
        }
   */
        /*
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
        }*/

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


        public static String GetDayName(int dayId)
        {
            switch (dayId)
            {
                case 0:
                    return "Sunday";
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
                default:
                    throw new Exception("");
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
                    throw new Exception("");
            }
        }

        public static String GetHours(Date compare)
        {
            int hours = compare.GetHours();

            if (hours == 0) 
            {
                return "12";
            }
            else if (hours > 12)
            {
                return (hours - 12).ToString();
            }

            return hours.ToString();
        }

        public static String GetFriendlyDateDescription(Date compare)
        {
            Date now = Date.Now;

            long nowTime = (long)now.GetTime() / 1000;
            long compareTime = (long)compare.GetTime() / 1000;

            String minutesStr = compare.GetMinutes().ToString();

            while (minutesStr.Length < 2)
            {
                minutesStr = "0" + minutesStr;
            }

            if (nowTime-compareTime > (long)(300 * 24 * 60 * 60))
            {
                return Utilities.GetMonthName(compare.GetMonth()) + " " + compare.GetDay() + " " + compare.GetFullYear();
            }            
            else if (nowTime-compareTime > (long)(7 * 24 * 60 * 60))
            {
                return Utilities.GetMonthName(compare.GetMonth()) + " " + compare.GetDay();
            }                        
            else if (nowTime-compareTime > (long)(7 * 24 * 60 * 60))
            {
                return Utilities.GetMonthName(compare.GetMonth()) + " " + compare.GetDay();
            }
            else if (nowTime - compareTime > (long)(7 * 24 * 60 * 60))
            {
                return Utilities.GetMonthName(compare.GetMonth()) + " " + compare.GetDay();
            }
            else if (nowTime - compareTime > (long)(1 * 24 * 60 * 60))
            {
                return GetDayName(compare.GetDay()) + " at " + GetHours(compare) + ":" + minutesStr;
            }

            if (compare.GetDay() == now.GetDay())
            {
                return "today at " + GetHours(compare) + ":" + minutesStr;
            }
            else
            {
                return "yesterday at " + GetHours(compare) + ":" + minutesStr;
            }
        }


        public static String CreateRandomId()
        {
            String id = String.Empty;

            for (int i = 0; i < 6; i++)
            {
                int typeSwitch = Math.Random() % 8;

                if (typeSwitch <= 1 && i > 0)
                {
                    id += String.FromCharCode((Math.Random() * 10) + 48);
                }
                else if (typeSwitch <= 4)
                {
                    id += String.FromCharCode((Math.Random() * 26) + 97);
                }
                else
                {
                    id += String.FromCharCode((Math.Random() * 26) + 65);
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

        public static String GetRandomId()
        {
            String id = "";

            for (int i = 0; i < 6; i++ )
            {
                Number choice = Math.Random() * 3;

                if (choice < 1)
                {
                    int index = (int)(Math.Floor(Math.Random() * 26));

                    id += String.FromCharCode(index + 65);
                }
                else if (choice < 2)
                {
                    int index = (int)(Math.Floor(Math.Random() * 26));

                    id += String.FromCharCode(index + 97);
                }
                else 
                {
                    int index = (int)(Math.Floor(Math.Random() * 10));

                    id += String.FromCharCode(index + 48);
                }
            }

            return id;
        }

        public static String GetHMSMFromDateTime(Date dt)
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

            Script.Literal("return temp;");
            return 0;
        }

        public static byte ByteAdd(byte start, byte valueToAdd)
        {
            int temp = start + valueToAdd;

            if (temp > 255)
            {
                temp = 255;
            }

            Script.Literal("return temp;");
            return 0;
        }

    }
}
