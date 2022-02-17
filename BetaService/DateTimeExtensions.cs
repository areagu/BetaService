using System;
namespace BetaService
{
    public static class DateTimeExtensions
    {
        public static int GetBusinessDays(this DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new NotSupportedException("ERROR: [startDate] cannot be greater than [endDate].");

            int cnt = 0;
            for (var current = startDate; current < endDate; current = current.AddDays(1))
            {
                if (current.DayOfWeek == DayOfWeek.Saturday || current.DayOfWeek == DayOfWeek.Sunday)
                {
                    // skip
                }
                else cnt++;
            }
            return cnt;
        }

        public static DateTime AddBusinessDays(this DateTime current, int days)
        {
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                }
                while (current.DayOfWeek == DayOfWeek.Saturday || current.DayOfWeek == DayOfWeek.Sunday);
            }
            return current;
        }

        public static DateTime SubtractBusinessDays(this DateTime current, int days)
        {
            return AddBusinessDays(current, -days);
        }
    }
}
