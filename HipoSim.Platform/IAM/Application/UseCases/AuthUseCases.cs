using System;
using System.Threading.Tasks;
using HipoSim.Platform.IAM.Application.DataTransferObjects;
using HipoSim.Platform.IAM.Domain.Model.Aggregates;
using HipoSim.Platform.IAM.Domain.Model.Repositories;
using HipoSim.Platform.IAM.Infrastructure.Tokens;

namespace HipoSim.Platform.IAM.Application.UseCases;

public class SignUpUseCase
{
    private readonly IUserRepository _repository;

    public SignUpUseCase(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> ExecuteAsync(SignUpRequest request)
    {
        if (await _repository.FindByEmailAsync(request.Email) != null)
            throw new Exception("El correo ya está registrado.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User(request.Email, hashedPassword, request.Role);
        
        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
        
        return "Usuario creado exitosamente.";
    }
}

public class SignInUseCase
{
    private readonly IUserRepository _repository;
    private readonly TokenService _tokenService;

    public SignInUseCase(IUserRepository repository, TokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }

    public async Task<SignInResponse> ExecuteAsync(SignInRequest request)
    {
        var user = await _repository.FindByEmailAsync(request.Email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        var token = _tokenService.GenerateToken(user.Email, user.Role);
        return new SignInResponse(token, "Autenticación exitosa.");
    }
}