using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Student.StartMedia;

public sealed record StartMediaCommand(Guid StudentId, Guid MediaBaseId) : ICortexCommand<Guid>;
