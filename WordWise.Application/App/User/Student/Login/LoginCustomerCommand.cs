using WordWise.Application.Authentication;
using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Student.Login;

public sealed record LoginCustomerCommand(string EmailOrPhone, string Password, bool RememberMe) : ICortexCommand<AccessToken>;
