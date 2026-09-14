using System;

namespace HipoSim.Platform.LeadManagement.Application.DataTransferObjects;

public record SaveCreditLeadRequest(
    Guid ProjectId,
    string CustomerEmail,
    decimal PropertyValue,
    decimal DownPayment,
    decimal LoanAmount,
    decimal MonthlyPayment,
    string Currency
);