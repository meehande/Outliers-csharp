using Outliers.Data;
using System.Collections.Generic;

namespace Outliers
{
    public interface ICalculator
    {
        double CalculateAverage(List<PriceWithDate> dataPoints);
        double CalculateStdDeviation(List<PriceWithDate> dataPoints, double mean);

    }

}
