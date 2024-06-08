using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CallCenter.Data.Model;
using CallCenter.Server.MyAnalytics;

namespace CallCenter.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsRepository _repository;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(IAnalyticsRepository repository, ILogger<AnalyticsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Analytics>> Get(CancellationToken token)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Get), nameof(AnalyticsController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<Analytics> analyticsRecords = await _repository.GetAllAnalyticsAsync(token);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Get), nameof(AnalyticsController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(AnalyticsController), stopwatch.ElapsedMilliseconds);
        return analyticsRecords;
    }

    [HttpGet("{id}")]
    public async Task<Analytics?> GetAnalytics(int id, CancellationToken token)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(GetAnalytics), nameof(AnalyticsController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        Analytics? analyticsRecord = await _repository.GetAnalyticsAsync(id, token);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(GetAnalytics), nameof(AnalyticsController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(GetAnalytics), nameof(AnalyticsController), stopwatch.ElapsedMilliseconds);
        return analyticsRecord;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AnalyticsData analyticsData, CancellationToken token)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Create), nameof(AnalyticsController));
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Create a new Analytics object and populate it with data from analyticsData and server-side information
        var analytics = new Analytics
        {
            DataKey = analyticsData.DataKey,
            DataValue = analyticsData.DataValue,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
            Referer = Request.Headers["Referer"].FirstOrDefault(),
            CreatedAt = DateTime.UtcNow // Assuming you want to use UTC time
        };

        await _repository.CreateAsync(analytics, token);

        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Create), nameof(AnalyticsController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Create), nameof(AnalyticsController), stopwatch.ElapsedMilliseconds);

        return CreatedAtAction(nameof(GetAnalytics), new { id = analytics.Id }, analytics);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Analytics analytics, CancellationToken token)
    {
        if (id != analytics.Id)
        {
            return BadRequest();
        }

        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Update), nameof(AnalyticsController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        await _repository.UpdateAnalyticsAsync(analytics, token);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Update), nameof(AnalyticsController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Update), nameof(AnalyticsController), stopwatch.ElapsedMilliseconds);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken token)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Delete), nameof(AnalyticsController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        await _repository.DeleteAnalyticsAsync(id, token);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Delete), nameof(AnalyticsController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Delete), nameof(AnalyticsController), stopwatch.ElapsedMilliseconds);
        return NoContent();
    }
}
