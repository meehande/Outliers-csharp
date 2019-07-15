using System.Linq;
using System.Collections.Generic;
using Outliers.Data;

namespace Outliers
{
    public class Utilities
    {
        public static bool IsOrderedByDate(List<PriceWithDate> outlierData)
        {
            var expectedList = outlierData.OrderBy(x => x.Date);

            return outlierData.SequenceEqual(expectedList);
        }

        public static double CalculateMean(IEnumerable<double> values)
        {
            double sum = 0;
            foreach(var value in values)
            {
                sum += value;
            }

            return sum / values.Count();

        }

    }
}
