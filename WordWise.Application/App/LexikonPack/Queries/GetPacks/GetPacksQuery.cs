using Dapper;
using WordWise.Application.Caching;
using WordWise.Application.Data;
using WordWise.Application.Messaging.Query;
using WordWise.Framework;

namespace WordWise.Application.App.LexikonPack.Queries.GetPacks;

public sealed record LexikonPackQueryResult(Guid Id, string Title, string Language);

public sealed record GetPacksQuery : ICortexQuery<IReadOnlyList<LexikonPackQueryResult>>, ICachedQuery
{
    public string CacheKey => "lexikon-packs-all";
    public TimeSpan? Expiration => TimeSpan.FromHours(1);
}

internal sealed class GetPacksQueryHandler(
    ISqlConnectionFactory _sqlConnectionFactory) : ICortexQueryHandler<GetPacksQuery, IReadOnlyList<LexikonPackQueryResult>>
{
    public async Task<Result<IReadOnlyList<LexikonPackQueryResult>>> Handle(GetPacksQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT p.Id, p.Title, l.Title AS Language
            FROM LexikonPacks p
            INNER JOIN Languages l ON p.LanguageId = l.Id
        ";

        var items = await connection.QueryAsync<LexikonPackQueryResult>(sql);

        return items.ToList().AsReadOnly();
    }
}