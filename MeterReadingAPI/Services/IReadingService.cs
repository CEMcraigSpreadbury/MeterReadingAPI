using MeterReadingAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MeterReadingAPI.Services
{
    public interface IReadingService
    {
        Task<ReadingReponse> UploadReadings(IFormFile meterReadings);
    }
}
