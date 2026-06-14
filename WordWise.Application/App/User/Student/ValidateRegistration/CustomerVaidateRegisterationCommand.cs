using WordWise.Application.Messaging.Command;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Application.App.User.Student.ValidateRegistration;

public sealed record CustomerVaidateRegisterationCommand(Phone Phone, string OTP) : ICortexCommand<string>;
