using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HipoSim.Platform.EvaluationGateway.Application.UseCases;

namespace HipoSim.Platform.EvaluationGateway.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CreditEvaluationsController : ControllerBase
{
    private readonly EvaluateCreditRiskUseCase _evaluateCreditRiskUseCase;

    public CreditEvaluationsController(EvaluateCreditRiskUseCase evaluateCreditRiskUseCase)
    {
        _evaluateCreditRiskUseCase = evaluateCreditRiskUseCase;
    }

    [HttpPost("{leadId}")]
    public async Task<IActionResult> EvaluateLead(Guid leadId)
    {
        try
        {
            var finalStatus = await _evaluateCreditRiskUseCase.ExecuteAsync(leadId);
            return Ok(new { 
                message = "Evaluación externa completada.", 
                finalStatus = finalStatus 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}