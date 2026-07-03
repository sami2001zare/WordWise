using MediatR;
using Microsoft.AspNetCore.Mvc;
using WordWise.Application.App.User.Student.ChangePassword;
using WordWise.Application.App.User.Student.ForgotPassword;
using WordWise.Application.App.User.Student.Login;
using WordWise.Application.App.User.Student.LoginWithOTP;
using WordWise.Application.App.User.Student.Logout;
using WordWise.Application.App.User.Student.Register;
using WordWise.Application.App.User.Student.ValidateLoginWithOTP;
using WordWise.Application.App.User.Student.ValidateRegistration;

namespace WordWise.Prez.Controllers;

[Route("api/[controller]")]
public sealed class StudentController : ControllerBase
{
    private readonly IMediator _mediator;
    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost("validate-registration")]
    public async Task<IActionResult> ValidateRegistration([FromBody] CustomerVaidateRegisterationCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost("login-otp")]
    public async Task<IActionResult> LoginWithOtp([FromBody] LoginCustomerWithOTPCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok();
    }

    [HttpPost("validate-login-otp")]
    public async Task<IActionResult> ValidateLoginOtp([FromBody] CustomerLoginVaidateWithOTPCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] CustomerChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] CustomerForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }
}