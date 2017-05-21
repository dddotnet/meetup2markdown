namespace MeetupToMarkdownConverter.Commands
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    internal class Render
    {
        internal static async Task Execute(string apikey, string outputPath)
        {
            // load data
            var data = await Meetup.API.GetEventData(apikey);

            // write result
            data.Select(x => Meetup.Helper.ConvertEventToMarkdown(x)).ToList().ForEach(
                x =>
                {
                    Console.WriteLine($"Writing file {x.filename}");
                    WriteMarkdown(Path.Combine(outputPath, x.filename), x.markdown);
                }
            );
        }

        private static void WriteMarkdown(string filePath, string markdown)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

            using (StreamWriter outputFile = new StreamWriter(fileStream))
            {
                outputFile.Write(markdown);
            }
        }
    }
}
