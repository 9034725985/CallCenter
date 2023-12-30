using CallCenter.Data;
using CallCenter.Data.Model;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Polly.Bulkhead;

namespace CallCenter.Server.MyPersons
{
    public class MyPersonRepository(CallCenterDbContext context) : IMyPersonRepository
    {
        private readonly AsyncRetryPolicy _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        private readonly AsyncBulkheadPolicy _bulkheadPolicy = Policy.BulkheadAsync(5, 10);

        public async Task CreateAsync(MyPerson person)
        {
            await _bulkheadPolicy.ExecuteAsync(async () =>
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    await Task.Run(() =>
                    {
                        context.Add(person);
                        context.SaveChanges();
                    });
                });
            });
        }

        public async Task<MyPerson?> GetPersonAsync(int id, CancellationToken token)
        {
            return await _bulkheadPolicy.ExecuteAsync(async () =>
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    return await context.Persons.Where(x => x.Id == id).FirstOrDefaultAsync<MyPerson>(token);
                });
            });
        }

        public async Task<List<MyPerson>> GetPersonsAsync(CancellationToken token)
        {
            return await _bulkheadPolicy.ExecuteAsync(async () =>
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    List<MyPerson> persons = await context.Persons.ToListAsync(token);
                    return persons ?? new();
                });
            });
        }

        public async Task<bool> GetPersonExistsAsync(int id, CancellationToken token)
        {
            return await _bulkheadPolicy.ExecuteAsync(async () =>
            {
                return await _retryPolicy.ExecuteAsync(async () =>
                {
                    return await context.Persons.AnyAsync(x => x.Id == id, token);
                });
            });
        }

        public async Task<MyInteger> PutPersonAsync(MyPerson person, CancellationToken token)
        {
            await _bulkheadPolicy.ExecuteAsync(async () =>
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    context.Persons.Update(person);
                    await context.SaveChangesAsync(token);
                });
            });
            return new();
        }

        public async Task<MyInteger> PutPersonsAsync(List<MyPerson> persons, CancellationToken token)
        {
            await _bulkheadPolicy.ExecuteAsync(async () =>
            {
                await _retryPolicy.ExecuteAsync(async () =>
                {
                    foreach (MyPerson person in persons)
                    {
                        context.Persons.Update(person);
                    }
                    await context.SaveChangesAsync(token);
                });
            });
            return new();
        }
    }
}
