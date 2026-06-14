using WordWise.Application.Messaging.Command;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Application.App.User.Student.ForgotPassword;

public sealed record CustomerForgotPasswordCommand(Phone Phone) : ICortexCommand<string>;
