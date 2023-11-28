using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Service.Catalog.Application.DTOs;
using System.Text.Json;

namespace Service.Catalog.Application.MessagesBrokers;

public class MessageService : IMessageService, IAsyncDisposable
{
    private readonly string _queueName;
    private readonly ServiceBusAdministrationClient _adminClient;
    private readonly ServiceBusClient _client;

    public MessageService(string connectionString, string queueName)
    {
        _client = new ServiceBusClient(connectionString);
        _adminClient = new ServiceBusAdministrationClient(connectionString);
        _queueName = queueName;
    }

    public async Task PublishAsync(ProductItemDto item)
    {
        await CreateQueueExistsAsync();
        
        await using ServiceBusSender sender = _client.CreateSender(_queueName);
        string messageBody = JsonSerializer.Serialize(item);
        ServiceBusMessage message = new ServiceBusMessage(messageBody);

        await sender.SendMessageAsync(message);
    }

    private async Task CreateQueueExistsAsync()
    {
        if (!await _adminClient.QueueExistsAsync(_queueName))
        {
            await _adminClient.CreateQueueAsync(_queueName);
            Console.WriteLine($"Topic {_queueName} created.");
        }
        else
        {
            Console.WriteLine($"Topic {_queueName} already exists.");
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _client.DisposeAsync();
    }
}