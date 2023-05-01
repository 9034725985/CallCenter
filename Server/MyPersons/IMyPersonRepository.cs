using CallCenter.Data.Model;

namespace CallCenter.Server.MyPersons
{
    public interface IMyPersonRepository
    {
        Task CreateAsync(MyPerson person);
        Task<MyPerson?> GetPersonAsync(int id, CancellationToken token);
        Task<bool> GetPersonExistsAsync(int id, CancellationToken token);
        Task<List<MyPerson>> GetPersonsAsync(CancellationToken token);
    }
}