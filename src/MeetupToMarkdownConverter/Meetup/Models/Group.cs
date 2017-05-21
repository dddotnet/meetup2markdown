namespace MeetupToMarkdownConverter.Meetup.Models
{
    public class Group
    {
        public long Created { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Join_mode { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Urlname { get; set; }
        public string Who { get; set; }
    }
}
