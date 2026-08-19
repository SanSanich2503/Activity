using Core;
using Core.Entities.Goods;
using Data.ViewModels;
using Data.ViewModels.Goods;

namespace Services.Services;

public class GoodService : BaseService
{
    private readonly GoodRepository _goodRepository;
    
    public GoodService(DataContext context, GoodRepository goodRepository) : base(context)
    {
        _goodRepository = goodRepository;
    }
    
    public GoodForm BuildByForm(GoodForm form) => new GoodForm(form.Id, form.Title, form.Description);

    public GoodForm BuildFormById(int id)
    {
        var good = _goodRepository.GetById(id);
        if (good != null) return new GoodForm(good.Id, good.Title, good.Description);

        return new GoodForm();
    }

    public GoodForm BuildForm() => new GoodForm();

    public GoodViewModelList BuildViewModelList(int pageNumber, int pageSize, string title)
    {
        try
        {
            var goods = _goodRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(title))
                goods = goods
                    .Where(x => !string.IsNullOrWhiteSpace(x.Title) && x.Title.ToLower().Contains(title.ToLower()))
                    .ToList();

            var count = goods.Count;
            var items = goods.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .OrderBy(x => x.Title)
                .Select(x => new GoodViewModelItem
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description
                });

            return new GoodViewModelList
            {
                Items = items,
                PageViewModel = new PageViewModel(count, pageNumber, pageSize),
                FilterViewModel = new FilterViewModel(title),
                Count = count
            };
        }
        catch (Exception e)
        {
            return new GoodViewModelList();
        }
    }

    public void Create(GoodForm form)
    {
        try
        {
            var good = new Good
            {
                Title = form.Title,
                Description = form.Description,
                LastModified =  DateTime.Now
            };
            _goodRepository.Add(good);
        }
        catch (Exception e)
        {
        }
    }

    public void Update(GoodForm form)
    {
        try
        {
            var good = _goodRepository.GetById(form.Id);
            if (good != null)
            {
                good.Title = form.Title;
                good.Description = form.Description;
                good.LastModified = DateTime.Now;
                _goodRepository.Update(good);
            }
        }
        catch (Exception e)
        {
        }
    }
    
    public void Delete(int id)
    {
        try
        {
            var good = _goodRepository.GetById(id);
            if (good != null) _goodRepository.Remove(good);
        }
        catch (Exception e)
        {
        }
    }
}