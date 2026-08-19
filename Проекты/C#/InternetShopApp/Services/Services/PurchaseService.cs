using Core;
using Core.Entities.Purchases;
using Core.Entities.Users;
using Microsoft.AspNetCore.Http;

namespace Services.Services;

public class PurchaseService : BaseService
{
    private readonly int _userId;
    private readonly PurchaseRepository _purchaseRepository;
    private readonly UserRepository _userRepository;

    public PurchaseService(DataContext context, IHttpContextAccessor contextAccessor,
        PurchaseRepository purchaseRepository, UserRepository userRepository) : base(context)
    {
        _purchaseRepository = purchaseRepository;
        _userRepository = userRepository;
        
        var userGuid = contextAccessor.HttpContext?.User.Identity?.Name ?? "";
        _userId = _userRepository.GetCurrentUser(userGuid)?.Id ?? 0;
    }

    public void GetCartForUser()
    {
        
    }

    public void GetOrdersForUser()
    {
        
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
        
    }

    public void Complete(int id)
    {
        
    }

    public void Return(int id)
    {
        
    }
}