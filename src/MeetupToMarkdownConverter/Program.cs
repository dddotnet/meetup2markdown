namespace MeetupToMarkdownConverter
{
    using Microsoft.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    internal class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var meetupSettings = new Meetup.Models.Settings();
            Configuration.GetSection("settings").Bind(meetupSettings);

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
                var argGroup = command.Argument("group", "The Group ID for the Meetup API.", false);
                var optionOutput = command.Option("-o | --output", "Name of the output folder", CommandOptionType.SingleValue);

                command.OnExecute(() =>
                {
                    if (string.IsNullOrWhiteSpace(argApiKey.Value))
                    {
                        app.ShowHelp("render");
                        return -1;
                    }

                    if (string.IsNullOrWhiteSpace(argGroup.Value))
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

                    meetupSettings.Key = argApiKey.Value;
                    meetupSettings.Group = argGroup.Value;

                    Commands.Render.Execute(
                        meetupSettings,
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