using CallCenter.Data.Model;
using System.Linq.Dynamic.Core.Tokenizer;

namespace CallCenter.Server.MyPersons
{
    public interface IMyPersonRepository
    {
        Task CreateAsync(MyPerson person);
        Task<MyPerson?> GetPersonAsync(int id, CancellationToken token);
        Task<bool> GetPersonExistsAsync(int id, CancellationToken token);
        Task<List<MyPerson>> GetPersonsAsync(CancellationToken token);
        Task<MyInteger> PutPersonAsync(MyPerson person, CancellationToken token);
        Task<MyInteger> PutPersonsAsync(List<MyPerson> persons, CancellationToken token);
    }
}