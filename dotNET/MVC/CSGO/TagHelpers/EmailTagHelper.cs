using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace CSGO.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            var target = content.GetContent().Split("@");
            output.Content.SetContent(target[0]);
        }
    }
}
