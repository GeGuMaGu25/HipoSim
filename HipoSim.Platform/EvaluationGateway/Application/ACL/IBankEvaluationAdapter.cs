using System;
using System.Threading.Tasks;
using HipoSim.Platform.EvaluationGateway.Infrastructure.ExternalServices.Models;

namespace HipoSim.Platform.EvaluationGateway.Application.ACL;

public interface IBankEvaluationAdapter
{
    // El método recibe tipos primitivos o modelos de nuestro dominio interno
    Task<ExternalBankResponse> EvaluateCreditAsync(string email, decimal amount, int months);
}

public class BankEvaluationAdapter : IBankEvaluationAdapter
{
    public async Task<ExternalBankResponse> EvaluateCreditAsync(string email, decimal amount, int months)
    {
        // 1. Transformación (Traducción al modelo externo)
        var externalRequest = new ExternalBankRequest(email, amount, months);

        // 2. Simulación de llamada HTTP al banco (retraso de 1.5s)
        await Task.Delay(1500); 

        // 3. Regla de negocio simulada del banco (Aprueba si el monto es <= $150,000)
        bool isApproved = externalRequest.RequestedAmount <= 150000;
        string risk = isApproved ? "LOW_RISK" : "HIGH_RISK";

        // 4. Retornar la respuesta del banco
        return new ExternalBankResponse(isApproved, risk, Guid.NewGuid().ToString());
    }
}