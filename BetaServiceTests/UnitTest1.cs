using System;
using BetaService;
using BetaService.Repositories;
using Xunit;


namespace BetaServiceTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var repository = new InMemTickTimeRepository();

            var calc = new BetaCalculator(repository);
            var betas = calc.GetBetas("MSFT",
                                      "SPY",
                                      new DateTime(2021, 8, 30),
                                      new DateTime(2021, 9, 3),
                                      20);

            var exp = new double[5] { 0.3348, 0.3101, 0.2987, 0.2965, 0.2819 };

            Assert.True(betas.Length == 5);

            Assert.True(betas[0] == exp[0]);
            Assert.True(betas[1] == exp[1]);
            Assert.True(betas[2] == exp[2]);
            Assert.True(betas[3] == exp[3]);
            Assert.True(betas[4] == exp[4]);
        }
    }
}
