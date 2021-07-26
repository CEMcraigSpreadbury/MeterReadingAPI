using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MeterReadingAPI.Models;
using CsvHelper;
using System.IO;
using MeterReadingAPI.Services;
using MeterReadingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MeterReadingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IReadingService _readingService;

        public ReadingsController(Context context, IReadingService readingService)
        {
            _context = context;
            _readingService = readingService;
        }

        // GET: api/Readings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reading>>> GetReadings()
        {
            return await _context.Readings.ToListAsync();
        }

        // GET: api/Readings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reading>> GetReading(int id)
        {
            var reading = await _context.Readings.FindAsync(id);

            if (reading == null)
            {
                return NotFound();
            }

            return reading;
        }

        // PUT: api/Readings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReading(int id, Reading reading)
        {
            if (id != reading.ReadingId)
            {
                return BadRequest();
            }

            _context.Entry(reading).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReadingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Readings
        [HttpPost]
        public async Task<ActionResult<Reading>> PostReading(Reading reading)
        {
            _context.Readings.Add(reading);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReading", new { id = reading.ReadingId }, reading);
        }

        // POST: api/Readings/meter-reading-uploads
        [HttpPost]
        [Route("meter-reading-uploads")]
        public async Task<ActionResult> Post(IFormFile meterReadings)
        {
            try
            {
                var response = await _readingService.UploadReadings(meterReadings);

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        // DELETE: api/Readings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reading>> DeleteReading(int id)
        {
            var reading = await _context.Readings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }

            _context.Readings.Remove(reading);
            await _context.SaveChangesAsync();

            return reading;
        }

        private bool ReadingExists(int id)
        {
            return _context.Readings.Any(e => e.ReadingId == id);
        }
    }
}
