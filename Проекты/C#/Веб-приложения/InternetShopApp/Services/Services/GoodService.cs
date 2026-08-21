using Core;
using Core.Entities.Goods;
using Core.Entities.Purchases;
using Core.Entities.PurchaseStatuses;
using Core.Entities.Users;
using Data.Enums.PurchaseStatuses;
using Data.ViewModels;
using Data.ViewModels.Goods;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Services.Services;

public class GoodService : BaseService
{
    private readonly GoodRepository _goodRepository;
    private readonly PurchaseRepository _purchaseRepository;
    private readonly PurchaseStatusRepository _purchaseStatusRepository;
    private readonly User? _user;
    
    public GoodService(DataContext context, GoodRepository goodRepository, IHttpContextAccessor contextAccessor,
        PurchaseRepository purchaseRepository, PurchaseStatusRepository purchaseStatusRepository, UserRepository userRepository) : base(context)
    {
        _goodRepository = goodRepository;
        _purchaseRepository = purchaseRepository;
        _purchaseStatusRepository = purchaseStatusRepository;
        
        var userGuid = contextAccessor.HttpContext?.User.Identity?.Name ?? "";
        _user = userRepository.GetCurrentUser(userGuid);
    }
    
    public GoodForm BuildByForm(GoodForm form) => new GoodForm(form.Id, form.Title, form.Description);

    public GoodForm BuildFormById(int id)
    {
        var good = _goodRepository.GetById(id);
        if (good != null) return new GoodForm(good.Id, good.Title, good.Description);

        return new GoodForm();
    }

    public GoodForm BuildForm() => new GoodForm();

    public async Task<GoodViewModelList> BuildViewModelList(int pageNumber, int pageSize, string title)
    {
        try
        {
            var goods = _goodRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(title))
                goods = goods
                    .AsEnumerable()
                    .Where(x => !string.IsNullOrWhiteSpace(x.Title) && x.Title.ToLower().Contains(title.ToLower()))
                    .AsQueryable();

            var goodsList = await goods.ToListAsync();
            var count = goodsList.Count;
            var items = goodsList.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .OrderBy(x => x.Title)
                .Select(x => new GoodViewModelItem
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Price = x.Price,
                    Count = x.Count,
                    DeliveryDays = x.DeliveryDays
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
                Price = form.Price,
                Count = form.Count,
                DeliveryDays =  form.DeliveryDays,
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
                good.Price = form.Price;
                good.Count = form.Count;
                good.DeliveryDays = form.DeliveryDays;
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
            if (good != null)
            {
                _goodRepository.Remove(good);
                
                var purchases = _purchaseRepository.GetPurchasesByGoodId(id);
                if (purchases.Any()) _purchaseRepository.RemoveRange(purchases);
            }
        }
        catch (Exception e)
        {
        }
    }

    public void AddToCart(int id)
    {
        try
        {
            if (_user != null)
            {
                var good = _goodRepository.GetById(id);
                if (good is { Count: > 0 })
                {
                    good.Count--;
                    _context.SaveChanges();
                    
                    var purchaseStatus = _purchaseStatusRepository.GetByEnumId(PurchaseStatusEnum.Cart);
                    if (purchaseStatus != null)
                    {
                        var purchase = new Purchase
                        {
                            UserId = _user.Id,
                            GoodId = id,
                            PurchaseStatusId = purchaseStatus.Id
                        };

                        _purchaseRepository.Add(purchase);
                    }
                }
            }
        }
        catch (Exception e)
        {
        }
    }
}