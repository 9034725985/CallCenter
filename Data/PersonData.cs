using CallCenter.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CallCenter.Data;

public class PersonDataAccess(CallCenterDbContext context) : IPersonDataAccess
{
    public async Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken)
    {
        IEnumerable<MyPerson> persons = await Task.Run(() => new List<MyPerson>());
        return persons;
    }

    public async Task<int> UpdateMyPerson(MyPerson person, CancellationToken cancellationToken)
    {
        int response = new();
        await Task.Run(() => _ = 1 + 1);
        return response;
    }
    public async Task<Stopwatch> PutMultiple(List<MyPerson> persons, CancellationToken cancellationToken)
    {
        string strMyPerson = JsonConvert.SerializeObject(persons);
        foreach (var person in persons)
        {
            var databasePerson = await context.Persons.FirstOrDefaultAsync(x => x.Id == person.Id, cancellationToken: cancellationToken);
            if (databasePerson != null) { databasePerson.ModifiedDate = person.ModifiedDate; }
        }
        Stopwatch stopwatch = Stopwatch.StartNew();
        await context.SaveChangesAsync(cancellationToken);
        stopwatch.Stop();
        return stopwatch;
    }
}
