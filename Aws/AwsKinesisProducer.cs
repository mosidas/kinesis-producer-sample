using System.Text;
using Amazon.Kinesis;
using Amazon.Kinesis.Model;

public class AwsKinesisProducer(Amazon.RegionEndpoint region, string streamName)
{
  private readonly AmazonKinesisClient _client = new(region);
  private string _streamName = streamName;
  public async void PutRecord(string message, string partitionKey)
  {
    var record = new PutRecordRequest()
    {
      StreamName = _streamName,
      Data = new MemoryStream(Encoding.UTF8.GetBytes(message)),
      PartitionKey = partitionKey
    };

    var response = await _client.PutRecordAsync(record);
    Console.WriteLine($"Record sent to shard id: {response.ShardId}");
  }
}
