﻿using AutoMapper;
using FluentValidation;
using Service.Catalog.Application.DTOs;
using Service.Catalog.Application.Validators;
using Service.Catalog.Domain.Entities;
using Service.Catalog.Infrastructure.Repositories;

namespace Service.Catalog.Application.Services;

public class ProductItemService : IProductItemService
{
    private readonly IProductItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly ProductItemValidator _itemValidator = new ProductItemValidator();

    public ProductItemService(IProductItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<ProductItemDto> GetItemByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _itemRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<ProductItemDto>(item);
    }

    public async Task<IEnumerable<ProductItemDto>> GetAllItemsAsync(CancellationToken cancellationToken = default)
    {
        var items = await _itemRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProductItemDto>>(items);
    }

    public async Task<(IEnumerable<ProductItemDto>, long)> GetItemsByCategoryIdAsync(int? categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        long itemCount = await _itemRepository.GetCountAsync(categoryId, cancellationToken);
        var res = await _itemRepository.GetItemsByCategoryIdAsync(categoryId, pageNumber, pageSize, cancellationToken);
        var items = _mapper.Map<IEnumerable<ProductItemDto>>(res);
        return (items, itemCount);
    }

    public async Task AddItemAsync(ProductItemDto itemDto, CancellationToken cancellationToken = default)
    {
        var validationResult = await _itemValidator.ValidateAsync(itemDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var item = _mapper.Map<ProductItem>(itemDto);
        await _itemRepository.AddAsync(item, cancellationToken);
    }

    public async Task UpdateItemAsync(ProductItemDto itemDto, CancellationToken cancellationToken = default)
    {
        var validationResult = await _itemValidator.ValidateAsync(itemDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var item = _mapper.Map<ProductItem>(itemDto);
        await _itemRepository.UpdateAsync(item, cancellationToken);
    }

    public async Task DeleteItemAsync(int id, CancellationToken cancellationToken = default)
    {
        await _itemRepository.DeleteAsync(id, cancellationToken);
    }
}