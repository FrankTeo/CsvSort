using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvSort.DataAccess;
using CsvSort.Model;

namespace CsvSort
{
    class Program
    {
        /// <summary>
        /// Entrance of business
        /// </summary>
        /// <param name="CsvFileName">string</param>
        /// <param name="OrderField">Order field.</param>
        public static void SortFile(string CsvFileName, string OrderField)
        {
            var csvRepository = new CsvRepository();

            var stream = File.OpenRead(CsvFileName);
            IEnumerable<CsvData> fileContent = csvRepository.ReadRecordsFromStream<CsvData>(stream);

            var FileStream = File.OpenWrite(Environment.CurrentDirectory + "/" + OrderField + ".csv");
            csvRepository.WriteRecordsToBytes<CsvData>(
                from fc in fileContent
                orderby(fc.GetType().GetProperty(OrderField).GetValue(fc))
                select new CsvData()
                {
                    id = fc.id,
                    first_name = fc.first_name,
                    last_name = fc.last_name,
                    email = fc.email,
                    gender = fc.gender,
                    ip_address = fc.ip_address,
                    balance = fc.balance
                }
            , FileStream);
        }

        static void Main(string[] args)
        {
            string CsvFileName = Environment.CurrentDirectory + "/test_data.csv";
            string[] OrderFields =
            {
                "id","first_name","last_name","email","gender","ip_address","balance"
            };


            foreach(var OrderField in OrderFields)
            {
                SortFile(CsvFileName, OrderField);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
