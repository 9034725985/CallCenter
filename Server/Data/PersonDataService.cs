using Data;
using Data.Model;

namespace CallCenter.Server.Data
{
    public class PersonDataService
    {
        private readonly PersonData _data;
        public PersonDataService(PersonData data)
        {
            _data = data;
        }
        public async Task<List<MyPerson>> GetPersons(CancellationToken cancellationToken)
        {
            IEnumerable<MyPerson> persons = await _data.GetPersons(cancellationToken);
            return persons.ToList();
        }
    }
}