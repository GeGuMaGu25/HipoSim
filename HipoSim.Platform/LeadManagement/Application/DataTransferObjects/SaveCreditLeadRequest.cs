namespace HipoSim.Platform.LeadManagement.Application.DataTransferObjects;

public record SaveCreditLeadRequest(
    string CustomerEmail,
    decimal PropertyValue,
    decimal DownPayment,
    decimal LoanAmount,
    decimal MonthlyPayment,
    string Currency
);