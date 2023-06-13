using System.CommandLine;
using System.CommandLine.Binding;

namespace DockerTraining;

public class CommandOption
{
	public string? SqlConnectionString { get; set; }
    public string? EchoString { get; set; }
    public bool IsReadFromFile { get; set; }
}

internal class CommandOptionBinder : BinderBase<CommandOption>
{
    private readonly Option<string> _echoOption;
    private readonly Option<string> _sqlOption;
    private readonly Option<bool> _readFileOption;

    public CommandOptionBinder(Option<string> echoOption, Option<string> sqlOption, Option<bool> readFileOp)
	{
        _echoOption = echoOption;
        _sqlOption = sqlOption;
        _readFileOption = readFileOp;
    }

	protected override CommandOption GetBoundValue(BindingContext bindingContext)
    {
        if (bindingContext.ParseResult.CommandResult.Children.Count == 0)
        {
            
            // var hh = bindingContext.ParseResult.RootCommandResult.GetValueForOption(bindingContext.ParseResult.RootCommandResult.Command.Options.First(a => a.Name.Equals("help")));
            //
            // bindingContext.ParseResult.RootCommandResult.Command.Invoke("");
            // //bindingContext. .Console. .WriteLine("test");// .ParseResult.RootCommandResult.Command.Invoke("--help");
            // return new CommandOption();
        }

        return new CommandOption
        {
            SqlConnectionString = bindingContext.ParseResult.GetValueForOption(_sqlOption), 
            EchoString = bindingContext.ParseResult.GetValueForOption(_echoOption),
            IsReadFromFile = bindingContext.ParseResult.GetValueForOption(_readFileOption)
        };
    }
}
