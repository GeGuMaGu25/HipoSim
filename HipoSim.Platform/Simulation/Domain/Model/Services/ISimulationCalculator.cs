using HipoSim.Platform.Simulation.Domain.Model.Entities;
using HipoSim.Platform.Simulation.Domain.Model.ValueObjects;

namespace HipoSim.Platform.Simulation.Domain.Model.Services;

public interface ISimulationCalculator
{
    Money CalculateMonthlyPayment(CreditSimulation simulation);
}