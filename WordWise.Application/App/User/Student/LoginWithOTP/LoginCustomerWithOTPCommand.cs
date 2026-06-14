using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Student.LoginWithOTP;

public sealed record LoginCustomerWithOTPCommand(string EmailOrPhone) : ICortexCommand;
