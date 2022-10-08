using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorklioAssessment.Helpers;
using WorklioAssessment.Repositories.Models;

namespace WorklioAssessment.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly HttpClient httpClient;
        private readonly IMyLogger myLogger;

        public CountryRepository(HttpClient httpClient, IMyLogger myLogger)
        {
            this.httpClient = httpClient;
            this.myLogger = myLogger;
        }
        public async Task<IEnumerable<Country>> GetCountries()
        {
            string queryString = "?fields=name,capital,translations,borders,cca3";
            var watch = new Stopwatch();
            watch.Start();
            var response = await httpClient.GetAsync($"{queryString}");
            watch.Stop();
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(data);
            await myLogger.LogMessage(LogLevel.Information, $"Fetched the list of countries from the Public API for duration of {watch.ElapsedMilliseconds}ms");
            return countries;

        }
        public async Task<string> GetCountry(string id)
        {
            var response = await httpClient.GetAsync($"/{id}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return data;
        }
    }
}
