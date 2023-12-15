﻿using System.Security.Authentication;
using Microsoft.Extensions.Logging;
using NanoRabbit.Connection;
using NanoRabbit.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NanoRabbit.Consumer;

/// <summary>
/// RabbitConsumer, can be inherited by custom Consumer
/// </summary>
public class RabbitConsumer
{
    private readonly IEnumerable<ConsumerOptions> _consumerOptionsList;

    public delegate void MessageHandler(string message);

    public RabbitConsumer(IEnumerable<ConsumerOptions> consumerOptionsList)
    {
        _consumerOptionsList = consumerOptionsList;
    }

    /// <summary>
    /// Receive messages from queue
    /// </summary>
    /// <param name="consumerName"></param>
    /// <param name="messageHandler"></param>
    public void Receive(string consumerName, Action<string> messageHandler)
    {
        var connectionOption = _consumerOptionsList.FirstOrDefault(x => x.ConsumerName == consumerName);

        if (connectionOption == null)
        {
            return;
        }

        var factory = new ConnectionFactory
        {
            HostName = connectionOption.HostName,
            Port = connectionOption.Port,
            UserName = connectionOption.UserName,
            Password = connectionOption.Password,
            VirtualHost = connectionOption.VirtualHost,
            AutomaticRecoveryEnabled = connectionOption.AutomaticRecoveryEnabled
            // SocketFactory = null,
            // AmqpUriSslProtocols = SslProtocols.None,
            // AuthMechanisms = null,
            // DispatchConsumersAsync = false,
            // ConsumerDispatchConcurrency = 0,
            // NetworkRecoveryInterval = default,
            // MemoryPool = null,
            // HandshakeContinuationTimeout = default,
            // ContinuationTimeout = default,
            // EndpointResolverFactory = null,
            // RequestedConnectionTimeout = default,
            // SocketReadTimeout = default,
            // SocketWriteTimeout = default,
            // Ssl = null,
            // TopologyRecoveryEnabled = false,
            // TopologyRecoveryFilter = null,
            // TopologyRecoveryExceptionHandler = null,
            // Endpoint = null,
            // ClientProperties = null,
            // CredentialsProvider = null,
            // CredentialsRefresher = null,
            // RequestedChannelMax = 0,
            // RequestedFrameMax = 0,
            // RequestedHeartbeat = default,
            // MaxMessageSize = 0,
            // Uri = null,
            // ClientProvidedName = null
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            try
            {
                // 处理接收到的消息
                messageHandler(message);
                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            // finally
            // {
            //     channel.BasicAck(ea.DeliveryTag, false);
            // }
        };

        channel.BasicConsume(
            queue: connectionOption.QueueName,
            autoAck: true,
            consumer: consumer);
        // channel.Dispose();
        // connection.Dispose();
    }
}