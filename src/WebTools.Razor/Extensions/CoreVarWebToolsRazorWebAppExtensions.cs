using CoreVar.WebTools.Razor.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder;

public static class CoreVarWebToolsRazorWebAppExtensions
{

	public static WebApplication CoreVarTagHelpers(this WebApplication webApplication, Action<RazorTagHelpersConfiguration> config)
	{
		webApplication.Use(async (context, next) =>
		{
			var tagHelpersConfig = ActivatorUtilities.CreateInstance<RazorTagHelpersConfiguration>(webApplication.Services, context.Features);
			config(tagHelpersConfig);

			await next();
		});
		return webApplication;
	}

}
