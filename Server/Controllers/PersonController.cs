using CallCenter.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Diagnostics;

namespace CallCenter.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;

    public PersonController(ILogger<PersonController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<MyPerson>> Get(CancellationToken cancellationToken)
    {
        await Task.Run(() => _logger.LogInformation("Begin {methodname} in {classname}", nameof(Get), nameof(PersonController)), cancellationToken);
        List<MyPerson> persons = new();
        return persons;
    }

    [HttpPut]
    public async Task<MyInteger> Put(MyPerson person, CancellationToken cancellationToken)
    {
        await Task.Run(() => _logger.LogInformation("Begin {methodname} in {classname}", nameof(Put), nameof(PersonController)), cancellationToken);
        return new();
    }

    [HttpPut("persons")]
    public async Task<Stopwatch> PutMultiple(List<MyPerson> persons, CancellationToken cancellationToken)
    {
        await Task.Run(() => _logger.LogInformation("Begin {methodname} in {classname}", nameof(PutMultiple), nameof(PersonController)), cancellationToken);
        Stopwatch stopwatch = Stopwatch.StartNew();
        stopwatch.Stop();
        return stopwatch;
    }
}
