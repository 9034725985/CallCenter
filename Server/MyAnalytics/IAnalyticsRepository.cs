using CallCenter.Data.Model;

namespace CallCenter.Server.MyAnalytics;

public interface IAnalyticsRepository
{
    Task CreateAsync(Analytics analytics, CancellationToken token);
    Task<Analytics?> GetAnalyticsAsync(int id, CancellationToken token);
    Task<bool> AnalyticsExistsAsync(int id, CancellationToken token);
    Task<List<Analytics>> GetAllAnalyticsAsync(CancellationToken token);
    Task UpdateAnalyticsAsync(Analytics analytics, CancellationToken token);
    Task DeleteAnalyticsAsync(int id, CancellationToken token);
}

