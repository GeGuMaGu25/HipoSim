using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HipoSim.Platform.LeadManagement.Application.DataTransferObjects;
using HipoSim.Platform.LeadManagement.Application.UseCases;
using Microsoft.AspNetCore.Authorization;

namespace HipoSim.Platform.LeadManagement.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CreditLeadsController : ControllerBase
{
    private readonly ISaveCreditLeadUseCase _saveCreditLeadUseCase;
    private readonly IGetAllCreditLeadsUseCase _getAllCreditLeadsUseCase;

    public CreditLeadsController(
        ISaveCreditLeadUseCase saveCreditLeadUseCase, 
        IGetAllCreditLeadsUseCase getAllCreditLeadsUseCase)
    {
        _saveCreditLeadUseCase = saveCreditLeadUseCase;
        _getAllCreditLeadsUseCase = getAllCreditLeadsUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> SaveLead([FromBody] SaveCreditLeadRequest request)
    {
        try
        {
            var leadId = await _saveCreditLeadUseCase.ExecuteAsync(request);
            return Ok(new { message = "Lead guardado exitosamente", id = leadId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al guardar el lead", details = ex.Message });
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllLeads()
    {
        var leads = await _getAllCreditLeadsUseCase.ExecuteAsync();
        return Ok(leads);
    }
}