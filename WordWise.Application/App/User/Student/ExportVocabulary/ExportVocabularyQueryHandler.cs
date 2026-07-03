using Dapper;
using System.Text;
using WordWise.Application.Data;
using WordWise.Application.Messaging.Query;
using WordWise.Framework;

namespace WordWise.Application.App.User.Student.ExportVocabulary;

internal sealed class ExportVocabularyQueryHandler(
    ISqlConnectionFactory _sqlConnectionFactory) : ICortexQueryHandler<ExportVocabularyQuery, byte[]>
{
    public async Task<Result<byte[]>> Handle(ExportVocabularyQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = @"
            SELECT 
                l.Word, 
                l.PartOfSpeech, 
                sv.SavedAt, 
                sv.ReviewCount, 
                sv.NextReviewDate
            FROM SavedVocabularies sv
            INNER JOIN Lexikons l ON sv.LexikonId = l.Id
            WHERE sv.StudentId = @StudentId
            ORDER BY sv.SavedAt ASC
        ";

        var vocabularies = await connection.QueryAsync<ExportVocabularyDto>(sql, new { StudentId = request.StudentId });

        var sb = new StringBuilder();
        sb.AppendLine("Word,PartOfSpeech,SavedAt,ReviewCount,NextReviewDate");

        foreach (var vocab in vocabularies)
        {
            sb.AppendLine($"{vocab.Word},{vocab.PartOfSpeech},{vocab.SavedAt:yyyy-MM-dd HH:mm},{vocab.ReviewCount},{vocab.NextReviewDate:yyyy-MM-dd HH:mm}");
        }

        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        return Result.Success(bytes);
    }

    private sealed record ExportVocabularyDto(string Word, string PartOfSpeech, DateTime SavedAt, int ReviewCount, DateTime NextReviewDate);
}
