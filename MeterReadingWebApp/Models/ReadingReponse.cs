using System.Collections.Generic;

namespace MeterReadingWebApp.Models
{
    public class ReadingReponse
    {
        public List<Reading> SuccessfulReadings { get; set; }
        public List<Reading> FailedReadings { get; set; }
    }
}
