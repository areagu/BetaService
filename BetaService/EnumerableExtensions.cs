using System;
using System.Collections.Generic;
using System.Linq;

namespace BetaService
{
    public static class EnumerableExtensions
    {
        public static double Variance(this IEnumerable<double> values)
        {
            var mean = values.Average();
            var sum = values.Sum(v => (v - mean) * (v - mean));
            var denominator = values.Count() - 1;
            return denominator > 0.0 ? (sum / denominator) : -1;
        }

        public static double StdDev(this IEnumerable<double> values)
        {
            return Math.Sqrt(values.Variance());
        }

        public static double Covariance(this IEnumerable<double> source, IEnumerable<double> other)
        {
            int len = source.Count() - 1; // sample size

            if (len <= 0)
                throw new ArgumentException("Sample size must be larger than 1");

            double avgSource = source.Average();
            double avgOther = other.Average();
            double covariance = 0;

            for (int i = 0; i < len; i++)
                covariance += (source.ElementAt(i) - avgSource) * (other.ElementAt(i) - avgOther);

            return covariance / len;
        }
    }
}