using System.Security.Cryptography;
using Core.Entities.Categories;
using Core.Entities.Goods;
using Core.Entities.GoodToCategories;
using Core.Entities.PurchaseStatuses;
using Core.Entities.Roles;
using Core.Entities.Users;
using Data.Enums.PurchaseStatuses;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Core;

public class DbInitializer
{
    public static void Initialize(DataContext context)
    {
        var creationDate = DateTime.Now;
        
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(new List<Role>
            {
                new Role
                {
                    Title = "Админ",
                    Description = "Роль администратора",
                    LastModified =  creationDate
                },
                new Role
                {
                    Title = "Покупатель",
                    Description = "Роль покупателя",
                    LastModified =  creationDate
                }
            });
            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            var adminRole = context.Roles.FirstOrDefault(x => x.Title == "Админ");
            if (adminRole != null)
            {
                context.Users.Add(new User
                {
                    UserGuid = Guid.NewGuid().ToString(),
                    Title = "Главный админ",
                    Description = "Главный админ системы",
                    Email = "admin@admin.com",
                    Password = HashPassword("123"),
                    RoleId = adminRole.Id,
                    LastModified = creationDate
                });
                context.SaveChanges();
            }
        }

        if (!context.Categories.Any())
        {
            context.Categories.AddRange(new List<Category>
            {
                new Category
                {
                    Title = "Обувь",
                    Description = "Категория обуви",
                    LastModified = creationDate
                },
                new Category
                {
                    Title = "Одежда",
                    Description = "Категория одежды",
                    LastModified = creationDate
                }
            });
            context.SaveChanges();
        }

        if (!context.Goods.Any())
        {
            var shoe = context.Categories.FirstOrDefault(x => x.Title == "Обувь");
            if (shoe != null)
            {
                var good = new Good
                {
                    Title = "Кроссовки мужские (размер 42)",
                    Description = "Мужские кроссовки 42-го размера.",
                    Count = 20,
                    DeliveryDays = 3,
                    Price = 3000,
                    LastModified = creationDate
                };
                context.Goods.Add(good);
                context.SaveChanges();
                
                context.GoodToCategories.AddRange(new GoodToCategory
                {
                    GoodId = good.Id,
                    CategoryId = shoe.Id,
                    LastModified = creationDate
                });
                context.SaveChanges();
            }
            
            var cloth = context.Categories.FirstOrDefault(x => x.Title == "Одежда");
            if (cloth != null)
            {
                var good = new Good
                {
                    Title = "Футболка мужская (размер 50)",
                    Description = "Мужская футболка 50-го размера.",
                    Count = 30,
                    DeliveryDays = 2,
                    Price = 1500,
                    LastModified = creationDate
                };
                context.Goods.Add(good);
                context.SaveChanges();
                
                context.GoodToCategories.AddRange(new GoodToCategory
                {
                    GoodId = good.Id,
                    CategoryId = cloth.Id,
                    LastModified = creationDate
                });
                context.SaveChanges();
            }
        }

        if (!context.PurchaseStatuses.Any())
        {
            context.PurchaseStatuses.AddRange(new List<PurchaseStatus>
            {
                new PurchaseStatus
                {
                    Title = "Корзина",
                    Description = "Статус 'Корзина'",
                    PurchaseStatusEnum = PurchaseStatusEnum.Cart,
                    LastModified = creationDate
                },
                new PurchaseStatus
                {
                    Title = "Доставка",
                    Description = "Статус 'Доставка'",
                    PurchaseStatusEnum = PurchaseStatusEnum.Delivery,
                    LastModified = creationDate
                },
                new PurchaseStatus
                {
                    Title = "Завершен",
                    Description = "Статус 'Завершен'",
                    PurchaseStatusEnum = PurchaseStatusEnum.Completed,
                    LastModified = creationDate
                },
                new PurchaseStatus
                {
                    Title = "Отменен",
                    Description = "Статус 'Отменен'",
                    PurchaseStatusEnum = PurchaseStatusEnum.Cancelled,
                    LastModified = creationDate
                },
                new PurchaseStatus
                {
                    Title = "Возврат",
                    Description = "Статус 'Возврат'",
                    PurchaseStatusEnum = PurchaseStatusEnum.Returned,
                    LastModified = creationDate
                }
            });
            context.SaveChanges();
        }
    }
    
    private static string HashPassword(string? password)
        => Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: RandomNumberGenerator.GetBytes(128 / 8),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
}