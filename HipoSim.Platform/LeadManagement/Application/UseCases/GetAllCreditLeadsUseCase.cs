using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HipoSim.Platform.LeadManagement.Application.DataTransferObjects;
using HipoSim.Platform.LeadManagement.Domain.Model.Repositories;

namespace HipoSim.Platform.LeadManagement.Application.UseCases;

public interface IGetAllCreditLeadsUseCase
{
    Task<IEnumerable<CreditLeadResponse>> ExecuteAsync();
}

public class GetAllCreditLeadsUseCase : IGetAllCreditLeadsUseCase
{
    private readonly ICreditLeadRepository _repository;

    public GetAllCreditLeadsUseCase(ICreditLeadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CreditLeadResponse>> ExecuteAsync()
    {
        var leads = await _repository.ListAsync();

        return leads.Select(lead => new CreditLeadResponse(
            lead.Id,
            lead.CustomerEmail,
            lead.PropertyValue,
            lead.DownPayment,
            lead.LoanAmount,
            lead.MonthlyPayment,
            lead.Currency,
            lead.CreatedAt,
            lead.Status
        ));
    }
}