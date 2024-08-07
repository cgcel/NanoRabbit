﻿using Example.ProducerInConsumer;
using Microsoft.Extensions.Hosting;
using NanoRabbit.Connection;
using NanoRabbit.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

// Configure the RabbitMQ Connection
builder.Services.AddRabbitHelper(builder =>
{
    builder.SetHostName("localhost")
        .SetPort(5672)
        .SetVirtualHost("/")
        .SetUserName("admin")
        .SetPassword("admin")
        .AddProducerOption(producer =>
        {
            producer.ProducerName = "BarProducer";
            producer.ExchangeName = "amq.direct";
            producer.RoutingKey = "bar.key";
            producer.Type = ExchangeType.Direct;
        })
        .AddConsumerOption(consumer =>
        {
            consumer.ConsumerName = "FooConsumer";
            consumer.QueueName = "foo-queue";
        });
})
.AddRabbitConsumer<FooQueueHandler>("FooConsumer", consumers: 3);

using IHost host = builder.Build();

await host.RunAsync();