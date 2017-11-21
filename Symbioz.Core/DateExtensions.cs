using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Core
{
    public static class DateExtensions
    {
        public static double DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalMilliseconds;
        }

        public static int DateTimeToUnixTimestampSeconds(this DateTime dateTime)
        {
            return (int)(dateTime - new DateTime(1970, 1, 1, 1,0,0).ToLocalTime()).TotalSeconds;
        }
        public static string ToFileNameDate(this DateTime dateTime)
        {
            return string.Format("{0}-{1}-{2}_{3}h{4}m{5}s", dateTime.Day, dateTime.Month, dateTime.Year, dateTime.Hour,
                dateTime.Minute, dateTime.Second);
        }
        public static DateTime UnixTimestampToDateTime(this double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime UnixTimestampToDateTime(this int unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static double ConvertToMinutes(this double milliseconds)
        {
            return milliseconds * 0.0000166667;
        }
    }

}