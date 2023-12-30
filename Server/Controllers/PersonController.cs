using CallCenter.Data.Model;
using CallCenter.Server.MyPersons;
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
public class PersonController(IMyPersonRepository repository, ILogger<PersonController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<MyPerson>> Get(CancellationToken token)
    {
        logger.LogInformation("Begin {methodname} in {classname}", nameof(Get), nameof(PersonController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<MyPerson> persons = await repository.GetPersonsAsync(token);
        stopwatch.Stop();
        logger.LogInformation("End {methodname} in {classname}", nameof(Get), nameof(PersonController));
        logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return persons;
    }

    [HttpPut]
    public async Task<MyInteger> Put(MyPerson person, CancellationToken token)
    {
        await Task.Run(() => logger.LogInformation("Begin {methodname} in {classname}", nameof(Put), nameof(PersonController)), token);
        string strMyPerson = JsonConvert.SerializeObject(person);
        logger.LogInformation("Input is {name} with stringified value {value}", nameof(person), strMyPerson);
        bool exists = await repository.GetPersonExistsAsync(person.Id, token);
        if (!exists)
        {
            return new()
            {
                Value = -1
            };
        }
        Stopwatch stopwatch = Stopwatch.StartNew();
        var result = await repository.PutPersonAsync(person, token);
        stopwatch.Stop();
        logger.LogInformation("End {methodname} in {classname}", nameof(Put), nameof(PersonController));
        logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return result;
    }

    [HttpPut("persons")]
    public async Task<Stopwatch> PutMultiple(List<MyPerson> persons, CancellationToken token)
    {
        await Task.Run(() => logger.LogInformation("Begin {methodname} in {classname}", nameof(PutMultiple), nameof(PersonController)), token);
        Stopwatch stopwatch = Stopwatch.StartNew();
        _ = await repository.PutPersonsAsync(persons, token);
        stopwatch.Stop();
        return stopwatch;
    }
}
