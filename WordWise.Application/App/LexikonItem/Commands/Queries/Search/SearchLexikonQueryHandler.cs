using Dapper;
using WordWise.Application.Data;
using WordWise.Application.Messaging.Query;
using WordWise.Framework;

namespace WordWise.Application.App.LexikonItem.Queries.Search;

internal sealed class SearchLexikonQueryHandler(
    ISqlConnectionFactory _sqlConnectionFactory) : ICortexQueryHandler<SearchLexikonQuery, IReadOnlyList<LexikonItemResult>>
{
    public async Task<Result<IReadOnlyList<LexikonItemResult>>> Handle(SearchLexikonQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT Id, Word, PartOfSpeech, AudioUrl, ImageUrl
            FROM Lexikons
            WHERE Word LIKE @SearchTerm OR Id IN (
                SELECT LexikonId FROM Translation WHERE Content LIKE @SearchTerm
            )
        ";

        var searchTerm = $"%{request.SearchTerm}%";
        var items = await connection.QueryAsync<LexikonItemResult>(sql, new { SearchTerm = searchTerm });

        return items.ToList().AsReadOnly();
    }
}