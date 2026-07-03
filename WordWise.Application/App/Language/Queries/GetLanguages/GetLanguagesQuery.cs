using Dapper;
using WordWise.Application.Caching;
using WordWise.Application.Data;
using WordWise.Application.Messaging.Query;
using WordWise.Framework;

namespace WordWise.Application.App.Language.Queries.GetLanguages;

public sealed record LanguageQueryResult(Guid Id, string Title);

public sealed record GetLanguagesQuery : ICortexQuery<IReadOnlyList<LanguageQueryResult>>, ICachedQuery
{
    public string CacheKey => "languages-all";
    public TimeSpan? Expiration => TimeSpan.FromHours(24);
}

internal sealed class GetLanguagesQueryHandler(
    ISqlConnectionFactory _sqlConnectionFactory) : ICortexQueryHandler<GetLanguagesQuery, IReadOnlyList<LanguageQueryResult>>
{
    public async Task<Result<IReadOnlyList<LanguageQueryResult>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT Id, Title 
            FROM Languages
        ";

        var items = await connection.QueryAsync<LanguageQueryResult>(sql);

        return items.ToList().AsReadOnly();
    }
}