using CsvHelper.Configuration.Attributes;
using System;

namespace MeterReadingAPI.Models
{
    public class Reading
    {
        [Ignore]
        public int ReadingId { get; set; }
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
    }
}
