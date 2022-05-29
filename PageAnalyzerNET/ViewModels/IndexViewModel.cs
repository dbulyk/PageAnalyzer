using PageAnalyzerNET.Models;

namespace PageAnalyzerNET.ViewModels;

public class IndexViewModel
{
    public IEnumerable<Url> Urls { get;}
    public PageViewModel PageViewModel { get;}
    public IndexViewModel(IEnumerable<Url> urls, PageViewModel viewModel)
    {
        Urls = urls;
        PageViewModel = viewModel;
    }
}