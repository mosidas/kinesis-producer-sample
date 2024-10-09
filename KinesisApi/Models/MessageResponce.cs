namespace KinesisApi.Models;

public class MessageResponce(string shardId, string sequenceNumber)
{
  public string ShardId { get; set; } = shardId;
  public string SequenceNumber { get; set; } = sequenceNumber;
}
