using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Student.Review;

public sealed record ReviewVocabularyCommand(Guid StudentId, Guid LexikonId, bool IsRemembered) : ICortexCommand;
