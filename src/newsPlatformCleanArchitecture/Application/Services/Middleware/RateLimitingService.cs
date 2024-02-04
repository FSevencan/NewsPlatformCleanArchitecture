using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Middleware;
public class RateLimitingService
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _timeSpan;
    private readonly int _maxRequestLimit;

    public RateLimitingService(IDistributedCache cache, IConfiguration configuration)
    {
        _cache = cache;
        _timeSpan = TimeSpan.FromMinutes(1);
        _maxRequestLimit = 3;
    }

    public async Task<bool> IsRateLimitExceeded(string entityType, string userId)
    {
        var key = $"{entityType}_RateLimit_{userId}";
        var currentCount = await _cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(currentCount))
        {
            await _cache.SetStringAsync(key, "1", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _timeSpan
            });
            return false;
        }

        var count = int.Parse(currentCount);
        if (count >= _maxRequestLimit)
        {
            return true;
        }

        await _cache.SetStringAsync(key, (count + 1).ToString(), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _timeSpan
        });

        return false;
    }
}