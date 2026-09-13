using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HipoSim.Platform.LeadManagement.Application.DataTransferObjects;
using HipoSim.Platform.LeadManagement.Application.UseCases;

namespace HipoSim.Platform.LeadManagement.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CreditLeadsController : ControllerBase
{
    private readonly ISaveCreditLeadUseCase _saveCreditLeadUseCase;

    public CreditLeadsController(ISaveCreditLeadUseCase saveCreditLeadUseCase)
    {
        _saveCreditLeadUseCase = saveCreditLeadUseCase;
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
}