namespace MeetupToMarkdownConverter
{
    using MeetupToMarkdownConverter.Models.Meetup;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Parsing Meetup Api {DateTime.Now}");

            Task.Run(async () =>
            {
                //load data
                var data = await Program.getEventData("add-api-key");

                //convert meetup format to markdown file
                var mdEvents = data.Select(x => convertEventToMarkdown(x));

                //write result
                mdEvents.ToList().ForEach(x => { Console.WriteLine(x.filename); Program.writeMarkdown(x.markdown); });

                
            }).Wait();
        }

        private static async Task<Event[]> getEventData(string apiKey)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://api.meetup.com/NET-User-Group-Dresden/events?&sign=true&photo-host=public&page=20&key=" + apiKey);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Event[]>(responseString,new JavaScriptDateTimeConverter());
        }

        private static void writeMarkdown(string markdown)
        {
            Console.WriteLine(markdown);
        }

        private static (string markdown, string filename) convertEventToMarkdown(Event meetup)
        {
            StringBuilder markdown = new StringBuilder();

            var date = new DateTime((meetup.time * 10000) + 621355968000000000).ToLocalTime();

            var filename = date.ToString("yyyy-MM-dd") + "-" +meetup.name.ToLower().Trim().Replace(" ", "-") +".markdown";

            var placelink = $"https://maps.google.com?q={meetup.venue.lat},{meetup.venue.lon}";

            var dateString = date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");

            markdown.AppendLine("---");
            markdown.AppendLine("layout: postnew");
            markdown.AppendLine($"title: \"{meetup.name}\"");
            markdown.AppendLine("categories: treffen");
            markdown.AppendLine($"date: {dateString}");
            markdown.AppendLine("speaker: \"siehe Meetup\"");
            markdown.AppendLine($"place:{meetup.venue.name}");
            markdown.AppendLine($"placelink:{placelink}");
            markdown.AppendLine("---");
            
            markdown.AppendLine(meetup.description);

            return (markdown: markdown.ToString(), filename: filename);
        }
    }
}