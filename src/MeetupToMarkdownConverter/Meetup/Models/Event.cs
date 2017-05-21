namespace MeetupToMarkdownConverter.Meetup.Models
{
    public class Event
    {
        public long Created { get; set; }
        public int Duration { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rsvp_limit { get; set; }
        public string Status { get; set; }
        public long Time { get; set; }
        public long Updated { get; set; }
        public int Utc_offset { get; set; }
        public int Waitlist_count { get; set; }
        public int Yes_rsvp_count { get; set; }
        public Venue Venue { get; set; }
        public Group Group { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Visibility { get; set; }
    }
}
