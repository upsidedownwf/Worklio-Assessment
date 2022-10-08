using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorklioAssessment.Services.DTOs;

namespace WorklioAssessment.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<CountryDetails> GetCountry(string id);
    }
}
