using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetaService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetaService.Controllers
{
    [ApiController]
    [Route("betas")]
    public class CalculatorController : ControllerBase
    {
        private readonly ITickTimeRepository repository;
        public CalculatorController(ITickTimeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<double[]> GetBetas(string ticker,
                                 string tickerBaseline,
                                 DateTime startDate,
                                 DateTime endDate,
                                 int betaDurationDays)
        {
            var calc = new BetaCalculator(repository);
            var betas = calc.GetBetas(ticker, tickerBaseline, startDate, endDate, betaDurationDays);
            return betas;
        }
    }
}
