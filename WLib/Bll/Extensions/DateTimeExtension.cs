using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Bll.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ToDateTime(this DateTimeOffset dateTime)
        {
            if (dateTime.Offset.Equals(TimeSpan.Zero))
                return dateTime.UtcDateTime;

            if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
                return DateTime.SpecifyKind(dateTime.DateTime, DateTimeKind.Local);

            return dateTime.DateTime;
        }

        public static DateTime GetFirstMonthDate(this DateTime month)
        {
            return new DateTime(month.Year, month.Month, 1, 0, 0, 0);
        }

        public static DateTime GetLastMonthDate(this DateTime month)
        {
            return GetFirstMonthDate(month.AddMonths(1)).AddMilliseconds(-1);
        }
    }
}
