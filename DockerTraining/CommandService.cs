using System.CommandLine;
using DockerTraining.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Spectre.Console;

namespace DockerTraining;

internal class CommandService
{
	private readonly ILogger _logger;
	private readonly ILoggerFactory _loggerFactory;

	public CommandService(ILogger<CommandService> logger, ILoggerFactory loggerFactory)
	{
		_logger = logger;
		_loggerFactory = loggerFactory;
	}

	public void Execute(CommandOption options)
	{
        if (!string.IsNullOrWhiteSpace(options.EchoString))
        {
            AnsiConsole.MarkupLine($"[invert yellow]Echo:[/] [gold3]{options.EchoString}[/]");
        }

        ExecuteSqlLogic(options.SqlConnectionString);

        if (options.IsReadFromFile)
        {
            ReadFromFile();
        }

        _logger.LogInformation("Ending Application");
    }

    private void ExecuteSqlLogic(string? sqlConnection)
    {
        if (sqlConnection == null)
            return;
        if (!sqlConnection.Contains("mssql", StringComparison.CurrentCultureIgnoreCase))
            _logger.LogInformation("Connection String doesn't contain 'mssql'");

        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.UseSqlServer(sqlConnection, o =>
        {
            o.EnableRetryOnFailure();
            o.MigrationsAssembly(typeof(MyDbContext).Assembly.GetName().Name);
            o.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
        });
        optionsBuilder.UseLoggerFactory(_loggerFactory);

        using (var context = new MyDbContext(optionsBuilder.Options))
        {
            context.Database.Migrate();

            var displays = context.Displays.ToList();

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            foreach (var display in displays)
            {
                var json = JsonConvert.SerializeObject(display, settings);
                AnsiConsole.Write(new Panel(json)
                                  .Header($"Display JSON object: {display.DisplayId}")
                                  .BorderColor(Color.Yellow));
            }
        }
    }

    private static void ReadFromFile()
    {
        const string fileDir = "/usr/DockerTraining/";
        const string filename = "textfile.txt";
        var fileData = "File is Empty";
        if (!Directory.Exists(fileDir))
            Directory.CreateDirectory(fileDir);

        var filepath = Path.Combine(fileDir, filename);
        if (File.Exists(filepath))
            fileData = File.ReadAllText(filepath);
        else
            File.WriteAllText(filepath, fileData);

        AnsiConsole.MarkupLine($"[invert yellow]readFile:[/] [gold3]{fileData}[/]");
    }
}
