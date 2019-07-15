using Outliers.Data;
using System.Collections.Generic;

namespace Outliers.OutlierRemovers
{
    public interface IOutlierRemover
    {
        IEnumerable<PriceWithDate> RemoveOutliers(List<PriceWithDate> dataWithOutliers);
    }
}
