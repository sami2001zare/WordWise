using WordWise.Application.Messaging.Command;
using WordWise.Core.Lexikon;

namespace WordWise.Application.App.Language.Commands.CreateLanguage;

public sealed record CreateLanguageCommand(string Title, string NativeTitle, string Abbr) : ICortexCommand<Guid>;
