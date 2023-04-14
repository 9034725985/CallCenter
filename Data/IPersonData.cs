using CallCenter.Data.Model;
using System.Diagnostics;

namespace CallCenter.Data;

public interface IPersonDataAccess
{
    Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken);
    Task<int> UpdateMyPerson(MyPerson person, CancellationToken cancellationToken);
    Task<Stopwatch> PutMultiple(List<MyPerson> persons, CancellationToken cancellationToken);
}
