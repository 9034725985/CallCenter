using CallCenter.Data;
using CallCenter.Data.Model;
using CallCenter.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CallCenter.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class PersonController : ControllerBase
{
    private readonly CallCenterDbContext _context;
    private readonly ILogger<PersonController> _logger;
    private readonly PersonDataService _service;

    public PersonController(CallCenterDbContext context, ILogger<PersonController> logger, PersonDataService service)
    {
        _context = context;
        _logger = logger;
        _service = service;
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
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(Put), nameof(PersonController));
        string strMyPerson = JsonConvert.SerializeObject(person);
        _logger.LogInformation("Input is {name} with stringified value {value}", nameof(person), strMyPerson);
        Stopwatch stopwatch = Stopwatch.StartNew();
        var databasePerson = await _context.Persons.FirstOrDefaultAsync(x => x.Id == person.Id);
        if (databasePerson != null) { databasePerson.ModifiedDate = person.ModifiedDate; }
        await _context.SaveChangesAsync(cancellationToken);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(Put), nameof(PersonController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return new();
    }

    [HttpPut("persons")]
    public async Task<MyInteger> PutMultiple(List<MyPerson> persons, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(PutMultiple), nameof(PersonController));
        string strMyPerson = JsonConvert.SerializeObject(persons);
        _logger.LogInformation("Input is {name} with stringified value {value}", nameof(persons), strMyPerson);
        Stopwatch stopwatch = Stopwatch.StartNew();
        foreach (var person in persons)
        {
            var databasePerson = await _context.Persons.FirstOrDefaultAsync(x => x.Id == person.Id, cancellationToken: cancellationToken);
            if (databasePerson != null) { databasePerson.ModifiedDate = person.ModifiedDate; }
        }
        await _context.SaveChangesAsync(cancellationToken);
        stopwatch.Stop();
        _logger.LogInformation("End {methodname} in {classname}", nameof(PutMultiple), nameof(PersonController));
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(Get), nameof(PersonController), stopwatch.ElapsedMilliseconds);
        return new();
    }
}
