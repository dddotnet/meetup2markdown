namespace MeetupToMarkdownConverter.Meetup
{
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Net.Http;
    using System.Threading.Tasks;

    internal class API
    {
        internal static async Task<Event[]> GetEventData(string apiKey)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("https://api.meetup.com/NET-User-Group-Dresden/events?&sign=true&photo-host=public&page=20&key=" + apiKey);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Event[]>(responseString, new JavaScriptDateTimeConverter());
        }
    }
}
