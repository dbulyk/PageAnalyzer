using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PageAnalyzerNET.ViewModels;

namespace PageAnalyzerNET.TagHelpers;

public class PageLinkTagHelper : TagHelper
{
    IUrlHelperFactory urlHelperFactory;

    public PageLinkTagHelper(IUrlHelperFactory helperFactory)
    {
        urlHelperFactory = helperFactory;
    }

    [ViewContext] [HtmlAttributeNotBound] 
    private ViewContext ViewContext { get; set; } = null!;
    private PageViewModel? PageModel { get; set; }
    private string PageAction { get; set; } = "";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (PageModel == null) throw new Exception("PageModel is not set");
        var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
        output.TagName = "nav";

        // набор ссылок будет представлять список ul
        var tag = new TagBuilder("ul");
        tag.AddCssClass("pagination justify-content-center mt-5");

        // формируем три ссылки - на текущую, предыдущую и следующую
        TagBuilder currentItem = CreateTag(urlHelper, PageModel.PageNumber);
        
        if (PageModel.HasPreviousPage)
        {
            var prevItem = CreateTag(urlHelper, PageModel.PageNumber - 1);
            tag.InnerHtml.AppendHtml(prevItem);
        }

        tag.InnerHtml.AppendHtml(currentItem);
        
        if (PageModel.HasNextPage)
        {
            var nextItem = CreateTag(urlHelper, PageModel.PageNumber + 1);
            tag.InnerHtml.AppendHtml(nextItem);
        }

        output.Content.AppendHtml(tag);
    }

    TagBuilder CreateTag(IUrlHelper urlHelper, int pageNumber = 1)
    {
        var item = new TagBuilder("li");
        var link = new TagBuilder("a");
        
        if (pageNumber == PageModel?.PageNumber)
        {
            item.AddCssClass("active");
        }
        else
        {
            link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber });
        }

        item.AddCssClass("page-item");
        link.AddCssClass("page-link");
        link.InnerHtml.Append(pageNumber.ToString());
        item.InnerHtml.AppendHtml(link);
        return item;
    }
}