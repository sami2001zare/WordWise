using WordWise.Application.Messaging.Command;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Application.App.User.Manager.ChangePassword;

public sealed record ManagerChangePasswordCommand(Guid Id, string NewPassword) : ICortexCommand<bool>;
