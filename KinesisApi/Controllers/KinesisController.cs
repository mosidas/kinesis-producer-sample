using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using KinesisApi.Models;
using System.Text;
using KinesisApi.Services;

namespace KinesisApi.Controllers;

[ApiController]
[Route("[controller]")]
public class KinesisController : ControllerBase
{
  private readonly IAwsKinesisProducer _awsKinesisProducer;// Replace with your Kinesis stream name

  public KinesisController(IAwsKinesisProducer awsKinesisProducer)
  {
    _awsKinesisProducer = awsKinesisProducer;
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] MessageRequest request)
  {
    if (request == null || string.IsNullOrEmpty(request.Message))
    {
      return BadRequest("Invalid request");
    }

    var responce = await _awsKinesisProducer.PutRecordAsync(message: request.Message, streamName: request.StreamName);
    if (responce == null)
    {
      return BadRequest("Failed to send message");
    }

    return Ok($"Message sent to {responce.ShardId} with sequence number {responce.SequenceNumber}");
  }
}
