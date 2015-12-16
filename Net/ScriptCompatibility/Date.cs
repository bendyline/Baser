using System;

namespace Bendyline.Base.ScriptCompatibility
{
    public class Date
    {
        private DateTime dateTime;

        private static Date empty;

        public static Date Empty
        {
            get
            {
                if (empty == null)
                {
                    empty = new Date(DateTime.MinValue);
                }

                return empty;
            }
        }

        public Date()
        {
            this.dateTime = DateTime.Now;
        }

        public Date(Int32 millisecondsSince1970)
        {
            this.dateTime = new DateTime(1970, 1, 1).AddMilliseconds(millisecondsSince1970);
        }

        public Date(DateTime dateTime)
        {

        }

        public int GetHours()
        {
            return this.dateTime.Hour;
        }

        public int GetMinutes()
        {
            return this.dateTime.Minute;
        }

        public int GetSeconds()
        {
            return this.dateTime.Second;
        }

        public int GetMilliseconds()
        {
            return this.dateTime.Millisecond;
        }

        public int GetDate()
        {
            return this.dateTime.Day;
        }

        public int GetMonth()
        {
            return this.dateTime.Month;
        }

        public int GetDay()
        {
            return (int)this.dateTime.DayOfWeek;
        }

        public int GetTime()
        {
            return Convert.ToInt32(this.dateTime.Subtract(new DateTime(1970,1,1)).TotalMilliseconds);
        }

        public static Date Now
        {
            get
            {
                return new Date();
            }
        }

        public Date ToUtc()
        {
            return new Date(this.dateTime.ToUniversalTime());
        }

        public static Date Parse(String dateTimeString)
        {
            Date d = new Date(DateTime.Parse(dateTimeString));

            return d;
        }
    }
}
