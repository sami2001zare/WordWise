using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Student.SaveVocabulary;

public sealed record SaveVocabularyCommand(Guid StudentId, Guid LexikonId) : ICortexCommand<Guid>;
