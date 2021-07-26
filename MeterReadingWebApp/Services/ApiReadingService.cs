using MeterReadingWebApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeterReadingWebApp.Services
{
    public class ApiReadingService : IApiReadingService
    {
        private readonly string ApiUrl = "http://localhost:23928";
        public async Task<ReadingReponse> UploadReadings(IFormFile meterReadings)
        {
            ReadingReponse apiResponse = new ReadingReponse();

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(ApiUrl);

                    byte[] data;
                    using (var br = new BinaryReader(meterReadings.OpenReadStream())) data = br.ReadBytes((int)meterReadings.OpenReadStream().Length);

                    ByteArrayContent bytes = new ByteArrayContent(data);
                    MultipartFormDataContent multiContent = new MultipartFormDataContent();
                    multiContent.Add(bytes, "meterReadings", meterReadings.FileName);

                    var result = await client.PostAsync("api/Readings/meter-reading-uploads", multiContent);

                    if (result.IsSuccessStatusCode)
                    {
                        var response = await result.Content.ReadAsStringAsync();

                        apiResponse = JsonConvert.DeserializeObject<ReadingReponse>(response);
                    }

                    return apiResponse;
                }
                catch (Exception)
                {
                    return apiResponse;
                }
            }
        }
    }
}
