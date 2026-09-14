using System;
using System.Threading.Tasks;
using HipoSim.Platform.LeadManagement.Application.DataTransferObjects;
using HipoSim.Platform.LeadManagement.Domain.Model.Aggregates;
using HipoSim.Platform.LeadManagement.Domain.Model.Repositories;

namespace HipoSim.Platform.LeadManagement.Application.UseCases;

public interface ISaveCreditLeadUseCase
{
    Task<Guid> ExecuteAsync(SaveCreditLeadRequest request);
}

public class SaveCreditLeadUseCase : ISaveCreditLeadUseCase
{
    private readonly ICreditLeadRepository _repository;

    public SaveCreditLeadUseCase(ICreditLeadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> ExecuteAsync(SaveCreditLeadRequest request)
    {
        var lead = new CreditLead(
            request.ProjectId,
            request.CustomerEmail,
            request.PropertyValue,
            request.DownPayment,
            request.LoanAmount,
            request.MonthlyPayment,
            request.Currency
        );

        await _repository.AddAsync(lead);
        await _repository.SaveChangesAsync();

        return lead.Id;
    }
}