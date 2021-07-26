using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeterReadingAPI.Models
{
    [NotMapped]
    public class ReadingReponse
    {
        public List<Reading> SuccessfulReadings { get; set; }
        public List<Reading> FailedReadings { get; set; }
    }
}
