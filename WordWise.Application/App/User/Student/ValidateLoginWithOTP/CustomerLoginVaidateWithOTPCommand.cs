using WordWise.Application.Messaging.Command;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Application.App.User.Student.ValidateLoginWithOTP;

public sealed record CustomerLoginVaidateWithOTPCommand(Phone Phone, string OTP) : ICortexCommand<string>;
