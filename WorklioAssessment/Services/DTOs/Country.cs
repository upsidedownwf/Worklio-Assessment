using System.Collections.Generic;

namespace WorklioAssessment.Services.DTOs
{
    public class Country
    {
        public Name Name { get; set; }
        public List<string> Capital { get; set; }
        public string Cca3 { get; set; }
        public GermanName GermanName { get; set; }
        public SpanishName SpanishName { get; set; }
        public FrenchName FrenchName { get; set; }
        public JapaneseName JapaneseName { get; set; }
        public ItalianName ItalianName { get; set; }
        public List<string> Borders { get; set; }
    }
    public class CountryDetails
    {
        public Country Country { get; set; }
        public List<Country> BorderingCountries { get; set; }
    }
    public class Name
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }
    public class GermanName
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }
    public class SpanishName
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }
    public class FrenchName
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }
    public class JapaneseName
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }
    public class ItalianName
    {
        public string Common { get; set; }
        public string Official { get; set; }
    }
}
