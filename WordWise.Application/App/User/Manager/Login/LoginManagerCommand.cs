using WordWise.Application.Authentication;
using WordWise.Application.Messaging.Command;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Application.App.User.Manager.Login;

public sealed record LoginManagerCommand(Phone Phone, string Password, bool RememberMe) : ICortexCommand<AccessToken>;
