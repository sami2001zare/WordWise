using Dapper;
using WordWise.Application.Clock;
using WordWise.Application.Data;
using WordWise.Application.Messaging.Query;
using WordWise.Framework;

namespace WordWise.Application.App.User.Student.Vocabulary;

internal sealed class GetDueVocabulariesQueryHandler(
    ISqlConnectionFactory _sqlConnectionFactory,
    IDateTimeProvider _dateTimeProvider) : ICortexQueryHandler<GetDueVocabulariesQuery, IReadOnlyList<DueVocabularyResult>>
{
    public async Task<Result<IReadOnlyList<DueVocabularyResult>>> Handle(GetDueVocabulariesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT 
                sv.Id AS SavedVocabularyId, 
                l.Id AS LexikonId, 
                l.Word, 
                l.PartOfSpeech, 
                l.AudioUrl, 
                l.ImageUrl
            FROM SavedVocabularies sv
            INNER JOIN Lexikons l ON sv.LexikonId = l.Id
            WHERE sv.StudentId = @StudentId
              AND sv.NextReviewDate <= @CurrentDate
        ";

        var items = await connection.QueryAsync<DueVocabularyResult>(sql, new
        {
            StudentId = request.StudentId,
            CurrentDate = _dateTimeProvider.UtcNow
        });

        return items.ToList().AsReadOnly();
    }
}
