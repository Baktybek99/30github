using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace _30git.Kafka;

public class KafkaService
{
    private const string BootstrapServers = "localhost:9092";

    public static async Task ProduceAsync(string topic, string message)
    {
        var config = new ProducerConfig { BootstrapServers = BootstrapServers };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });

        Console.WriteLine($"[Kafka Producer] Sent '{result.Value}' to '{result.TopicPartitionOffset}'");
    }

    public static void Consume(string topic, string groupId, CancellationToken cancellationToken = default)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = BootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe(topic);

        Console.WriteLine($"[Kafka Consumer] Subscribed to '{topic}'. Waiting for messages...");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(cancellationToken);
                Console.WriteLine($"[Kafka Consumer] Received: '{consumeResult.Message.Value}' " +
                                  $"from partition {consumeResult.Partition}, offset {consumeResult.Offset}");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("[Kafka Consumer] Shutting down.");
        }
        finally
        {
            consumer.Close();
        }
    }
}
