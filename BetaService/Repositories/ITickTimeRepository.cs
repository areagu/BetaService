using System;
using System.Collections.Generic;
using BetaService.Entities;

namespace BetaService.Repositories
{
    public interface ITickTimeRepository
    {
        Dictionary<string, TimeSeries> GetTickTimeSeries();
        TimeSeries GetTimeSeries(string ticker);
    }
}