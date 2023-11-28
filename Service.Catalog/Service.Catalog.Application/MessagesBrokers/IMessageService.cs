using Service.Catalog.Application.DTOs;

namespace Service.Catalog.Application.MessagesBrokers;

public interface IMessageService
{
    Task PublishAsync(ProductItemDto item);
}