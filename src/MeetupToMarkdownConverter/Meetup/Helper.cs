namespace MeetupToMarkdownConverter.Meetup
{
    using Models;
    using System;
    using System.Text;
    internal class Helper
    {
        #pragma warning disable SA1008 // StyleCop does not knor Tuples
        internal static (string markdown, string filename) ConvertEventToMarkdown(Event meetup)
        #pragma warning restore SA1008 // StyleCop does not knor Tuples
        {
            StringBuilder markdown = new StringBuilder();

            var date = new DateTime((meetup.Time * 10000) + 621355968000000000).ToLocalTime();

            var filename = date.ToString("yyyy-MM-dd") + "-" + meetup.Name.ToLower().Trim().Replace(" ", "-") + ".markdown";

            var placelink = $"https://maps.google.com?q={meetup.Venue.Lat},{meetup.Venue.Lon}";

            var dateString = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");

            markdown.AppendLine("---");
            markdown.AppendLine("layout: postnew");
            markdown.AppendLine($"title: \"{meetup.Name}\"");
            markdown.AppendLine("categories: treffen");
            markdown.AppendLine($"date: {dateString}");
            markdown.AppendLine("speaker: \"siehe Meetup\"");
            markdown.AppendLine($"place:{meetup.Venue.Name}");
            markdown.AppendLine($"placelink:{placelink}");
            markdown.AppendLine("---");

            markdown.AppendLine(meetup.Description);

            return (markdown: markdown.ToString(), filename: filename);
        }
    }
}
