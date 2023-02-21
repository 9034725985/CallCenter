namespace Data;

public interface IPersonData
{
    Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken);
    Task<int> UpdateMyPerson(MyPerson person, CancellationToken cancellationToken);
}
