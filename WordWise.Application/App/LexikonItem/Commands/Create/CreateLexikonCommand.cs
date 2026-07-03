using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.LexikonItem.Commands.Create;

public sealed record ExampleSentenceDto(string Text, string? Translation, string? Source);

public sealed record CreateLexikonCommand(
    Guid LexikonPackId,
    string Word,
    string PartOfSpeech,
    int? DifficultyLevelValue,
    string? AudioUrl,
    string? ImageUrl,
    List<ExampleSentenceDto> ExampleSentences,
    List<string> Synonyms,
    List<string> Antonyms
) : ICortexCommand<Guid>;
