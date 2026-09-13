using System;

namespace HipoSim.Platform.LeadManagement.Domain.Model.Aggregates;

public class CreditLead
{
    public Guid Id { get; private set; }
    public string CustomerEmail { get; private set; }
    public decimal PropertyValue { get; private set; }
    public decimal DownPayment { get; private set; }
    public decimal LoanAmount { get; private set; }
    public decimal MonthlyPayment { get; private set; }
    public string Currency { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected CreditLead()
    {
        CustomerEmail = null!;
        Currency = null!;
    }

    public CreditLead(string customerEmail, decimal propertyValue, decimal downPayment, decimal loanAmount, decimal monthlyPayment, string currency)
    {
        Id = Guid.NewGuid();
        CustomerEmail = customerEmail;
        PropertyValue = propertyValue;
        DownPayment = downPayment;
        LoanAmount = loanAmount;
        MonthlyPayment = monthlyPayment;
        Currency = currency;
        CreatedAt = DateTime.UtcNow;
    }
}