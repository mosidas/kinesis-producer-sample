using System.Text;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using KinesisApi.Models;

namespace KinesisApi.Services;

public interface IAwsKinesisProducer
{
  Task<MessageResponse?> PutRecordAsync(string message, string streamName);
}

public class AwsKinesisProducer : IAwsKinesisProducer
{
  private readonly AmazonKinesisClient _client = new(Amazon.RegionEndpoint.APNortheast1);
  public async Task<MessageResponse?> PutRecordAsync(string message, string streamName)
  {
    try
    {
      using var data = new MemoryStream(Encoding.UTF8.GetBytes(message));
      var record = new PutRecordRequest()
      {
        StreamName = streamName,
        Data = data,
        PartitionKey = Guid.NewGuid().ToString()
      };

      var response = await _client.PutRecordAsync(record);
      Console.WriteLine($"Record sent to shard id: {response.ShardId} message: '{message}' with sequence number: {response.SequenceNumber}");
      return new MessageResponse(response.ShardId, response.SequenceNumber);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error: {ex}");
      return null;
    }
  }
}
