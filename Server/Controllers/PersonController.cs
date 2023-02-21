using CallCenter.Data.Model;
using CallCenter.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CallCenter.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly PersonDataService _service;

    public PersonController(ILogger<PersonController> logger, PersonDataService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<MyPerson>> Get(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Get), nameof(PersonController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<MyPerson> persons = await _service.GetPersons(cancellationToken);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Get), nameof(PersonController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return persons;
    }

    [HttpPut]
    public async Task<MyInteger> Put(MyPerson person, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Put), nameof(PersonController));
        string strMyPerson = JsonConvert.SerializeObject(person);
        _logger.LogInformation("Input is {name} with stringified value {value}", nameof(person), strMyPerson);
        Stopwatch stopwatch = Stopwatch.StartNew();
        MyInteger myInteger = await _service.Put(person, cancellationToken);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Put), nameof(PersonController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return myInteger;
    }
}
