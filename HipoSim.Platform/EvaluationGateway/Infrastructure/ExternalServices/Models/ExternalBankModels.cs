namespace HipoSim.Platform.EvaluationGateway.Infrastructure.ExternalServices.Models;

public record ExternalBankRequest(
    string ApplicantEmail, 
    decimal RequestedAmount, 
    int TermMonths
);

public record ExternalBankResponse(
    bool IsApproved, 
    string RiskLevel, 
    string ExternalTransactionId
);