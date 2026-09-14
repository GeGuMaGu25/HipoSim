using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HipoSim.Platform.PropertyCatalog.Application.DataTransferObjects;
using HipoSim.Platform.PropertyCatalog.Application.UseCases;

namespace HipoSim.Platform.PropertyCatalog.Interfaces.Rest.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly CreateProjectUseCase _createProjectUseCase;
    private readonly GetAllProjectsUseCase _getAllProjectsUseCase;

    public ProjectsController(CreateProjectUseCase createProjectUseCase, GetAllProjectsUseCase getAllProjectsUseCase)
    {
        _createProjectUseCase = createProjectUseCase;
        _getAllProjectsUseCase = getAllProjectsUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        var projectId = await _createProjectUseCase.ExecuteAsync(request);
        return Ok(new { message = "Proyecto registrado", id = projectId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _getAllProjectsUseCase.ExecuteAsync();
        return Ok(projects);
    }
}