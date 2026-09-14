using Microsoft.AspNetCore.Mvc;
using HipoSim.Platform.IAM.Application.DataTransferObjects;
using HipoSim.Platform.IAM.Infrastructure.Tokens;

namespace HipoSim.Platform.IAM.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("sign-in")]
    public IActionResult SignIn([FromBody] SignInRequest request)
    {
        // Validación de administrador estático para este MVP
        if (request.Email == "admin@hiposim.com" && request.Password == "admin123")
        {
            var token = _tokenService.GenerateToken(request.Email);
            return Ok(new SignInResponse(token, "Autenticación exitosa"));
        }

        return Unauthorized(new { error = "Credenciales inválidas" });
    }
}