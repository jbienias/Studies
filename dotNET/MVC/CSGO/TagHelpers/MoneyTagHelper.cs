using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CSGO.TagHelpers
{
    public class MoneyTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "strong";
            output.PostContent.SetHtmlContent(" $");
        }
    }
}
