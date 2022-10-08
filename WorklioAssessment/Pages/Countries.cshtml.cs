using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WorklioAssessment.Helpers;
using WorklioAssessment.Services.DTOs;

namespace WorklioAssessment.Pages
{
    public class CountriesModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMyLogger myLogger;
        public IEnumerable<Country> Countries;
        private readonly string apiClient="internalApi";
        private readonly string apiPath = "api/country";
        public CountriesModel(IHttpClientFactory httpClientFactory, IMyLogger myLogger)
        {
            this.httpClientFactory = httpClientFactory;
            this.myLogger = myLogger;
        }
        public async Task OnGet()
        {
            
            var httpClient = httpClientFactory.CreateClient(apiClient);
            var response = await httpClient.GetAsync(apiPath);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            Countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(data);
        }
        public async Task<IActionResult> OnGetCountry(string id)
        {
            var httpClient = httpClientFactory.CreateClient(apiClient);
            var response = await httpClient.GetAsync($"{apiPath}/{id}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var countryDetails= JsonConvert.DeserializeObject<CountryDetails>(data);
            await myLogger.LogMessage(LogLevel.Information, $"Displaying details for the country: {countryDetails.Country.Name.Common} with cca3: {id}");
            return new OkObjectResult(countryDetails);
        }
    }
}
