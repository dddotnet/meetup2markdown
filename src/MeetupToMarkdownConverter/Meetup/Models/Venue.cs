namespace MeetupToMarkdownConverter.Meetup.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool Repinned { get; set; }
        public string Address_1 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Localized_country_name { get; set; }
    }
}
