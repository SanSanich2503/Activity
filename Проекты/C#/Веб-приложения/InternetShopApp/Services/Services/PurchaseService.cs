using Core;
using Core.Entities.Purchases;
using Core.Entities.PurchaseStatuses;
using Core.Entities.Users;
using Data.Enums.PurchaseStatuses;
using Data.ViewModels;
using Data.ViewModels.Carts;
using Data.ViewModels.Orders;
using Microsoft.AspNetCore.Http;

namespace Services.Services;

public class PurchaseService : BaseService
{
    private readonly User? _user;
    private readonly PurchaseRepository _purchaseRepository;
    private readonly PurchaseStatusRepository _purchaseStatusRepository;

    public PurchaseService(DataContext context, IHttpContextAccessor contextAccessor, PurchaseRepository purchaseRepository,
        PurchaseStatusRepository purchaseStatusRepository, UserRepository userRepository) : base(context)
    {
        _purchaseRepository = purchaseRepository;
        _purchaseStatusRepository = purchaseStatusRepository;
        
        var userGuid = contextAccessor.HttpContext?.User.Identity?.Name ?? "";
        _user = userRepository.GetCurrentUser(userGuid);
    }

    public CartViewModelList GetCart(int pageNumber, int pageSize)
    {
        if (_user != null)
        {
            try
            {
                var cart = _user.Role.Title?.ToLower() == "админ"
                    ? _purchaseRepository.GetCart()
                    : _purchaseRepository.GetCartByUserId(_user.Id);
                
                var count = cart.Count;
                var items = cart
                    .OrderByDescending(x => x.PurchaseDate)
                    .Select(x => new CartViewModelItem
                    {
                        Id = x.Id,
                        Title = x.Good.Title,
                        Description = x.Good.Description,
                        Price = x.Good.Price,
                        DeliveryDays = x.Good.DeliveryDays
                    });

                return new CartViewModelList
                {
                    Items = items,
                    PageViewModel = new PageViewModel(count, pageNumber, pageSize),
                    FilterViewModel = new FilterViewModel(),
                    Count = count
                };
            }
            catch (Exception e)
            {
                return new CartViewModelList();
            }
        }

        return new CartViewModelList();
    }

    public OrderViewModelList GetOrders(int pageNumber, int pageSize)
    {
        if (_user != null)
        {
            try
            {
                var orders = _user.Role.Title?.ToLower() == "админ"
                    ? _purchaseRepository.GetOrders()
                    : _purchaseRepository.GetOrdersByUserId(_user.Id);
                
                var count = orders.Count;
                var items = orders
                    .OrderByDescending(x => x.PurchaseDate)
                    .Select(x => new OrderViewModelItem
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        Price = x.Good.Price,
                        PurchaseDate = x.PurchaseDate.ToShortDateString(),
                        DeliveryDate = x.PurchaseDate.AddDays(x.Good.DeliveryDays).ToShortDateString(),
                        Status = x.PurchaseStatus.Title?.ToString(),
                        StatusEnum = x.PurchaseStatus.PurchaseStatusEnum
                    });

                return new OrderViewModelList
                {
                    Items = items,
                    PageViewModel = new PageViewModel(count, pageNumber, pageSize),
                    FilterViewModel = new FilterViewModel(),
                    Count = count
                };
            }
            catch (Exception e)
            {
                return new OrderViewModelList();
            }
        }
        
        return new OrderViewModelList();
    }

    public void Delete(int id)
    {
        try
        {
            var purchase = _purchaseRepository.GetById(id);
            if (purchase != null) _purchaseRepository.Remove(purchase);
        }
        catch (Exception e)
        {
        }
    }

    public void Cancel(int id)
    {
        try
        {
            var purchaseStatus = _purchaseStatusRepository.GetByEnumId(PurchaseStatusEnum.Cancelled);
            if (purchaseStatus != null) _purchaseRepository.ChangeStatus(id, purchaseStatus);
        }
        catch (Exception e)
        {
        }
    }

    public void Complete(int id)
    {
        try
        {
            var purchaseStatus = _purchaseStatusRepository.GetByEnumId(PurchaseStatusEnum.Completed);
            if (purchaseStatus != null) _purchaseRepository.ChangeStatus(id, purchaseStatus);
        }
        catch (Exception e)
        {
        }
    }

    public void Return(int id)
    {
        try
        {
            var purchaseStatus = _purchaseStatusRepository.GetByEnumId(PurchaseStatusEnum.Returned);
            if (purchaseStatus != null) _purchaseRepository.ChangeStatus(id, purchaseStatus);
        }
        catch (Exception e)
        {
        }
    }
}