namespace MeetupToMarkdownConverter.Meetup
{
    using MeetupToMarkdownConverter.Meetup.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class API
    {
        internal static async Task<Event[]> GetEventData(Settings settings)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync($"{settings.Api}{settings.Group}/events?&sign=true&photo-host=public&page=20");

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Event[]>(responseString, new JavaScriptDateTimeConverter());
        }
    }
}
