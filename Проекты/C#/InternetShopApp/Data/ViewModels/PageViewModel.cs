namespace Data.ViewModels;

public class PageViewModel
{
    public int PageNumber { get; set; }
    public int TotalCount { get; set; }

    public PageViewModel() { }

    public PageViewModel(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalCount = (int)Math.Ceiling((double)count / pageSize);
    }

    public bool HasNextPage => PageNumber < TotalCount;

    public bool HasPreviousPage => PageNumber > 1;
}