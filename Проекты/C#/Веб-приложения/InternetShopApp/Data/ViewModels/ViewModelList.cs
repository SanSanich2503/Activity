using Scrutor.AspNetCore;

namespace Data.ViewModels;

public abstract class ViewModelList<T> where T : ViewModelItem
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public PageViewModel PageViewModel { get; set; } = new PageViewModel();
    public FilterViewModel FilterViewModel { get; set; } = new FilterViewModel();
    public int Count { get; set; }
}