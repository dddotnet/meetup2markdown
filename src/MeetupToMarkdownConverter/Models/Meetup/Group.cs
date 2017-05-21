namespace MeetupToMarkdownConverter.Models.Meetup
{
    public class Group
    {
        public long created { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string join_mode { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string urlname { get; set; }
        public string who { get; set; }
    }
}
