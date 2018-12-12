namespace MeetupToMarkdownConverter.Meetup
{
    using Models;
    using System;
    using System.Text;
    internal class Helper
    {
        public static DateTime FromUnixTime(long unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


#pragma warning disable SA1008 // StyleCop does not knor Tuples
        internal static (string markdown, string filename) ConvertEventToMarkdown(Event meetup)
#pragma warning restore SA1008 // StyleCop does not knor Tuples
        {
            StringBuilder markdown = new StringBuilder();

            var germanTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            
            var date = FromUnixTime(meetup.time).ToLocalTime();
            var localDate = TimeZoneInfo.ConvertTimeFromUtc(date, germanTimeZone);

            var filename = date.ToString("yyyy-MM-dd") + "-" + meetup.Name.ToLower().Trim().Replace(" ", "-") + ".markdown";

            var placeQuery = System.Net.WebUtility.UrlEncode($"{meetup.Venue.Address_1}, {meetup.Venue.City}, {meetup.Venue.Country}");
            var placelink = $"https://maps.google.com/maps?f=q&hl=en&q={placeQuery}";

            var dateString = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");

            markdown.AppendLine("---");
            markdown.AppendLine("layout: postnew");
            markdown.AppendLine($"title: \"{meetup.Name}\"");
            markdown.AppendLine("categories: treffen");
            markdown.AppendLine($"date: {dateString}");
            markdown.AppendLine("speaker: \"siehe Meetup\"");
            markdown.AppendLine($"place: {meetup.Venue.Name}");
            markdown.AppendLine($"placelink: {placelink}");
            markdown.AppendLine("---");

            markdown.AppendLine(string.Empty);
            markdown.AppendLine($"## {meetup.Name}");

            markdown.AppendLine(meetup.Description);

            markdown.AppendLine(string.Empty);
            markdown.AppendLine("## Ort und Zeit");
            markdown.AppendLine($"Wir treffen uns am {localDate.ToString("dd. MMMM yyyy")} um {localDate.ToString("HH:mm")} Uhr bei [{meetup.Venue.Name}]({placelink}).  ");
            markdown.AppendLine($"Die Gästeliste wird auf [Meetup]({meetup.Link}) verwaltet.");

            return (markdown: markdown.ToString(), filename: filename);
        }
    }
}
