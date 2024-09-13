using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreVar.WebTools.Razor.Support;

public class RazorTagHelpersConfiguration(IFeatureCollection features)
{

	public RazorTagHelpersConfiguration LiLink(string? activeClassName = null)
	{
		var itemsFeature = features.GetRequiredFeature<IItemsFeature>();

		if (!itemsFeature.Items.TryGetValue(typeof(LiLinkTagHelperSettings), out var settingsObject) || settingsObject is not LiLinkTagHelperSettings settings)
			itemsFeature.Items.Add(typeof(LiLinkTagHelperSettings), settings = new());

		settings.ActiveClassName = activeClassName;

		return this;
	}

}
