using CoreVar.WebTools.Razor.Support;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace CoreVar.WebTools.Razor.TagHelpers;

[HtmlTargetElement("li-link", Attributes = "page")]
public class LiLinkTagHelper : TagHelper
{

	[ViewContext]
	public ViewContext ViewContext { get; set; } = default!;

	[HtmlAttributeName("page")]
	public string? Page { get; set; }

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		var items = ViewContext.HttpContext.Features.GetRequiredFeature<IItemsFeature>();
		if (!items.Items.TryGetValue(typeof(LiLinkTagHelperSettings), out var settingsObject) || settingsObject is not LiLinkTagHelperSettings settings)
			settings = new();

		output.TagName = "li";

		var isLinkActive = false;
		if (ViewContext.RouteData.Values.TryGetValue("page", out var pageObjectValue) && 
			pageObjectValue is string pageValue && 
			pageValue.Equals(Page, StringComparison.OrdinalIgnoreCase))
			isLinkActive = true;

		if (isLinkActive)
		{
			var classNames = new StringBuilder();
			if (context.AllAttributes.TryGetAttribute("class", out var classAttribute))
				classNames.Append(classAttribute.Value);
			if (classNames.Length > 0)
				classNames.Append(' ');
			classNames.Append(settings.ActiveClassName ?? "active");

			output.Attributes.SetAttribute("class", classNames);
		}
		
		output.PreContent.AppendHtml($@"<a href=""~{Page}"">");
		output.PostContent.AppendHtml($@"</a>");

		base.Process(context, output);
	}

}
