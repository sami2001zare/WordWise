using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Student.UpdateMediaProgress;

public sealed record UpdateMediaProgressCommand(
    Guid StudentId,
    Guid TakenMediaId,
    TimeSpan CurrentPosition,
    bool IsCompleted) : ICortexCommand;