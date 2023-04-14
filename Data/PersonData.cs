using CallCenter.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CallCenter.Data;

public class PersonDataAccess : IPersonDataAccess
{
    private readonly CallCenterDbContext _context;

    public PersonDataAccess(CallCenterDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken)
    {
        IEnumerable<MyPerson> persons = await Task.Run(() => new List<MyPerson>());
        return persons;
    }

    public async Task<int> UpdateMyPerson(MyPerson person, CancellationToken cancellationToken)
    {
        int response = new();
        await Task.Run(() => _ = 1 + 1);
        return 0;
    }
    public async Task<Stopwatch> PutMultiple(List<MyPerson> persons, CancellationToken cancellationToken)
    {
        string strMyPerson = JsonConvert.SerializeObject(persons);
        foreach (var person in persons)
        {
            var databasePerson = await _context.Persons.FirstOrDefaultAsync(x => x.Id == person.Id, cancellationToken: cancellationToken);
            if (databasePerson != null) { databasePerson.ModifiedDate = person.ModifiedDate; }
        }
        Stopwatch stopwatch = Stopwatch.StartNew();
        await _context.SaveChangesAsync(cancellationToken);
        stopwatch.Stop();
        return stopwatch;
    }
}
