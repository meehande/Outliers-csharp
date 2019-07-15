using System.Collections.Generic;
using Outliers.Data;

namespace Outliers.IO
{
    public interface IDataHandler
    {
        bool TryGetData(string filePath, out List<PriceWithDate> outlierData, out string status);
        void WriteOutput(string filePath, IEnumerable<PriceWithDate> outlierData);
    }
}
