﻿using Microsoft.Extensions.Logging;
using NanoRabbit.Consumer;

namespace Example.Autofac;

public class ConsumeService : RabbitSubscriber
{
    private readonly ILogger<RabbitSubscriber>? _logger;

    public ConsumeService(IRabbitConsumer consumer, ILogger<RabbitSubscriber>? logger, string consumerName) : base(consumer, consumerName, logger)
    {
        _logger = logger;
    }

    protected override bool HandleMessage(string message)
    {
        Console.WriteLine(message);
        return true;
    }
}