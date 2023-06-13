using System.CommandLine.Binding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DockerTraining;

internal class ServieBinder : BinderBase<CommandService>
{
	protected override CommandService GetBoundValue(BindingContext bindingContext)
	{
		var host = bindingContext.GetRequiredService<IHost>();
		return host.Services.GetRequiredService<CommandService>();
	}
}
