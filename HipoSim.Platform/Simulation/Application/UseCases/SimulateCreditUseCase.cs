using HipoSim.Platform.Simulation.Application.DataTransferObjects;
using HipoSim.Platform.Simulation.Domain.Model.Entities;
using HipoSim.Platform.Simulation.Domain.Model.ValueObjects;
using HipoSim.Platform.Simulation.Domain.Services;

namespace HipoSim.Platform.Simulation.Application.UseCases;

public interface ISimulateCreditUseCase
{
    SimulateCreditResponse Execute(SimulateCreditRequest request);
}

public class SimulateCreditUseCase : ISimulateCreditUseCase
{
    private readonly ISimulationCalculator _calculator;

    public SimulateCreditUseCase(ISimulationCalculator calculator)
    {
        _calculator = calculator;
    }

    public SimulateCreditResponse Execute(SimulateCreditRequest request)
    {
        var propertyValue = new Money(request.PropertyValue, request.Currency);
        var downPayment = new Money(request.DownPayment, request.Currency);
        var interestRate = new InterestRate(request.AnnualInterestRate);

        var simulation = new CreditSimulation(propertyValue, downPayment, interestRate, request.TermInYears);

        var loanAmount = simulation.CalculateLoanAmount();
        var monthlyPayment = _calculator.CalculateMonthlyPayment(simulation);

        return new SimulateCreditResponse(
            propertyValue.Amount,
            loanAmount.Amount,
            monthlyPayment.Amount,
            monthlyPayment.Currency
        );
    }
}