using MeterReadingWebApp.Models;
using MeterReadingWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace MeterReadingWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IApiReadingService _meterReadingApiService;

        public IndexModel(IApiReadingService meterReadingApiService)
        {
            _meterReadingApiService = meterReadingApiService;
        }

        [BindProperty]
        public IFormFile MeterReadings { get; set; }

        [BindProperty]
        public ReadingReponse ReadingReponse { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            ReadingReponse response = await _meterReadingApiService.UploadReadings(MeterReadings);
            ReadingReponse = response;

            return Page();
        }
    }
}
