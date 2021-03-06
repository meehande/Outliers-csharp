﻿using Outliers.Data;
using System.Collections.Generic;

namespace Outliers
{
    public class WindowedCalculator: SimpleCalculator
    {
        public readonly int WindowSize = 20;
        public double CalculateAverage(List<PriceWithDate> dataPoints)
        {
            if (dataPoints.Count > WindowSize)
            {
                return base.CalculateAverage(dataPoints.GetRange(dataPoints.Count - WindowSize - 1, WindowSize));
            }
            return base.CalculateAverage(dataPoints);
        }

        public double CalculateStdDeviation(List<PriceWithDate> dataPoints, double mean)
        {
            if (dataPoints.Count > WindowSize)
            {
                return base.CalculateStdDeviation(dataPoints.GetRange(dataPoints.Count - WindowSize - 1, WindowSize), mean);
            }
            return base.CalculateStdDeviation(dataPoints, mean);
        }
    }

}
