namespace MeetupToMarkdownConverter.Meetup
{
    using MeetupToMarkdownConverter.Meetup.Models;
    using System;
    using System.Text;
    internal class Helper
    {
        public static DateTime FromUnixTime(long unixTime)
        {
            return Epoch.AddMilliseconds(unixTime);
        }

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        internal static (string markdown, string filename) ConvertEventToMarkdown(Event meetup)
        {
            StringBuilder markdown = new StringBuilder();

            var germanTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

            var date = FromUnixTime(meetup.Time).ToUniversalTime();
            var localDate = TimeZoneInfo.ConvertTimeFromUtc(date, germanTimeZone);

            var filename = date.ToString("yyyy-MM-dd") + "-" + MakeValidFileName(meetup.Name) + ".markdown";

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

        private static string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "-").ToLower().Trim().Replace(" ", "-");
        }
    }
}
