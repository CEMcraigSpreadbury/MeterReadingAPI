using CsvHelper;
using MeterReadingAPI.Models;
using MeterReadingAPI.Repositories;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingAPI.Services
{
    public class ReadingService : IReadingService
    {
        private readonly IReadingRepository _readingRepository;

        public ReadingService(IReadingRepository readingRepository)
        {
            _readingRepository = readingRepository;
        }

        public async Task<ReadingReponse> UploadReadings(IFormFile meterReadings)
        {
            TextReader reader = new StreamReader(meterReadings.OpenReadStream());
            var csvReader = new CsvReader(reader, new System.Globalization.CultureInfo("en-GB"));
            List<Reading> records = csvReader.GetRecords<Reading>().ToList();

            return await _readingRepository.UploadReadings(records);
        }
    }
}
