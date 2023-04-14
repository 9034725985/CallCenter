﻿using CallCenter.Data;
using CallCenter.Data.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CallCenter.Server.Data;

public class PersonDataService : IPersonDataService
{
    private readonly IPersonDataAccess _data;
    private readonly ILogger<PersonDataService> _logger;
    public PersonDataService(IPersonDataAccess data, ILogger<PersonDataService> logger)
    {
        _data = data;
        _logger = logger;
    }
    public async Task<List<MyPerson>> GetPersons(CancellationToken cancellationToken)
    {
        IEnumerable<MyPerson> persons = await _data.GetPersons(cancellationToken);
        return persons.ToList();
    }

    public async Task<int> Put(MyPerson person, CancellationToken cancellationToken)
    {
        int myInteger = await _data.UpdateMyPerson(person, cancellationToken);
        return myInteger;
    }

    public async Task<Stopwatch> PutMultiple(List<MyPerson> persons, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Begin {methodname} in {classname}", nameof(PutMultiple), nameof(PersonDataService));
        string strMyPerson = JsonConvert.SerializeObject(persons);
        _logger.LogInformation("Input is {name} with stringified value {value}", nameof(persons), strMyPerson);
        //foreach (var person in persons)
        //{
        //    var databasePerson = await _context.Persons.FirstOrDefaultAsync(x => x.Id == person.Id, cancellationToken: cancellationToken);
        //    if (databasePerson != null) { databasePerson.ModifiedDate = person.ModifiedDate; }
        //}
        Stopwatch stopwatch = Stopwatch.StartNew();
        //await _context.SaveChangesAsync(cancellationToken);
        stopwatch.Stop();
        await Task.Run(() => _logger.LogInformation("End {methodname} in {classname}", nameof(PutMultiple), nameof(PersonDataService)), cancellationToken);
        _logger.LogInformation("PerfMatters: {methodname} in {classname} returned in {stopwatchmilliseconds} milliseconds",
            nameof(PutMultiple), nameof(PersonDataService), stopwatch.ElapsedMilliseconds);
        return stopwatch;
    }
}