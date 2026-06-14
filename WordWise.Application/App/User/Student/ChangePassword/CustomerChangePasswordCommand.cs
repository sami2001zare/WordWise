using WordWise.Application.Messaging.Command;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Application.App.User.Student.ChangePassword;

public sealed record CustomerChangePasswordCommand(Phone Phone, string NewPassword) : ICortexCommand<bool>;
