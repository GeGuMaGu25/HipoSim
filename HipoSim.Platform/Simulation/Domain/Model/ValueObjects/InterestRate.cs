using System;

namespace HipoSim.Platform.Simulation.Domain.Model.ValueObjects;

public record InterestRate
{
    public decimal Value { get; init; }

    public InterestRate(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("La tasa de interés no puede ser negativa.", nameof(value));
        }
        Value = value;
    }
}