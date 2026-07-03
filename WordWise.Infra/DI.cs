using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WordWise.Application.Authentication;
using WordWise.Application.Caching;
using WordWise.Application.Clock;
using WordWise.Application.Data;
using WordWise.Core.Language.Repository;
using WordWise.Core.Lexikon.Repository;
using WordWise.Core.User.Repositpry;
using WordWise.Framework.Repository;
using WordWise.Infra.Data;
using WordWise.Infra.Data.Interceptors;
using WordWise.Infra.Repositories;
using WordWise.Infra.Services;

namespace WordWise.Infra;

public static class DI
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Data Access & EF Core
        services.AddSingleton<DomainEventInterceptor>();
        services.AddDbContext<WordWiseDbContext>((sp, options) =>
        {
            var interceptor = sp.GetRequiredService<DomainEventInterceptor>();
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                   .AddInterceptors(interceptor);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        // 2. Repositories
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ILexikonRepository, LexikonRepository>();
        services.AddScoped<ILexikonPackRepository, LexikonPackRepository>();
        services.AddScoped<IOneTimePasswordRepository, OneTimePasswordRepository>();

        // 3. Caching (Redis)
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });
        services.AddSingleton<ICacheService, CacheService>();

        // 4. Authentication & Security
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IOtpService, OtpService>();

        // 5. System Services
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<ITextMessageService, TextMessageService>();

        return services;
    }
}