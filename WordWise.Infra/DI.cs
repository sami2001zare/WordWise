using System;
using System.Collections.Generic;
using System.Text;

namespace WordWise.Infra;

public static class DI
{
    //public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    //{
    //    // 1. Data Access & EF Core
    //    services.AddSingleton<DomainEventInterceptor>();
    //    services.AddDbContext<WordWiseDbContext>((sp, options) =>
    //    {
    //        var interceptor = sp.GetRequiredService<DomainEventInterceptor>();
    //        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
    //               .AddInterceptors(interceptor);
    //    });

    //    services.AddScoped<IUnitOfWork, UnitOfWork>();
    //    services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

    //    // 2. Repositories
    //    services.AddScoped<IStudentRepository, StudentRepository>();
    //    services.AddScoped<ILanguageRepository, LanguageRepository>();
    //    services.AddScoped<ILexikonRepository, LexikonRepository>();
    //    services.AddScoped<ILexikonPackRepository, LexikonPackRepository>();
    //    services.AddScoped<IOneTimePasswordRepository, OneTimePasswordRepository>();

    //    // 3. Caching (Redis)
    //    services.AddStackExchangeRedisCache(options =>
    //    {
    //        options.Configuration = configuration.GetConnectionString("Redis");
    //    });
    //    services.AddSingleton<ICacheService, CacheService>();

    //    // 4. Authentication & Security
    //    services.AddSingleton<IPasswordHasher, PasswordHasher>();
    //    services.AddSingleton<IOtpService, OtpService>();
    //    // Add IJwtService and IRsaKeyProvider implementations here...

    //    // 5. System Services
    //    services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    //    services.AddTransient<ITextMessageService, TextMessageService>();

    //    return services;
    //}
}
