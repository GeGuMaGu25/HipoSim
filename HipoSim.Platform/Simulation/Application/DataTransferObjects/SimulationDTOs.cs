namespace HipoSim.Platform.Simulation.Application.DataTransferObjects;

public record SimulateCreditRequest(
    decimal PropertyValue,
    decimal DownPayment,
    string Currency,
    decimal AnnualInterestRate,
    int TermInYears);
    
    public record SimulateCreditResponse(
        decimal PropertyAmount,
        decimal LoanAmount,
        decimal MonthlyPayment,
        string Currency);