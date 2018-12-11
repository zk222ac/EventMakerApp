using System;

namespace EventMakerApp.Converter
{
    class DateTimeConverter
    {
        public static DateTime DateTimeOffsetAndTimeSetToDateTime(DateTimeOffset date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);
        }
    }
}
