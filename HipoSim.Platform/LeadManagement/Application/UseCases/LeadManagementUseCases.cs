using System;
using System.Linq;
using System.Threading.Tasks;
using HipoSim.Platform.LeadManagement.Application.DataTransferObjects;
using HipoSim.Platform.LeadManagement.Domain.Model.Repositories;

namespace HipoSim.Platform.LeadManagement.Application.UseCases;

public class GetDashboardMetricsUseCase
{
    private readonly ICreditLeadRepository _repository;

    public GetDashboardMetricsUseCase(ICreditLeadRepository repository)
    {
        _repository = repository;
    }

    public async Task<DashboardMetricsResponse> ExecuteAsync()
    {
        var leads = await _repository.ListAsync();
        var today = DateTime.UtcNow.Date;

        var totalLeads = leads.Count();
        var leadsToday = leads.Count(l => l.CreatedAt.Date == today);
        var leadsThisMonth = leads.Count(l => l.CreatedAt.Month == today.Month && l.CreatedAt.Year == today.Year);
        var leadsThisYear = leads.Count(l => l.CreatedAt.Year == today.Year);

        var sales = leads.Where(l => l.Status == "Sold");
        var totalSales = sales.Count();
        var totalSalesVolume = sales.Sum(l => l.PropertyValue);

        return new DashboardMetricsResponse(totalLeads, leadsToday, leadsThisMonth, leadsThisYear, totalSales, totalSalesVolume);
    }
}

public class UpdateLeadStatusUseCase
{
    private readonly ICreditLeadRepository _repository;

    public UpdateLeadStatusUseCase(ICreditLeadRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Guid leadId, string newStatus)
    {
        var lead = await _repository.FindByIdAsync(leadId);
        if (lead == null) throw new Exception("Lead no encontrado.");

        lead.UpdateStatus(newStatus);
        await _repository.SaveChangesAsync();
    }
}