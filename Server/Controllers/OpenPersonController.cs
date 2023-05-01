using CallCenter.Data;
using CallCenter.Data.Model;
using CallCenter.Server.MyPersons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CallCenter.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class OpenPersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IMyPersonRepository _repository;

    public OpenPersonController(IMyPersonRepository repository, ILogger<PersonController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<MyPerson>> Get(CancellationToken token)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Get), nameof(PersonController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<MyPerson> persons = await _repository.GetPersonsAsync(token);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Get), nameof(PersonController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return persons;
    }

    // for example, https://localhost:7109/openperson/1
    [HttpGet("{id}")]
    public async Task<MyPerson?> GetPerson(int id, CancellationToken token)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(GetPerson), nameof(PersonController));
        Stopwatch stopwatch = Stopwatch.StartNew();
        MyPerson? person = await _repository.GetPersonAsync(id, token);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(GetPerson), nameof(PersonController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return person;
    }

    [HttpPut]
    public async Task<MyInteger> Put(MyPerson person, CancellationToken token)
    {
        await Task.Run(() => _logger.LogInformation("Begin {methodname} in {classname}", nameof(Put), nameof(PersonController)), token);
        string strMyPerson = JsonConvert.SerializeObject(person);
        _logger.LogInformation("Input is {name} with stringified value {value}", nameof(person), strMyPerson);
        bool exists = await _repository.GetPersonExistsAsync(person.Id, token);
        if (!exists)
        {
            return new()
            {
                Value = -1
            };
        }
        Stopwatch stopwatch = Stopwatch.StartNew();
        var result = _repository.PutPerson(person);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Put), nameof(PersonController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return result;
    }
}
