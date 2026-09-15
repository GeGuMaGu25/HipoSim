using System;
using System.Threading.Tasks;
using HipoSim.Platform.EvaluationGateway.Application.ACL;
using HipoSim.Platform.LeadManagement.Domain.Model.Repositories; // Reutilizamos el repositorio del otro módulo

namespace HipoSim.Platform.EvaluationGateway.Application.UseCases;

public class EvaluateCreditRiskUseCase
{
    private readonly ICreditLeadRepository _leadRepository;
    private readonly IBankEvaluationAdapter _bankAdapter;

    public EvaluateCreditRiskUseCase(ICreditLeadRepository leadRepository, IBankEvaluationAdapter bankAdapter)
    {
        _leadRepository = leadRepository;
        _bankAdapter = bankAdapter;
    }

    public async Task<string> ExecuteAsync(Guid leadId)
    {
        // 1. Obtenemos el Lead
        var lead = await _leadRepository.FindByIdAsync(leadId);
        if (lead == null) throw new Exception("Solicitud no encontrada.");

        // 2. Ejecutamos la evaluación en el "Banco" a través de nuestro ACL
        // Asumimos 180 meses (15 años) por simplicidad en la simulación
        var bankResponse = await _bankAdapter.EvaluateCreditAsync(lead.CustomerEmail, lead.LoanAmount, 180);

        // 3. Traducimos la respuesta a nuestros propios estados internos
        var newStatus = bankResponse.IsApproved ? "BankApproved" : "BankRejected";
        
        // 4. Actualizamos y guardamos
        lead.UpdateStatus(newStatus);
        await _leadRepository.SaveChangesAsync();

        return newStatus;
    }
}