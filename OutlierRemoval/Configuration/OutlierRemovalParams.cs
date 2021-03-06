﻿using Outliers.IO;
using Outliers.OutlierRemovers;

namespace Outliers.Configuration
{
    public class OutlierRemovalParams
    {
        public IDataHandler DataHandler { get; private set; }

        public IOutlierRemover OutlierRemover { get; private set; }


        public string InputFile { get; private set; }

        public string OutputFile { get; private set; }


        public OutlierRemovalParams(IDataHandler dataHandler, IOutlierRemover outlierRemover, string inputFile, 
            string outputFile)
        {
            DataHandler = dataHandler;
            OutlierRemover = outlierRemover;
            InputFile = inputFile;
            OutputFile = outputFile;
        }


    }
}
