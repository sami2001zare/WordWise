using MediatR;
using Microsoft.AspNetCore.Mvc;
using WordWise.Application.App.User.Manager.ChangePassword;
using WordWise.Application.App.User.Manager.ForgotPassword;
using WordWise.Application.App.User.Manager.Login;
using WordWise.Application.App.User.Manager.Logout;

namespace WordWise.Prez.Controllers;

[Route("api/[controller]")]
public sealed class ManagerController : ControllerBase
{
    private readonly IMediator _mediator;

    public ManagerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginManagerCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutAdminCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ManagerChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ManagerForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }
}
