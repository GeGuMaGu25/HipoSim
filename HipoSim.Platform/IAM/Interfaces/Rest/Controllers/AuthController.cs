using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HipoSim.Platform.IAM.Application.DataTransferObjects;
using HipoSim.Platform.IAM.Application.UseCases;

namespace HipoSim.Platform.IAM.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignUpUseCase _signUpUseCase;
    private readonly SignInUseCase _signInUseCase;

    public AuthController(SignUpUseCase signUpUseCase, SignInUseCase signInUseCase)
    {
        _signUpUseCase = signUpUseCase;
        _signInUseCase = signInUseCase;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        try
        {
            var message = await _signUpUseCase.ExecuteAsync(request);
            return Ok(new { message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        try
        {
            var response = await _signInUseCase.ExecuteAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }
}