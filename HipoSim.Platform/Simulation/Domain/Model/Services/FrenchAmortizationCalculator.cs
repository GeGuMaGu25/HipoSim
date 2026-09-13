using System;
using HipoSim.Platform.Simulation.Domain.Model.Entities;
using HipoSim.Platform.Simulation.Domain.Model.ValueObjects;

namespace HipoSim.Platform.Simulation.Domain.Model.Services;

public class FrenchAmortizationCalculator : ISimulationCalculator
{
    public Money CalculateMonthlyPayment(CreditSimulation simulation)
    {
        var principal = simulation.CalculateLoanAmount().Amount;
        var monthlyInterestRate = (simulation.AnnualInterestRate.Value / 100) / 12;
        var totalPayments = simulation.TermInYears * 12;

        if (monthlyInterestRate == 0)
        {
            return new Money(principal / totalPayments, simulation.PropertyValue.Currency);
        }

        var mathPower = (decimal)Math.Pow((double)(1 + monthlyInterestRate), totalPayments);
        var monthlyPayment = principal * (monthlyInterestRate * mathPower) / (mathPower - 1);
        
        return new Money(Math.Round(monthlyPayment, 2), simulation.PropertyValue.Currency);
    }
}