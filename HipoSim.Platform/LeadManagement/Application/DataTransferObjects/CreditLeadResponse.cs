using System;

namespace HipoSim.Platform.LeadManagement.Application.DataTransferObjects;

public record CreditLeadResponse(
    Guid Id,
    string CustomerEmail,
    decimal PropertyValue,
    decimal DownPayment,
    decimal LoanAmount,
    decimal MonthlyPayment,
    string Currency,
    DateTime CreatedAt
);