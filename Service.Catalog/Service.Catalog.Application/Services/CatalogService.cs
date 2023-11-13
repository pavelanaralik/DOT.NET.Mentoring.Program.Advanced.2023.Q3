using Service.Catalog.Application.DTOs;
using Service.Catalog.Infrastructure.Repositories;
using AutoMapper;
using Service.Catalog.Application.Validators;
using Service.Catalog.Domain.Entities;
using FluentValidation;

namespace Service.Catalog.Application.Services;

public class CatalogService : ICatalogService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly CategoryValidator _categoryValidator = new CategoryValidator();

    public CatalogService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<(IEnumerable<CategoryDto>, long)> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        long itemCount = await _categoryRepository.GetCountAsync(cancellationToken);
        var res = await _categoryRepository.GetAllAsync(cancellationToken);
        var categories = _mapper.Map<IEnumerable<CategoryDto>>(res);
        return (categories, itemCount);
    }

    public async Task AddCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken = default)
    {
        var validationResult = await _categoryValidator.ValidateAsync(categoryDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.AddAsync(category, cancellationToken);
    }

    public async Task UpdateCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken = default)
    {
        var validationResult = await _categoryValidator.ValidateAsync(categoryDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.UpdateAsync(category, cancellationToken);
    }

    public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken = default)
    {
        await _categoryRepository.DeleteAsync(id, cancellationToken);
    }
}