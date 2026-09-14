namespace HipoSim.Platform.IAM.Application.DataTransferObjects;

public record SignUpRequest(string Email, string Password, string Role);