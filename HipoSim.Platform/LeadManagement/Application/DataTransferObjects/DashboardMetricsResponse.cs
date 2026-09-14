namespace HipoSim.Platform.LeadManagement.Application.DataTransferObjects;

public record DashboardMetricsResponse(
    int TotalLeads,
    int LeadsToday,
    int LeadsThisMonth,
    int LeadsThisYear,
    int TotalSales,
    decimal TotalSalesVolume
);