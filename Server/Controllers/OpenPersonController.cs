using CallCenter.Data;
using CallCenter.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CallCenter.Server.Controllers;

[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class OpenPersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly CallCenterDbContext _context;

    public OpenPersonController(CallCenterDbContext context, ILogger<PersonController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<MyPerson>> Get(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Get), nameof(PersonController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<MyPerson> persons = await _context.Persons.ToListAsync(cancellationToken);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Get), nameof(PersonController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return persons;
    }

    [HttpPut]
    public async Task<MyInteger> Put(MyPerson person, CancellationToken cancellationToken)
    {
        await Task.Run(() => _logger.LogInformation("Begin {methodname} in {classname}", nameof(Put), nameof(PersonController)), cancellationToken);
        string strMyPerson = JsonConvert.SerializeObject(person);
        _logger.LogInformation("Input is {name} with stringified value {value}", nameof(person), strMyPerson);
        Stopwatch stopwatch = Stopwatch.StartNew();
        bool exists = _context.Persons.Any(x => x.Id == person.Id);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Put), nameof(PersonController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return new();
    }
}
