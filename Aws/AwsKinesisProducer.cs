using System.Text;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

namespace KinesisProducer.Aws;

public class AwsKinesisProducer(Amazon.RegionEndpoint region, string streamName)
{
  private readonly AmazonKinesisClient _client = new(region);
  private readonly string _streamName = streamName;
  public async void PutRecord(string message, string partitionKey)
  {
    try
    {
      using var data = new MemoryStream(Encoding.UTF8.GetBytes(message));
      var record = new PutRecordRequest()
      {
        StreamName = _streamName,
        Data = data,
        PartitionKey = partitionKey
      };

      var response = await _client.PutRecordAsync(record);
      Console.WriteLine($"Record sent to shard id: {response.ShardId}");

    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error: {ex}");
    }
  }
}
