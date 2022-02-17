using System;
using System.Collections.Generic;
using System.IO;
using BetaService.Entities;

namespace BetaService.Repositories
{
    public class InMemTickTimeRepository : ITickTimeRepository
    {
        public Dictionary<string, TimeSeries> GetTickTimeSeries()
        {
            var lines = File.ReadAllLines("TestMarketData.csv");

            var timeSeries = new TimeSeries();

            var TickTimeSeries = new Dictionary<string, TimeSeries>();

            var priorTicker = "";
            var priorClosingPrice = 0.0;

            for (var i = 1; i < lines.Length; i++) // skip the header 
            {
                var line = lines[i].Split(",");
                var ticker = line[1];

                if (i > 1 && ticker != priorTicker)
                {
                    TickTimeSeries.Add(priorTicker, timeSeries);
                    timeSeries = new TimeSeries();
                    priorClosingPrice = 0.0;
                }

                var date = DateTime.Parse(line[0]);
                var closingPrice = double.Parse(line[2]);

                // check that priorClosingPrice is not Zero
                var dailyReturnLn = Math.Log(1 + (closingPrice - priorClosingPrice) / priorClosingPrice);

                timeSeries.Add(date, closingPrice, dailyReturnLn);

                priorClosingPrice = closingPrice;
                priorTicker = ticker;

                if (i == lines.Length - 1) // handle last batch
                    TickTimeSeries.Add(priorTicker, timeSeries);
            }

            return TickTimeSeries;
        }

        public TimeSeries GetTimeSeries(string ticker)
        {
            var data = GetTickTimeSeries();

            if (data.ContainsKey(ticker))
                return data[ticker];
            else
                return null;
        }
    }
}