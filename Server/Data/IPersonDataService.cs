using CallCenter.Data.Model;
using System.Diagnostics;

namespace CallCenter.Server.Data
{
    public interface IPersonDataService
    {
        Task<List<MyPerson>> GetPersons(CancellationToken cancellationToken);
        Task<int> Put(MyPerson person, CancellationToken cancellationToken);
        Task<Stopwatch> PutMultiple(List<MyPerson> persons, CancellationToken cancellationToken);
    }
}