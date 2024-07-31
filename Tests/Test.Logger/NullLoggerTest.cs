using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NanoRabbit;
using NanoRabbit.Connection;

namespace Test.Logger
{
    [TestClass]
    public class NullLoggerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;

            var rabbitHelper = new RabbitHelper(rabbitConfig: new RabbitConfiguration
            {
                HostName = "localhost",
                Port = 5672,
                VirtualHost = "/",
                UserName = "admin",
                Password = "admin",
                Producers = new List<ProducerOptions> { 
                    new ProducerOptions {
                        ProducerName = "FooProducer",
                        ExchangeName = "amq.topic",
                        RoutingKey = "foo.key"
                    }
                },
                Consumers = new List<ConsumerOptions> { 
                    new ConsumerOptions {
                        ConsumerName= "FooConsumer",
                        QueueName = "foo-queue"
                    }
                }
            }, logger);

            rabbitHelper.Publish<string>("FooProducer", "Hello from NanoRabbit");
        }
    }
}