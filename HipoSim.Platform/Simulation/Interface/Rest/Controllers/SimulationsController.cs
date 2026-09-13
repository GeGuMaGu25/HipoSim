using System;
using Microsoft.AspNetCore.Mvc;
using HipoSim.Platform.Simulation.Application.DataTransferObjects;
using HipoSim.Platform.Simulation.Application.UseCases;

namespace HipoSim.Platform.Simulation.Interface.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SimulationsController : ControllerBase
{
    private readonly ISimulateCreditUseCase _simulateCreditUseCase;

    public SimulationsController(ISimulateCreditUseCase simulateCreditUseCase)
    {
        _simulateCreditUseCase = simulateCreditUseCase;
    }

    [HttpPost]
    public IActionResult SimulateCredit([FromBody] SimulateCreditRequest request)
    {
        try
        {
            var response = _simulateCreditUseCase.Execute(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {error = ex.Message});
        }
    }
}