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
        private static bool initialized;

        private static RegularExpression EmailValidator;
        public static String GetString(object value)
        {
            return value.ToString();
        }

        public static PropertyChangedEventArgs AllProperties
        {
            get
            {
                return new PropertyChangedEventArgs("*");
            }
        }

        public static bool IsValidEmail(String email)
        {
            int indexOfAt = email.IndexOf("@");

            if (email.Length < 6 || indexOfAt <= 0)
            {
                return false;
            }

            if (EmailValidator == null)
            {
                Script.Literal(@"
{0}=/^[a-zA-Z0-9.!#$%&'*+/=?^_`{{|}}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{{0,61}}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{{0,61}}[a-zA-Z0-9])?)*$/", EmailValidator);                    
            }

            if (!EmailValidator.Test(email))
            {
                return false;
            }

            String suffix = email.Substring(indexOfAt, email.Length);

            int period = suffix.LastIndexOf(".");

            if (period <= 0)
            {
                return false;
            }

            return true;
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

            initialized = true;
        }

        [Conditional("DEBUG")]
        private static void InitializeDebug()
        {
            versionHash = Utilities.CreateRandomId();
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

        public static Date ConvertToUtc(Date original)
        {
            return new Date(Date.UTC(original.GetFullYear(), original.GetMonth(), original.GetDate(), original.GetHours(), original.GetMinutes(), original.GetSeconds(), original.GetMilliseconds()));
        }

        public static void ExecuteUnsafeFunction(Action a)
        {
            Script.Literal("if (typeof MSApp  !== \"undefined\" && typeof MSApp.execUnsafeLocalFunction !== \"undefined\") {{ MSApp.execUnsafeLocalFunction({0}); }} else {{ {0}(); }}", a);
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

        public static String GetMonthName(int zeroBasedMonthId)
        {
            switch (zeroBasedMonthId)
            {
                case 0:
                    return "January";
                case 1:
                    return "February";
                case 2:
                    return "March";
                case 3:
                    return "April";
                case 4:
                    return "May";
                case 5:
                    return "June";
                case 6:
                    return "July";
                case 7:
                    return "August";
                case 8:
                    return "September";
                case 9:
                    return "October";
                case 10:
                    return "November";
                case 11:
                    return "December";
                default:
                    throw new Exception("");
            }
        }

        public static String GetShortMonthName(int zeroBasedMonthId)
        {
            switch (zeroBasedMonthId)
            {
                case 0:
                    return "Jan";
                case 1:
                    return "Feb";
                case 2:
                    return "Mar";
                case 3:
                    return "Apr";
                case 4:
                    return "May";
                case 5:
                    return "Jun";
                case 6:
                    return "Jul";
                case 7:
                    return "Aug";
                case 8:
                    return "Sep";
                case 9:
                    return "Oct";
                case 10:
                    return "Nov";
                case 11:
                    return "Dec";
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

        public static String LongToShortString(long number)
        {
            String result = null;
            Script.Literal("{0}=IntToStr.fromInt({1})", result, number);

            Debug.Assert(ShortStringToLong(result) == number);

            return result;
        }

        public static long ShortStringToLong(String shortStringAsNumber)
        {
            long result = 0;
            Script.Literal("{0}=IntToStr.toInt({1})", result, shortStringAsNumber);
            return result;
        }

        public static int GetDayOfYear(Date compare)
        {
            Date firstDayOfYear = new Date(compare.GetFullYear(), 0, 1);

            Int64 diff = compare.GetTime() - firstDayOfYear.GetTime();

            return Math.Ceil(diff / (24 * 60 * 60 * 1000));
        }

        public static String GetFriendlyDateDescription(Date compare)
        {
            Date now = Date.Now;

            if (compare.GetTimezoneOffset() != now.GetTimezoneOffset())
            {
                compare.SetTime(compare.GetTime() + ((now.GetTimezoneOffset() - compare.GetTimezoneOffset()) * 60 * 1000));
            }

            long nowTime = (long)now.GetTime() / 1000;
            long compareTime = (long)compare.GetTime() / 1000;

            String minutesStr = compare.GetMinutes().ToString();

            while (minutesStr.Length < 2)
            {
                minutesStr = "0" + minutesStr;
            }

            int hours = compare.GetHours();

            String hoursStr = null;
            String ampmStr = null;

            if (hours > 12)
            {
                hoursStr = (hours - 12).ToString();
                ampmStr = "pm";
            }
            else if (hours == 12)
            {
                hoursStr = "12";
                ampmStr = "pm";
            }
            else if (hours == 0)
            {
                hoursStr = "12";
                ampmStr = "am";
            }
            else
            {
                hoursStr = (hours).ToString();
                ampmStr = "am";
            }

            if (nowTime - compareTime < (long)(3 * 60))
            {
                return "just now";
            }
            else if (nowTime - compareTime < (long)(90 * 60))
            {
                return ((int)Math.Floor((nowTime - compareTime) / 60)).ToString() + " minutes ago";
            }
            else if (nowTime - compareTime < (long)(320 * 60))
            {
                hours = Math.Floor((nowTime - compareTime) / (60 * 60));
                int minutes = Math.Floor( ((nowTime - compareTime) / 60) % 60);

                if (hours == 1)
                {
                    return "1 hour " + minutes + " minutes ago";
                }
                else
                {
                    return hours + " hours " + minutes + " minutes ago";
                }
            }
            else if (nowTime - compareTime > (long)(300 * 24 * 60 * 60))
            {
                return Utilities.GetMonthName(compare.GetMonth()) + " " + (compare.GetDate()).ToString() + " " + compare.GetFullYear();
            }            
            else if (nowTime-compareTime > (long)(7 * 24 * 60 * 60))
            {
                return Utilities.GetMonthName(compare.GetMonth()) + " " + (compare.GetDate()).ToString();
            }                        
            else if (nowTime-compareTime > (long)(7 * 24 * 60 * 60))
            {
                return Utilities.GetMonthName(compare.GetMonth()) + " " + (compare.GetDate()).ToString();
            }
            else if (nowTime - compareTime > (long)(7 * 24 * 60 * 60))
            {
                return Utilities.GetMonthName(compare.GetMonth()) + " " + (compare.GetDate()).ToString();
            }
            else if (nowTime - compareTime > (long)(1 * 24 * 60 * 60))
            {
                return GetStaticDateTimeValue(compare);
            }

            if (compare.GetDate() == now.GetDate())
            {
                return "today at " + hoursStr + ":" + minutesStr + " " + ampmStr;
            }
            else
            {
                return "yesterday at " + hoursStr + ":" + minutesStr + " " + ampmStr;
            }
        }

        public static String GetStaticDateTimeValue(Date date)
        {
            String minutesStr = date.GetMinutes().ToString();
            int hours = date.GetHours();

            while (minutesStr.Length < 2)
            {
                minutesStr = "0" + minutesStr;
            }

            String ampmStr = null;

            if (hours >= 12)
            {
                ampmStr = "pm";
            }
            else
            {
                ampmStr = "am";
            }
           
            return GetStaticDateValue(date) + " at " + GetHours(date) + ":" + minutesStr + " " + ampmStr;
        }

        public static String GetStaticDateValue(Date date)
        {      
            return GetDayName(date.GetDay()) + " " + (date.GetMonth() + 1) + "/" + date.GetDate();
        }

        public static Number GenerateSeededRandom(Number seed)
        {
            Number n = Math.Sin(seed++) * 10000;

            n = n - Math.Floor(n);

            return n;
        }

        public static String CreateRandomId()
        {
            return CreateRandomIdToLength(6);
        }

        public static String CreateRandomIdToLength(int length)
        {
            String id = String.Empty;

            for (int i = 0; i < length; i++)
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

            Script.Literal("return {0};", temp);
            return 0;
        }

        public static byte ByteAdd(byte start, byte valueToAdd)
        {
            int temp = start + valueToAdd;

            if (temp > 255)
            {
                temp = 255;
            }

            Script.Literal("return {0};", temp);
            return 0;
        }

    }
}
