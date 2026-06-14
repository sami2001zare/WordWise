using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.LexikonPack.Commands.Edit;

public sealed record EditLexikonPackCommand(Guid Id, string Title) : ICortexCommand;
