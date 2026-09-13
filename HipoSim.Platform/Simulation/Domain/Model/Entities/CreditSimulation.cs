using System;
using HipoSim.Platform.Simulation.Domain.Model.ValueObjects;

namespace HipoSim.Platform.Simulation.Domain.Model.Entities;

public class CreditSimulation
{
    public Guid Id { get; private set; }
    public Money PropertyValue { get; private set; }
    public Money DownPayment { get; private set; }
    public InterestRate AnnualInterestRate { get; private set; }
    public int TermInYears { get; private set; }

    public CreditSimulation(Money propertyValue, Money downPayment, InterestRate annualInterestRate, int termInYears)
    {
        if (termInYears <= 0 || termInYears > 30)
        {
            throw new ArgumentException("El plazo debe ser mayor a 0 y hasta un máximo de 30 años.", nameof(termInYears));
        }

        if (downPayment.Amount >= propertyValue.Amount)
        {
            throw new ArgumentException("La cuota inicial no puede ser mayor o igual al valor de inmueble.");
        }

        if (propertyValue.Currency != downPayment.Currency)
        {
            throw new ArgumentException("La moneda del inmueble y la cuota inicial deben coincidir.");
        }
        
        Id = Guid.NewGuid();
        DownPayment = downPayment;
        AnnualInterestRate = annualInterestRate;
        TermInYears = termInYears;
        PropertyValue = propertyValue;
    }

    public Money CalculateLoanAmount()
    {
        return new Money(PropertyValue.Amount - DownPayment.Amount, PropertyValue.Currency);
    }
}