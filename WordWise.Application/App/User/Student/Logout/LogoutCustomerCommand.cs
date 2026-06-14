using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Student.Logout;

public sealed record LogoutCustomerCommand : ICortexCommand<bool>;
