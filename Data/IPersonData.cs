namespace Data;

public interface IPersonData
{
    Task<IEnumerable<MyPerson>> GetPersons(CancellationToken cancellationToken);
}
