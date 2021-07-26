using MeterReadingAPI.Data;
using MeterReadingAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingAPI.Repositories
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly Context _context;
        private readonly static int MaxLength = 5;

        public ReadingRepository(Context context)
        {
            _context = context;
        }

        public async Task<ReadingReponse> UploadReadings(IEnumerable<Reading> readings)
        {
            List<Reading> successfulReadings = new List<Reading>();
            List<Reading> failedReadings = new List<Reading>();

            foreach (var reading in readings)
            {
                if (IsValid(reading))
                {
                    successfulReadings.Add(reading);
                    _context.Readings.Add(reading);
                }
                else
                {
                    failedReadings.Add(reading);
                }
            }

            await _context.SaveChangesAsync();

            return new ReadingReponse { SuccessfulReadings = successfulReadings, FailedReadings = failedReadings };
        }

        private bool IsValid(Reading reading)
        {
            var accountExists = _context.Accounts.Any(e => e.AccountId == reading.AccountId);
            if (accountExists)
            {
                if (reading.MeterReadValue.Length == MaxLength)
                {
                    return !_context.Readings.Any(e => e.AccountId == reading.AccountId && e.MeterReadValue == reading.MeterReadValue && e.MeterReadingDateTime == reading.MeterReadingDateTime);
                }

                return false;
            }

            return false;
        }
    }
}
