using System.Collections.Generic;
using System.Threading.Tasks;
using WorklioAssessment.Repositories.Models;

namespace WorklioAssessment.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<string> GetCountry(string id);
    }
}
