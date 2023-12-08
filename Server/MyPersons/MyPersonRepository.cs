using CallCenter.Data;
using CallCenter.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using Polly;
using Polly.Retry;

namespace CallCenter.Server.MyPersons
{
    public class MyPersonRepository : IMyPersonRepository
    {
        private readonly CallCenterDbContext _context;
        private readonly AsyncRetryPolicy _retryPolicy;

        public MyPersonRepository(CallCenterDbContext context)
        {
            _context = context;
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public async Task CreateAsync(MyPerson person)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                await Task.Run(() =>
                {
                    _context.Add(person);
                    _context.SaveChanges();
                });
            });
        }

        public async Task<MyPerson?> GetPersonAsync(int id, CancellationToken token)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                return await _context.Persons.Where(x => x.Id == id).FirstOrDefaultAsync<MyPerson>(token);
            });
        }

        public async Task<List<MyPerson>> GetPersonsAsync(CancellationToken token)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                List<MyPerson> persons = await _context.Persons.ToListAsync(token);
                return persons ?? new();
            });
        }

        public async Task<bool> GetPersonExistsAsync(int id, CancellationToken token)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                return await _context.Persons.AnyAsync(x => x.Id == id, token);
            });
        }

        public async Task<MyInteger> PutPersonAsync(MyPerson person, CancellationToken token)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                _context.Persons.Update(person);
                await _context.SaveChangesAsync(token);
            });
            return new();
        }

        public async Task<MyInteger> PutPersonsAsync(List<MyPerson> persons, CancellationToken token)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                foreach (MyPerson person in persons)
                {
                    _context.Persons.Update(person);
                }
                await _context.SaveChangesAsync(token);
            });
            return new();
        }

    }
}
