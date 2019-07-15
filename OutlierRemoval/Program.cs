using System;
using System.Collections.Generic;
using Outliers.Configuration;
using Outliers.Data;


namespace Outliers
{
    public class Program
    {
        private static void PrintSummary(List<PriceWithDate> input, List<PriceWithDate> output)
        {
            Console.WriteLine($"");
        }
        static void Main(string[] args)
        {
            var configuration = OutlierRemovalConfigurationFactory.ResolveConfiguration();

            if(configuration.DataHandler.TryGetData(configuration.InputFile, out var dataWithOutliers, out var status))
            {
                var dataWithoutOutliers = configuration.OutlierRemover.RemoveOutliers(dataWithOutliers);
                
                configuration.DataHandler.WriteOutput(configuration.OutputFile, dataWithoutOutliers);
            }

            Console.ReadLine();
        }
    }
}
