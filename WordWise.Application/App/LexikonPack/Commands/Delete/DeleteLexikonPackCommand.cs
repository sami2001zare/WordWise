using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.LexikonPack.Commands.Delete;

public sealed record DeleteLexikonPackCommand(Guid Id) : ICortexCommand<Guid>;
