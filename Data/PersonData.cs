using CallCenter.Data.Model;

namespace CallCenter.Data;

public class PersonDataAccess : IPersonDataAccess
{
    private readonly string _connectionString;
    private readonly ILogger<PersonDataAccess> _logger;

    public PersonDataAccess(string connectionString, ILogger<PersonDataAccess> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken)
    {
        using NpgsqlConnection connection = new(_connectionString);
        IEnumerable<MyPerson> persons = await Task.Run(() => new List<MyPerson>());
        _logger.LogDebug("{methodName} returned {result}", nameof(GetPersons), JsonConvert.SerializeObject(persons));
        return persons;
    }

    public async Task<MyInteger> UpdateMyPerson(MyPerson person, CancellationToken cancellationToken)
    {
        using NpgsqlConnection connection = new(_connectionString);
        int response = new();
        await Task.Run(() => _logger.LogInformation("placeholder"));
        _logger.LogDebug("{methodName} returned {response} for input of {input}", nameof(UpdateMyPerson), response, JsonConvert.SerializeObject(person));
        MyInteger myInteger = new()
        {
            Value = response,
            Id = person.Id,
            ExternalId = person.ExternalId,
            CreatedBy = person.CreatedBy,
            CreatedDate = person.CreatedDate,
            ModifiedBy = person.ModifiedBy,
            ModifiedDate = person.ModifiedDate
        };
        return myInteger;
    }
}
