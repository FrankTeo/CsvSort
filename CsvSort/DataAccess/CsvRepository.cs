using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Linq;

namespace CsvSort.DataAccess
{
    public class CsvRepository : ICsvRepository
    {
        public CsvRepository()
        {
        }
        /// <summary>
        /// Reads the records from stream.
        /// </summary>
        /// <returns>The records from stream.</returns>
        /// <param name="stream">A file Stream.</param>
        /// <typeparam name="T">Generic parameter.</typeparam>
        public IEnumerable<T> ReadRecordsFromStream<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var csvReader = new CsvReader(streamReader))
            {
                csvReader.Configuration.RegisterClassMap<CsvDataClassMapping>();
                csvReader.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLowerInvariant();
                return csvReader.GetRecords<T>().ToArray();
            }
        }

        /// <summary>
        /// Writes the records to File.
        /// </summary>
        /// <param name="records">List</param>
        /// <param name="stream">FileStream</param>
        /// <typeparam name="T">Generic parameter.</typeparam>
        public void WriteRecordsToBytes<T>(IEnumerable<T> records, FileStream stream)
        {
            using (var streamWriter = new StreamWriter(stream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
            }
        }
    }
}
