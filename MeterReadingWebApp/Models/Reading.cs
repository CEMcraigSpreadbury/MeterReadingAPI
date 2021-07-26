using System;

namespace MeterReadingWebApp.Models
{
    public class Reading
    {
        public int ReadingId { get; set; }
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
    }
}
