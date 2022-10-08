using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorklioAssessment.Repositories;
using WorklioAssessment.Services.DTOs;

namespace WorklioAssessment.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMemoryCache memoryCache;
        private readonly string cacheKey = "countries";
        public CountryService(ICountryRepository countryRepository, IMemoryCache memoryCache)
        {
            this.countryRepository = countryRepository;
            this.memoryCache = memoryCache;
        }
        public async Task<IEnumerable<Country>> GetCountries()
        {
            IEnumerable<Country> countries = await GetOrAddToCache();
            return countries;
        }
        public async Task<CountryDetails> GetCountry(string id)
        {
            IEnumerable<Country> countries = await GetOrAddToCache();
            var country = countries.FirstOrDefault(c => c.Cca3 == id);
            var borderingCountries = new List<Country>();
            foreach (var border in country.Borders)
            {
                borderingCountries.Add(countries.FirstOrDefault(c => c.Cca3 == border));
            }
            var countryDetails = new CountryDetails
            {
                Country = country,
                BorderingCountries = borderingCountries
            };
            return countryDetails;
        }
        public async Task<IEnumerable<Country>> GetOrAddToCache()
        {
            if (!memoryCache.TryGetValue(cacheKey, out IEnumerable<Country> countriesList))
            {
                var countriesFromApi = await countryRepository.GetCountries();
                countriesList = countriesFromApi.Select(country => new Country
                {
                    Name = new Name
                    {
                        Common = country.name.common,
                        Official = country.name.official
                    },
                    Capital = country.capital,
                    Cca3 = country.cca3,
                    GermanName = new GermanName
                    {
                        Common = country.translations.deu.common,
                        Official = country.translations.deu.official
                    },
                    SpanishName = new SpanishName
                    {
                        Common = country.translations.spa.common,
                        Official = country.translations.spa.official
                    },
                    JapaneseName = new JapaneseName
                    {
                        Common = country.translations?.jpn?.common,
                        Official = country.translations?.jpn?.official
                    },
                    FrenchName = new FrenchName
                    {
                        Common = country.translations.fra.common,
                        Official = country.translations.fra.official
                    },
                    ItalianName = new ItalianName
                    {
                        Common = country.translations.ita.common,
                        Official = country.translations.ita.official
                    },
                    Borders = country.borders
                });
                var memoryCacheOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                };
                memoryCache.Set(cacheKey, countriesList);
            }
            return countriesList;
        }
    }
}
