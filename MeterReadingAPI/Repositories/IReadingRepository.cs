using MeterReadingAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeterReadingAPI.Repositories
{
    public interface IReadingRepository
    {
        Task<ReadingReponse> UploadReadings(IEnumerable<Reading> records);
    }
}
