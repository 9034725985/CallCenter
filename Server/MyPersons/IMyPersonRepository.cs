using CallCenter.Data.Model;

namespace CallCenter.Server.MyPersons
{
    public interface IMyPersonRepository
    {
        Task CreateAsync(MyPerson person);
    }
}