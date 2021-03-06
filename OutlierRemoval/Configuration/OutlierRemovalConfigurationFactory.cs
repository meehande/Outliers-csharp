﻿using System;
using System.IO;
using System.Configuration;
using Outliers.IO;
using Outliers.OutlierRemovers;

namespace Outliers.Configuration
{
    public class OutlierRemovalConfigurationFactory
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static OutlierRemovalParams ResolveConfiguration()
        {
            var dataHandler = ResolveDataHandler();
            var outlierRemover = ResolveOutlierRemover();
            var inputFile = ResolveInputFile();
            var outputFile = ResolveOutputFile();
            
            return new OutlierRemovalParams(dataHandler, outlierRemover, inputFile, outputFile);

        }

        private static IDataHandler ResolveDataHandler()
        {
            var dataHandlerConfig = ConfigurationManager.AppSettings["DataHandler"];
            switch (dataHandlerConfig.ToUpper())
            {
                case "CSV":
                    _logger.Info("Using csv data handler");
                    return new CSVHandler();
                    break;
                default:
                    throw new ArgumentException($"Type {dataHandlerConfig} not recognised as valid data handler!");
            }
        }

        private static IOutlierRemover ResolveOutlierRemover()
        {
            var removerConfig = ConfigurationManager.AppSettings["OutlierRemover"];
            var averageCalculator = ResolveCalculator();

            switch (removerConfig.ToUpper())
            {
                case "ZSCORE":
                    _logger.Info("Using Z Score outlier remover");
                    return new ZScoreOutlierRemover(averageCalculator);
                    break;
                default:
                    throw new ArgumentException($"Type {removerConfig} not recognised as valid outlier remover!");
            }

        }

        private static ICalculator ResolveCalculator()
        {
            var calculatorConfig = ConfigurationManager.AppSettings["Calculator"];

            switch (calculatorConfig.ToUpper())
            {
                case "SIMPLE":
                    _logger.Info("Using simple calculator");
                    return new SimpleCalculator();
                    break;
                case "WINDOWED":
                    _logger.Info("Using windowed calculator");
                    return new WindowedCalculator();
                    break;
                default:
                    throw new ArgumentException($"Type {calculatorConfig} not recognised as valid calculator!");
            }
        }
        
        private static string ResolveInputFile()
        {
            var filePath = ConfigurationManager.AppSettings["InputFile"];
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"Input file does not exist: {filePath}");
            }
            return filePath;
        }
        private static string ResolveOutputFile()
        {
            var filePath = ConfigurationManager.AppSettings["OutputFile"];
            
            return filePath;
        }
    }
}
