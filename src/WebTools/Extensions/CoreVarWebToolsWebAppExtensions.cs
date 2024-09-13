using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder;

public static class CoreVarWebToolsWebAppExtensions
{

	public static WebApplication UseHostnameRedirection(this WebApplication app, string targetHostname)
	{
		app.Use(async (context, next) =>
		{
			if (context.Request.Host.Host.Equals(targetHostname, StringComparison.OrdinalIgnoreCase))
				await next();
			else
				context.Response.Redirect(string.Concat(context.Request.Scheme, "://", targetHostname, context.Request.Path, context.Request.QueryString.Value), true, true);
		});

		return app;
	}

	public static WebApplication UseHostnameRedirection(this WebApplication app, string targetHostname, params string[] matchingHostnames)
	{
		var redirectableHostnames = new HashSet<string>(matchingHostnames, StringComparer.OrdinalIgnoreCase);
		app.Use(async (context, next) =>
		{
			if (!redirectableHostnames.Contains(context.Request.Host.Host) || context.Request.Host.Host.Equals(targetHostname, StringComparison.OrdinalIgnoreCase))
				await next();
			else
				context.Response.Redirect(string.Concat(context.Request.Scheme, "://", targetHostname, context.Request.Path, context.Request.QueryString.Value), true, true);
		});

		return app;
	}

	public static WebApplication UseHttpsHostnameRedirection(this WebApplication app, string targetHostname)
	{
		app.Use(async (context, next) =>
		{
			if (context.Request.Host.Host.Equals(targetHostname, StringComparison.OrdinalIgnoreCase))
				await next();
			else
				context.Response.Redirect(string.Concat("https://", targetHostname, context.Request.Path, context.Request.QueryString.Value), true, true);
		});

		app.UseHttpsRedirection();

		return app;
	}

	public static WebApplication UseHttpsHostnameRedirection(this WebApplication app, string targetHostname, params string[] matchingHostnames)
	{
		var redirectableHostnames = new HashSet<string>(matchingHostnames, StringComparer.OrdinalIgnoreCase);
		app.Use(async (context, next) =>
		{
			if (!redirectableHostnames.Contains(context.Request.Host.Host) || context.Request.Host.Host.Equals(targetHostname, StringComparison.OrdinalIgnoreCase))
				await next();
			else
				context.Response.Redirect(string.Concat("https://", targetHostname, context.Request.Path, context.Request.QueryString.Value), true, true);
		});

		app.UseHttpsRedirection();

		return app;
	}

}