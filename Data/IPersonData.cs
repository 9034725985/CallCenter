using CallCenter.Data.Model;

namespace CallCenter.Data;

public interface IPersonDataAccess
{
    Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken);
    Task<MyInteger> UpdateMyPerson(MyPerson person, CancellationToken cancellationToken);
}
