namespace KinesisApi.Models;

public class MessageRequest(string message, string streamName)
{
  public string StreamName { get; set; } = streamName;
  public string Message { get; set; } = message;
}

