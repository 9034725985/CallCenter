using CallCenter.Data.Model;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Polly.Bulkhead;
using CallCenter.Data;

namespace CallCenter.Server.MyAnalytics;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly CallCenterDbContext _context;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly AsyncBulkheadPolicy _bulkheadPolicy;

    public AnalyticsRepository(CallCenterDbContext context)
    {
        _context = context;
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        _bulkheadPolicy = Policy.BulkheadAsync(5, 10);
    }

    public async Task CreateAsync(Analytics analytics, CancellationToken token)
    {
        await _bulkheadPolicy.ExecuteAsync(async () =>
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                await _context.Analytics.AddAsync(analytics, token);
                await _context.SaveChangesAsync(token);
            });
        });
    }

    public async Task<Analytics?> GetAnalyticsAsync(int id, CancellationToken token)
    {
        return await _bulkheadPolicy.ExecuteAsync(async () =>
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                return await _context.Analytics.FindAsync(new object[] { id }, token);
            });
        });
    }

    public async Task<List<Analytics>> GetAllAnalyticsAsync(CancellationToken token)
    {
        return await _bulkheadPolicy.ExecuteAsync(async () =>
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                return await _context.Analytics.ToListAsync(token);
            });
        });
    }

    public async Task<bool> AnalyticsExistsAsync(int id, CancellationToken token)
    {
        return await _bulkheadPolicy.ExecuteAsync(async () =>
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                return await _context.Analytics.AnyAsync(a => a.Id == id, token);
            });
        });
    }

    public async Task UpdateAnalyticsAsync(Analytics analytics, CancellationToken token)
    {
        await _bulkheadPolicy.ExecuteAsync(async () =>
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                _context.Analytics.Update(analytics);
                await _context.SaveChangesAsync(token);
            });
        });
    }

    public async Task DeleteAnalyticsAsync(int id, CancellationToken token)
    {
        await _bulkheadPolicy.ExecuteAsync(async () =>
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                var analytics = await _context.Analytics.FindAsync(new object[] { id }, token);
                if (analytics != null)
                {
                    _context.Analytics.Remove(analytics);
                    await _context.SaveChangesAsync(token);
                }
            });
        });
    }
}

