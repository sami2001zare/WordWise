using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Student.Settings;

public sealed record SetReminderFrequencyCommand(Guid StudentId, string Frequency) : ICortexCommand;
