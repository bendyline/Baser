/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using Bendyline.Base.ScriptCompatibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Bendyline.Base
{
    public static partial class Utilities
    {
        private readonly static Random random = new Random();
        private static String versionHash;
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

            Context.InitializeCurrent();

            InitializeDebug();

            if (versionHash == null)
            {
                versionHash = String.Format("{0}x{1}x{0}x{2}", AssemblyInfo.BuildNumber, AssemblyInfo.MajorVersion, AssemblyInfo.RevisionNumber);
            }

            initialized = true;
        }

        [Conditional("DEBUG")]
        private static void InitializeDebug()
        {
            versionHash = Utilities.CreateRandomId();
 
            Log.ItemAdded += new LogItemEventHandler(Log_LogItemAdded);
        }
        
        public static void Log_LogItemAdded(object sender, LogItemEventArgs e)
        {
            Debug.WriteLine(e.Item.Message);
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
            return Utilities.CreateRandomIdToLength(8);
        }

        public static int GetDayOfYear(Date compare)
        {
            Date firstDayOfYear = new Date(compare.GetFullYear(), 0, 1);

            Int64 diff = compare.GetTime() - firstDayOfYear.GetTime();

            return Convert.ToInt32(Math.Ceiling((double)(diff / (24 * 60 * 60 * 1000))));
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

        public static String CreateRandomIdToLength(int length)
        {
            String id = String.Empty;

            for (int i = 0; i < length; i++)
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

        public static String GetHMSMFromDateTime(Date dt)
        {
            String hms = String.Empty;

            hms += PrePad(dt.GetHours().ToString(), '0', 2) + ":";
            hms += PrePad(dt.GetMinutes().ToString(), '0', 2) + ":";
            hms += PrePad(dt.GetSeconds().ToString(), '0', 2) + ":";
            hms += PrePad(dt.GetMilliseconds().ToString(), '0', 3);

            return hms;
        }

        public static PropertyChangedEventArgs AllProperties
        {
            get
            {
                return new PropertyChangedEventArgs("*");
            }
        }


        public static String LongToShortString(long number)
        {
            throw new NotImplementedException();
        }

        public static long ShortStringToLong(String shortStringAsNumber)
        {
            throw new NotImplementedException();
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
