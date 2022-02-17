using System;
using BetaService.Repositories;

namespace BetaService
{
    public class BetaCalculator
    {
        private readonly ITickTimeRepository repository;
        public BetaCalculator(ITickTimeRepository repository)
        {
            this.repository = repository;
        }

        public double[] GetBetas(string ticker,
                                 string tickerBaseline,
                                 DateTime startDate,
                                 DateTime endDate,
                                 int betaDurationDays)
        {
            var tickTimeSeries = repository.GetTickTimeSeries();

            var businessDays = startDate.GetBusinessDays(endDate) + 1;
            var result = new double[businessDays];

            for (var i = 0; i < businessDays; i++)
            {
                var lookbackDate = startDate.SubtractBusinessDays(betaDurationDays);

                var dailyReturnsLn = tickTimeSeries[ticker].ExtractDailyReturnsForDateRange(lookbackDate, betaDurationDays);

                var variance = dailyReturnsLn.Variance();

                var baselineDailyReturnsLn = tickTimeSeries[tickerBaseline].ExtractDailyReturnsForDateRange(lookbackDate, betaDurationDays);

                var covariance = dailyReturnsLn.Covariance(baselineDailyReturnsLn);

                if (variance != 0)
                {
                    var beta = covariance / variance;
                    if (!double.IsNaN(beta))
                        result[i] = Math.Round(beta, 4);
                }

                startDate = startDate.AddBusinessDays(1);
            }

            return result;
        }
    }
}
