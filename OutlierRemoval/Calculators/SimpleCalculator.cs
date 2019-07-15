using Outliers.Data;
using System;
using System.Collections.Generic;

namespace Outliers
{

    public class SimpleCalculator : ICalculator
    {
        public double CalculateAverage(List<PriceWithDate> dataPoints)
        {
            var sum = 0.0;
            foreach (var dp in dataPoints)
            {
                sum += dp.Price;
            }
            return sum / dataPoints.Count;
        }

        public double CalculateStdDeviation(List<PriceWithDate> dataPoints, double mean)
        {
            var sumSqDifference = 0.0;
            foreach (var dp in dataPoints)
            {
                sumSqDifference += Math.Pow(dp.Price - mean, 2);
            }

            return Math.Sqrt(sumSqDifference / (dataPoints.Count - 1));
        }
    }
    
}
