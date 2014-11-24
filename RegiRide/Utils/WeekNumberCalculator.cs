namespace RegiRide.Utils
{
    using System;
    using System.Globalization;

    public class WeekNumberCalculator
    {
        public short GetWeekNumber(DateTime dateTime)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            int weekNum = currentCulture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return (short)weekNum;
        }
    }
}