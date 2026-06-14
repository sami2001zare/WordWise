using WordWise.Application.Messaging.Command;

namespace WordWise.Application.App.User.Manager.Logout;

public sealed record LogoutAdminCommand : ICortexCommand<bool>;
