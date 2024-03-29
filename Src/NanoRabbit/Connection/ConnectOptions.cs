﻿namespace NanoRabbit.Connection;


/// <summary>
/// NanoRabbit producer connect options
/// </summary>
public class ProducerOptions
{
    /// <summary>
    /// Customize producer name
    /// </summary>
    public string ProducerName { get; set; } = null!;

    /// <summary>
    /// RabbitMQ HostName, default: localhost
    /// </summary>
    public string HostName { get; set; } = "localhost";

    /// <summary>
    /// RabbitMQ AmqpTcpEndpoint port, default: 5672
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// RabbitMQ UserName, default: guest
    /// </summary>
    public string UserName { get; set; } = "guest";

    /// <summary>
    /// RabbitMQ Password, default: guest
    /// </summary>
    public string Password { get; set; } = "guest";

    /// <summary>
    /// RabbitMQ VirtualHost, default: "/"
    /// </summary>
    public string VirtualHost { get; set; } = "/";
    
    /// <summary>
    /// Exchange name
    /// </summary>
    public string? ExchangeName { get; set; }
    
    /// <summary>
    /// Publish routing-key
    /// </summary>
    public string? RoutingKey { get; set; }
    
    /// <summary>
    /// Exchange type, default: direct
    /// </summary>
    public string Type { get; set; } = ExchangeType.Direct;
    
    /// <summary>
    /// Exchange durable, default: true
    /// </summary>
    public bool Durable { get; set; } = true;
    
    /// <summary>
    /// Exchange auto-delete, default: false
    /// </summary>
    public bool AutoDelete { get; set; } = false;

    /// <summary>
    /// Set to false to disable automatic connection recovery. Defaults to true.
    /// </summary>
    public bool AutomaticRecoveryEnabled { get; set; } = true;

    /// <summary>
    /// Set to true to enable automatic resend cached massages. Defaults to false.
    /// </summary>
    public bool AutomaticResend { get; set; } = false;

    /// <summary>
    /// Exchange additional arguments
    /// </summary>
    public IDictionary<string, object>? Arguments { get; set; }
}

/// <summary>
/// Includes the list of ProducerOptions
/// </summary>
public class RabbitProducerOptions
{
    /// <summary>
    /// List of ProducerOptions
    /// </summary>
    public List<ProducerOptions> Producers { get; set; } = null!;
}

/// <summary>
/// NanoRabbit consumer connect options
/// </summary>
public class ConsumerOptions
{
    /// <summary>
    /// Customize consumer name
    /// </summary>
    public string ConsumerName { get; set; } = null!;

    /// <summary>
    /// RabbitMQ HostName, default: localhost
    /// </summary>
    public string HostName { get; set; } = "localhost";

    /// <summary>
    /// RabbitMQ AmqpTcpEndpoint port, default: 5672
    /// </summary>
    public int Port { get; set; } = 5672;

    /// <summary>
    /// RabbitMQ UserName, default: guest
    /// </summary>
    public string UserName { get; set; } = "guest";

    /// <summary>
    /// RabbitMQ Password, default: guest
    /// </summary>
    public string Password { get; set; } = "guest";

    /// <summary>
    /// RabbitMQ VirtualHost, default: "/"
    /// </summary>
    public string VirtualHost { get; set; } = "/";
    
    /// <summary>
    /// Subscribe queue name
    /// </summary>
    public string QueueName { get; set; } = null!;
    
    /// <summary>
    /// Set to false to disable automatic connection recovery. Defaults to true.
    /// </summary>
    public bool AutomaticRecoveryEnabled { get; set; } = true;

    /// <summary>
    /// BasicQos prefetchSize, default: 0
    /// </summary>
    public uint PrefetchSize { get; set; } = 0;
    
    /// <summary>
    /// BasicQos prefetchCount, default: 0
    /// </summary>
    public ushort PrefetchCount { get; set; } = 0;
}

/// <summary>
/// Includes the list of ConsumerOptions
/// </summary>
public class RabbitConsumerOptions
{
    /// <summary>
    /// List of ConsumerOptions
    /// </summary>
    public List<ConsumerOptions> Consumers { get; set; } = null!;
}

/// <summary>
/// NanoRabbit configs in appsettings.json
/// </summary>
public class RabbitConfiguration
{
    /// <summary>
    /// RabbitProducer configs
    /// </summary>
    public List<ProducerOptions>? Producers { get; set; }
    /// <summary>
    /// RabbitConsumer configs
    /// </summary>
    public List<ConsumerOptions>? Consumers { get; set; }
}

/// <summary>
/// Convenience class providing compile-time names for standard exchange types.
/// </summary>
/// <remarks>
/// Use the static members of this class as values for the
/// "exchangeType" arguments for IModel methods such as
/// ExchangeDeclare. The broker may be extended with additional
/// exchange types that do not appear in this class.
/// </remarks>
public static class ExchangeType
{
    /// <summary>
    /// Exchange type used for AMQP direct exchanges.
    /// </summary>
    public const string Direct = "direct";

    /// <summary>
    /// Exchange type used for AMQP fanout exchanges.
    /// </summary>
    public const string Fanout = "fanout";

    /// <summary>
    /// Exchange type used for AMQP headers exchanges.
    /// </summary>
    public const string Headers = "headers";

    /// <summary>
    /// Exchange type used for AMQP topic exchanges.
    /// </summary>
    public const string Topic = "topic";

    private static readonly string[] s_all = { Fanout, Direct, Topic, Headers };

    /// <summary>
    /// Retrieve a collection containing all standard exchange types.
    /// </summary>
    public static ICollection<string> All()
    {
        return s_all;
    }
}
