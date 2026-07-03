using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text.Json;
using WordWise.Application.Authentication;
using WordWise.Application.Caching;
using WordWise.Application.Clock;
using WordWise.Application.Generator;
using WordWise.Core.User;

namespace WordWise.Infra.Services;

internal sealed class TextMessageService(ILogger<TextMessageService> logger) : ITextMessageService
{
    public Task SendForgotPasswordAsync(string phone, string randomPass, CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"Sending Forgot Password {randomPass} to Phone {phone}");
        return Task.CompletedTask;
    }

    public Task SendOTP(string phone, string code, CancellationToken cancellationToken = default)
    {
        // Integration with Twilio or equivalent would go here
        logger.LogInformation($"Sending OTP {code} to Phone {phone}");
        return Task.CompletedTask;
    }

    public Task SendOTP(string phone, int otp, CancellationToken cancellationToken = default)
    {
        //throw new NotImplementedException();
        return Task.CompletedTask;
    }
}


internal sealed class OtpService : IOtpService
{
   
    public int Generate(int length = 6)
    {
        return Random.Shared.Next(100000, 999999);
    }

    public bool IsExpired(DateTime expiresAt)
    {
        return DateTime.UtcNow > expiresAt;
    }
}

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

internal sealed class IdGenerator : IIdGenerator
{
    public Task<string> GenerateRandomPassword()
    {
        return Task.FromResult(Random.Shared.Next(100000, 999999).ToString());
    }

    public Task<string> GenerateSerial()
    {
        return Task.FromResult(Random.Shared.Next(10000000, 99999999).ToString());
    }
}

internal sealed class JwtService : IJwtService
{
    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetAccessTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AccessToken> GetAccessTokenWithMetadataAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public string? GetJtiFromToken(string token)
    {
        throw new NotImplementedException();
    }

    public string HashToken(string rawToken)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal? ValidateAccessToken(string token)
    {
        throw new NotImplementedException();
    }
}


internal sealed class CacheService(IDistributedCache distributedCache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        string? cachedValue = await distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedValue is null) return default;
        return JsonSerializer.Deserialize<T>(cachedValue);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return distributedCache.RemoveAsync(key, cancellationToken);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        string serializedValue = JsonSerializer.Serialize(value);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(10)
        };
        return distributedCache.SetStringAsync(key, serializedValue, options, cancellationToken);
    }
}