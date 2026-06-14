using WordWise.Application.Messaging.Command;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Application.App.User.Manager.ForgotPassword;

public sealed record ManagerForgotPasswordCommand(Guid Id) : ICortexCommand<string>;
