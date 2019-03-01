using System;
using System.Collections.Generic;
using System.IO;

namespace CsvSort.DataAccess
{
    public interface ICsvRepository
    {
        IEnumerable<T> ReadRecordsFromStream<T>(Stream stream);
        void WriteRecordsToBytes<T>(IEnumerable<T> records, FileStream stream);
    }
}
