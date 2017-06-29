namespace MeetupToMarkdownConverter.Commands
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    internal static class Cleanup
    {
        internal static async Task Execute(string outputPath)
        {
            await Task.Factory.StartNew(
                path => {
                    Console.WriteLine($"Deleting directory {path}...");
                    Directory.Delete((string)path, true);
                    Console.WriteLine("    Done.");
                },
                outputPath
            );
        }
    }
}
