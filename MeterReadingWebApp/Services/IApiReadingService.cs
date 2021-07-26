using MeterReadingWebApp.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MeterReadingWebApp.Services
{
    public interface IApiReadingService
    {
        Task<ReadingReponse> UploadReadings(IFormFile meterReadings);
    }
}
