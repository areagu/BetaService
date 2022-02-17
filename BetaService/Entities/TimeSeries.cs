using System;
using System.Collections.Generic;

namespace BetaService.Entities
{
    public class TimeSeries
    {
        private List<DateTime> Dates;
        private List<double> ClosingPrices;
        private List<double> DailyReturnsLn;

        public TimeSeries()
        {
            Dates = new List<DateTime>();
            ClosingPrices = new List<double>();
            DailyReturnsLn = new List<double>();
        }

        public void Add(DateTime date, double closingPrice, double dailyReturnLn)
        {
            Dates.Add(date);
            ClosingPrices.Add(closingPrice);
            DailyReturnsLn.Add(dailyReturnLn);
        }

        public List<double> ExtractDailyReturnsForDateRange(DateTime from, int increment)
        {
            //Dates.Sort();  
            var found = Dates.BinarySearch(from);

            if (found == -1 * Dates.Count)
            {
                throw new ArgumentException(string.Format("Date {0} not found within input", from));
            }
            else if (found < 0)
            {
                // use the next available date
                found = -1 * found;
            }

            var adjustedIncrement = DailyReturnsLn.Count - found;

            if (found + increment >= DailyReturnsLn.Count)
            {
                Console.WriteLine("increment of {0} is outside of array bounds, " +
                    "using max available increment of {1}", increment, adjustedIncrement);
                increment = adjustedIncrement;
            }

            return DailyReturnsLn.GetRange(found, increment);
        }
    }
}
