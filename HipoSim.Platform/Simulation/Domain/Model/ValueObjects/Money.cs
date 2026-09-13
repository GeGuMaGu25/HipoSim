namespace HipoSim.Platform.Simulation.Domain.Model.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("El monto no puede ser negativo.", nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("La moneda es obligatoria", nameof(currency));
        }
        
        Amount = amount;
        Currency = currency;
    }
}