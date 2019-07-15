using System;

namespace Outliers.Data
{
    public class PriceWithDate
    {
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public PriceWithDate(DateTime date, double price)
        {
            Price = price;
            Date = date;
        }

        public override string ToString()
        {
            return $"{Date} - {Price}";
        }
    }
}
