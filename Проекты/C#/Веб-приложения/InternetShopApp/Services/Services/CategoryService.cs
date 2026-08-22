using Core;
using Core.Entities.Categories;
using Data.ViewModels;
using Data.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;

namespace Services.Services;

public class CategoryService : BaseService
{
    private readonly CategoryRepository _categoryRepository;
    
    public CategoryService(DataContext context, CategoryRepository categoryRepository) : base(context)
    {
        _categoryRepository = categoryRepository;
    }
    
    public CategoryForm BuildByForm(CategoryForm form) => new CategoryForm(form.Id, form.Title, form.Description);

    public CategoryForm BuildFormById(int id)
    {
        var category = _categoryRepository.GetById(id).Result;
        if (category != null) return new CategoryForm(category.Id, category.Title, category.Description);

        return new CategoryForm();
    }

    public CategoryForm BuildForm() => new CategoryForm();

    public async Task<CategoryViewModelList> BuildViewModelList(int pageNumber, int pageSize, string title)
    {
        var categories = _categoryRepository.GetAll();
        if (!string.IsNullOrWhiteSpace(title))
            categories = categories
                .AsEnumerable()
                .Where(x => !string.IsNullOrWhiteSpace(x.Title) && x.Title.ToLower().Contains(title.ToLower()))
                .AsQueryable();

        var categoriesList = await categories.ToListAsync();
        var count = categoriesList.Count;
        var items = categoriesList.Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .OrderBy(x => x.Title)
            .Select(x => new CategoryViewModelItem
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            });

        return new CategoryViewModelList
        {
            Items = items,
            PageViewModel = new PageViewModel(count, pageNumber, pageSize),
            FilterViewModel = new FilterViewModel(title),
            Count = count
        };
    }

    public async Task<(bool, string)> Create(CategoryForm form)
    {
        try
        {
            var category = new Category
            {
                Title = form.Title,
                Description = form.Description,
                LastModified = DateTime.Now
            };
            
            await _categoryRepository.Add(category);
            
            return (true, "OK");
        }
        catch (Exception e)
        {
            return (false, "Произошла внутренняя ошибка сервера");
        }
    }

    public async Task<(bool, string)> Update(CategoryForm form)
    {
        try
        {
            var category = _categoryRepository.GetById(form.Id).Result;
            if (category != null)
            {
                category.Title = form.Title;
                category.Description = form.Description;
                category.LastModified = DateTime.Now;
                
                await _categoryRepository.Update(category);
                
                return (true, "OK");
            }
        }
        catch (Exception e)
        {
            return (false, "Произошла внутренняя ошибка сервера");
        }

        return (false, "Элемент не найден");
    }
    
    public async Task<(bool, string)> Delete(int id)
    {
        try
        {
            var category = _categoryRepository.GetById(id).Result;
            if (category != null)
            {
                await _categoryRepository.Remove(category);
                
                return (true, "OK");
            }
        }
        catch (Exception e)
        {
            return (false, "Произошла внутренняя ошибка сервера");
        }

        return (false, "Элемент не найден");
    }
}