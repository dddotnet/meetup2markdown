namespace MeetupToMarkdownConverter
{
    using Microsoft.Extensions.CommandLineUtils;
    using System;
    using System.IO;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var app = new CommandLineApplication()
            {
                Name = "meetup2markdown",
                Description = "Meetup To Markdown Converter"
            };

            app.HelpOption("-? | -h | --help");
            app.VersionOption(
                "-v | --version",
                " Version\t: 1.0.0"
            );

            Console.WriteLine(app.Description);

            // show help when no command is present
            app.OnExecute(() =>
            {
                app.ShowVersion();
                app.ShowHelp();
                return -1;
            });

            app.Command("render", (command) =>
            {
                command.Description = "Render upcoming Meetups as Markdown.";
                command.HelpOption("-? | -h | --help");
                var argApiKey = command.Argument("apikey", "The API key for the Meetup API.", false);
                var optionOutput = command.Option("-o | --output", "Name of the output folder", CommandOptionType.SingleValue);

                command.OnExecute(() =>
                {
                    if (string.IsNullOrWhiteSpace(argApiKey.Value))
                    {
                        app.ShowHelp("render");
                        return -1;
                    }

                    var outputPath = Path.Combine(AppContext.BaseDirectory, "output" );
                    if (optionOutput.HasValue())
                    {
                        outputPath = optionOutput.Value();
                    }
                    if (!Directory.Exists(outputPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(outputPath);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"The output path is not valid: {outputPath}");
                            return -1;
                        }
                    }

                    Commands.Render.Execute(
                        argApiKey.Value,
                        outputPath
                    ).Wait();

                    return 0;
                });
            });

            var result = app.Execute(args);

            Environment.Exit(result);
        }
    }
}