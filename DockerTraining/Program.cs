using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using DockerTraining;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console;

await BuildCommandLine()
      .UseHost(_ => Host.CreateDefaultBuilder(),
          host =>
          {
              host
                  .ConfigureLogging((ctx, builder) =>
                  {
                      builder.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Error);
                  })
                  .ConfigureServices(services =>
                  {
                      services.AddTransient<CommandService>();
                  });
          })
      .UseDefaults()
      .Build()
      .InvokeAsync(args);

static CommandLineBuilder BuildCommandLine()
{
    var echoStringOp = new Option<string>(new[] { "--echo", "-e" }, getDefaultValue: () => "No Command - My Test message", "Echo String to spit about");
    var sqlConnectionStringOp = new Option<string>(new[] { "--sql", "-s" }, "Connection to sql db");
    var readFileTextOp = new Option<bool>(new[] { "--readFile", "-r" }, "Read and Display Text File");

    var root = new RootCommand { Description = "Docker Training",  };
    root.AddOption(sqlConnectionStringOp);
    root.AddOption(echoStringOp);
    root.AddOption(readFileTextOp);

    root.SetHandler(Run, new CommandOptionBinder(echoStringOp, sqlConnectionStringOp, readFileTextOp), new ServieBinder());
    return new CommandLineBuilder(root);
}

static void Run(CommandOption options, CommandService commandService)
{
    AnsiConsole.Write(new FigletText("MedSol Training").LeftJustified().Color(new Color(89, 48, 1)));

    commandService.Execute(options);
}
