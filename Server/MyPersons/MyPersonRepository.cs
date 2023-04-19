using CallCenter.Data;
using CallCenter.Data.Model;

namespace CallCenter.Server.MyPersons;

public class MyPersonRepository : IMyPersonRepository
{
    private readonly CallCenterDbContext _context;

    public async Task CreateAsync(MyPerson person)
    {
        await Task.Run(() =>
        {
            _context.Add(person);
            _context.SaveChanges();
        });
    }
}
