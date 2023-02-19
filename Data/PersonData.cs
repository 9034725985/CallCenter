using System.Diagnostics;

namespace Data;

public class PersonData : IPersonData
{
    //private readonly string _connectionString = "Host=hansken.db.elephantsql.com;Database=xrbmpoui;User Id=xrbmpoui;Password=i38x7v1O3aNteoNxteJNB5thtPfKqqxn;";
    private readonly string _connectionString;
    private readonly ILogger<PersonData> _logger;

    public PersonData(string connectionString, ILogger<PersonData> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken)
    {
        using NpgsqlConnection connection = new(_connectionString);
        Stopwatch stopwatch = Stopwatch.StartNew();
        IEnumerable<MyPerson> persons = await connection.QueryAsync<MyPerson>(
            @"select
                person.*,
                createdby.alias as createdbyname, 
                modifiedby.alias as modifiedbyname
            from myperson person
            left join myperson createdby on createdby.id = person.createdby
            left join myperson modifiedby on modifiedby.id = person.modifiedby
            limit 200
            ;");
        stopwatch.Stop();
        foreach (MyPerson person in persons)
        {
            person.Stopwatch = stopwatch;
        }
        _logger.LogDebug("{methodName} returned {result}", nameof(GetPersons), JsonConvert.SerializeObject(persons));
        return persons;
    }
}
