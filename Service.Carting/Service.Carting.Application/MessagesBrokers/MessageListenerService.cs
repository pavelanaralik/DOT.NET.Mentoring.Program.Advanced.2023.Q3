﻿using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Service.Carting.Application.DTOs;
using Service.Carting.Application.Services;

namespace Service.Carting.Application.MessagesBrokers;

public class MessageListenerService : BackgroundService
{
    private readonly ServiceBusClient _client;
    private readonly ICartAppService _cartService;

    private readonly string _queueName;
    private readonly ILogger<MessageListenerService> _logger;
    private ServiceBusProcessor _processor;

    public MessageListenerService(string connectionString, string queueName, ICartAppService cartService, ILogger<MessageListenerService> logger)
    {
        _client = new ServiceBusClient(connectionString);
        _queueName = queueName;
        _cartService = cartService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor = _client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());

        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;

        stoppingToken.Register(() => _processor.CloseAsync(stoppingToken));

        await _processor.StartProcessingAsync(stoppingToken);

        // Wait for the stop signal
        WaitHandle.WaitAny(new[] { stoppingToken.WaitHandle });
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        try
        {
            string body = args.Message.Body.ToString();
            var item = JsonSerializer.Deserialize<CartItemDto>(body);
            var correlationId = args.Message.CorrelationId;

            if (item != null)
            {
                _logger.LogWarning("Start updating cart item for Id: {Id}  CorrelationId: {CorrelationId}", item.Id, correlationId);
                await UpdateCartItemAsync(item);
                _logger.LogWarning("End updating cart item for Id: {Id}  CorrelationId: {CorrelationId}", item.Id, correlationId);
            }
            
            await args.CompleteMessageAsync(args.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
            _logger.LogError($"Error processing message: {ex.Message}");

            if (args.Message.DeliveryCount >= 5)
            {
                await args.DeadLetterMessageAsync(args.Message, "ProcessingFailed", "Failed to process message after multiple attempts.");
            }
            else
            {
                await args.AbandonMessageAsync(args.Message);
            }
        }
       
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine($"Message handling error: {args.Exception}");
        return Task.CompletedTask;
    }

    private Task UpdateCartItemAsync(CartItemDto item)
    {
        return _cartService.UpdateItemToCartsAsync(item);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _processor.StopProcessingAsync(cancellationToken);
        Console.WriteLine("Stopped listening for messages.");
        await base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        StopAsync(CancellationToken.None).GetAwaiter().GetResult(); 

        _processor?.CloseAsync().GetAwaiter().GetResult();
        _client?.DisposeAsync().GetAwaiter().GetResult();
        base.Dispose();
    }
}