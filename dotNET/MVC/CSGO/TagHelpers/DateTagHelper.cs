using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace CSGO.TagHelpers
{
    public class DateTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "font";
            var content = await output.GetChildContentAsync();
            var target = DateTime.Parse(content.GetContent());
            int years = DateTime.Today.Year - target.Year;
            if (years > 18)
                output.Attributes.SetAttribute("color", "green");
            else
                output.Attributes.SetAttribute("color", "red");
        }
    }
}
