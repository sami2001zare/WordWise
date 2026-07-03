using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Student.Settings;

public sealed record ToggleRemindersCommand(Guid StudentId, bool Enabled) : ICortexCommand;
