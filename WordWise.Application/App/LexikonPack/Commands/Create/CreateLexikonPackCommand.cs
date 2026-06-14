using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.LexikonPack.Commands.Create;

public sealed record CreateLexikonPackCommand(string Title, string Language) : ICortexCommand<Guid>;
