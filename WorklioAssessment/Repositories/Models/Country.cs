using System.Collections.Generic;

namespace WorklioAssessment.Repositories.Models
{
    public class Country
    {
        public Name name { get; set; }
        public List<string> capital { get; set; }
        public string cca3 { get; set; }
        public Translations translations { get; set; }
        public List<string> borders { get; set; }
    }
}
