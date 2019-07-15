using Outliers.Data;
using System;
using System.Collections.Generic;

namespace Outliers.OutlierRemovers
{
    public class ZScoreOutlierRemover : IOutlierRemover
    {
        private readonly double _zScoreOutlierThreshold = 3; 
        private readonly double _stdDeviationOutlierThreshold = 3; 
        private readonly double _minObservationsForMean = 10; 

        private ICalculator _calculator;
        public ZScoreOutlierRemover(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public IEnumerable<PriceWithDate> RemoveOutliers(List<PriceWithDate> dataWithOutliers)
        {
            double numObservationsSeen = 0;
            double average = 0;
            double stdDeviation = 0;
            var dataWithoutOutliers = new List<PriceWithDate>();

            var justDiscardedValues = new List<PriceWithDate>();
            var outliers = new List<PriceWithDate>();
            var prevDiscarded = false;
            var startIdx = 0;

            foreach (var dataPoint in dataWithOutliers)
            {
                numObservationsSeen++;
                var zScore = CalculateZScore(average, stdDeviation, dataPoint.Price);
                if (numObservationsSeen < _minObservationsForMean || !IsOutlier(zScore))
                {

                    dataWithoutOutliers.Add(dataPoint);
                                        average = CalculateAverage(dataWithoutOutliers);
                    stdDeviation = CalculateStdDeviation(dataWithoutOutliers, average);
                    prevDiscarded = false;
                }
                else
                {
                    if (!prevDiscarded)
                    {
                        outliers.AddRange(justDiscardedValues);
                        justDiscardedValues = new List<PriceWithDate>();
                    }
                    prevDiscarded = true;
                    justDiscardedValues.Add(dataPoint);
                    if (TrendHasJumped(justDiscardedValues))
                    {
                        startIdx = dataWithoutOutliers.Count;
                        var dataWithNewTrend = dataWithOutliers.GetRange(startIdx, dataWithOutliers.Count - startIdx);
                        var nextDataWithoutOutliers = RemoveOutliers(dataWithNewTrend);
                        dataWithoutOutliers.AddRange(nextDataWithoutOutliers);
                        break;
                    }
                }
            }

            PrintOutliers(outliers);
            return dataWithoutOutliers;
        }
        private double CalculateZScore(double mean, double deviation, double value)
        {
            return (value - mean) / deviation;
        }

        private double CalculateAverage(List<PriceWithDate> dataPoints)
        {
            return _calculator.CalculateAverage(dataPoints);
        }
        private double CalculateStdDeviation(List<PriceWithDate> dataPoints, double mean)
        {
            return _calculator.CalculateStdDeviation(dataPoints, mean);
        }

        private bool IsOutlier(double zScore)
        {
            return zScore >= _zScoreOutlierThreshold || zScore <= -_zScoreOutlierThreshold;
        }

        private bool TrendHasJumped(List<PriceWithDate> dataPoints)
        {
            if(dataPoints.Count > _minObservationsForMean)
            {
                var mean = CalculateAverage(dataPoints);
                var stdDeviation = CalculateStdDeviation(dataPoints, mean);
                return stdDeviation < _stdDeviationOutlierThreshold;
            }
            return false;
        }

        private void PrintOutliers(List<PriceWithDate> outliers)
        {
            Console.WriteLine($"{outliers.Count} outliers found: ");
            outliers.ForEach(_ => Console.WriteLine(_));
        }
    }
    
}
