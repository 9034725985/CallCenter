using CallCenter.Data;
using CallCenter.Data.Model;

namespace CallCenter.Server.Data;

public class PersonDataService
{
    private readonly PersonDataAccess _data;
    public PersonDataService(PersonDataAccess data)
    {
        _data = data;
    }
    public async Task<List<MyPerson>> GetPersons(CancellationToken cancellationToken)
    {
        IEnumerable<MyPerson> persons = await _data.GetPersons(cancellationToken);
        return persons.ToList();
    }

    public async Task<MyInteger> Put(MyPerson person, CancellationToken cancellationToken)
    {
        MyInteger myInteger = await _data.UpdateMyPerson(person, cancellationToken);
        return myInteger;
    }
}