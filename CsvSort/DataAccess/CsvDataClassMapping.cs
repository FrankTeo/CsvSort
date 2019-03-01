using System;
using CsvHelper.Configuration;
using CsvSort.Model;

namespace CsvSort.DataAccess
{
    public class CsvDataClassMapping : ClassMap<CsvData>
    {
        public CsvDataClassMapping()
        {
            AutoMap();
            Map(x => x.balance)
                .ConvertUsing(r =>
                {
                    var value = r.GetField("balance").Replace("\"", string.Empty);

                    if (Decimal.TryParse(value, out decimal result))
                    {
                        return result;
                    }

                    return 0m;
                });
        }
    }
}
