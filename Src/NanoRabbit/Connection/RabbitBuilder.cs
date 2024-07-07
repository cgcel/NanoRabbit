﻿using Microsoft.Extensions.Configuration;

namespace NanoRabbit.Connection;

/// <summary>
/// RabbitConfiguration Builder.
/// </summary>
public class RabbitConfigurationBuilder
{
    private readonly RabbitConfiguration _rabbitConfiguration;

    /// <summary>
    /// RabbitConfiguration constructor.
    /// </summary>
    public RabbitConfigurationBuilder()
    {
        _rabbitConfiguration = new RabbitConfiguration
        {
            Producers = new List<ProducerOptions>(),
            Consumers = new List<ConsumerOptions>()
        };
    }

    /// <summary>
    /// Set hostname of RabbitMQ connection.
    /// </summary>
    /// <param name="hostName"></param>
    public void SetHostName(string hostName)
    {
        _rabbitConfiguration.HostName = hostName;
    }

    /// <summary>
    /// Set port of RabbitMQ amqp connection.
    /// </summary>
    /// <param name="port"></param>
    public void SetPort(int port)
    {
        _rabbitConfiguration.Port = port;
    }

    /// <summary>
    /// Set virtual host of RabbitMQ connection.
    /// </summary>
    /// <param name="virtualHost"></param>
    public void SetVirtualHost(string virtualHost)
    {
        _rabbitConfiguration.VirtualHost = virtualHost;
    }

    /// <summary>
    /// Set username of RabbitMQ connection.
    /// </summary>
    /// <param name="userName"></param>
    public void SetUserName(string userName)
    {
        _rabbitConfiguration.UserName = userName;
    }

    /// <summary>
    /// Set password of RabbitMQ connection.
    /// </summary>
    /// <param name="password"></param>
    public void SetPassword(string password)
    {
        _rabbitConfiguration.Password = password;
    }

    /// <summary>
    /// Set to true will enable a asynchronous consumer dispatcher. Defaults to false.
    /// </summary>
    /// <param name="useAsyncConsumer"></param>
    public void UseAsyncConsumer(bool useAsyncConsumer)
    {
        _rabbitConfiguration.UseAsyncConsumer = useAsyncConsumer;
    }

    /// <summary>
    /// Set to false will disable NanoRabbit GlobalLogger. Defaults to true.
    /// </summary>
    /// <param name="enableLogging"></param>
    public void EnableLogging(bool enableLogging)
    {
        _rabbitConfiguration.EnableLogging = enableLogging;
    }

    /// <summary>
    /// Add a producer to RabbitMQ connection.
    /// </summary>
    /// <param name="configureProducer"></param>
    public void AddProducer(Action<ProducerOptions> configureProducer)
    {
        var options = new ProducerOptions();
        configureProducer(options);
        if (_rabbitConfiguration.Producers != null) _rabbitConfiguration.Producers.Add(options);
    }

    /// <summary>
    /// Add a consumer to RabbitMQ connection.
    /// </summary>
    /// <param name="configureConsumer"></param>
    public void AddConsumer(Action<ConsumerOptions> configureConsumer)
    {
        var options = new ConsumerOptions();
        configureConsumer(options);
        if (_rabbitConfiguration.Consumers != null) _rabbitConfiguration.Consumers.Add(options);
    }

    /// <summary>
    /// Build RabbitConfiguration.
    /// </summary>
    /// <returns></returns>
    public RabbitConfiguration Build()
    {
        return _rabbitConfiguration;
    }
}

/// <summary>
/// RabbitConfiguration extensions.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Read NanoRabbit configs in appsettings.json
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static RabbitConfiguration? ReadSettings(this IConfiguration configuration)
    {
        var rabbitConfig = configuration.GetSection("NanoRabbit").Get<RabbitConfiguration>();
        return rabbitConfig;
    }
}