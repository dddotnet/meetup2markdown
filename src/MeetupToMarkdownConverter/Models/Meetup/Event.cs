﻿namespace MeetupToMarkdownConverter.Models.Meetup
{
    public class Event
    {
        public long created { get; set; }
        public int duration { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int rsvp_limit { get; set; }
        public string status { get; set; }
        public long time { get; set; }
        public long updated { get; set; }
        public int utc_offset { get; set; }
        public int waitlist_count { get; set; }
        public int yes_rsvp_count { get; set; }
        public Venue venue { get; set; }
        public Group group { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public string visibility { get; set; }
    }
}
