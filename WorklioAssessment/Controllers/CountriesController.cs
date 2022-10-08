using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorklioAssessment.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorklioAssessment.Controllers
{
    [ApiController, Route("api/country")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var countries = await countryService.GetCountries();
            return Ok(countries);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var countryDetails = await countryService.GetCountry(id);
            return Ok(countryDetails);
        }
    }
}
