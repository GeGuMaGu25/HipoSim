using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HipoSim.Platform.PropertyCatalog.Application.DataTransferObjects;
using HipoSim.Platform.PropertyCatalog.Domain.Model.Aggregates;
using HipoSim.Platform.PropertyCatalog.Domain.Model.Repositories;

namespace HipoSim.Platform.PropertyCatalog.Application.UseCases;

public class CreateProjectUseCase
{
    private readonly IProjectRepository _repository;

    public CreateProjectUseCase(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> ExecuteAsync(CreateProjectRequest request)
    {
        var project = new RealEstateProject(request.Name, request.Location, request.BasePrice);
        await _repository.AddAsync(project);
        await _repository.SaveChangesAsync();
        return project.Id;
    }
}

public class GetAllProjectsUseCase
{
    private readonly IProjectRepository _repository;

    public GetAllProjectsUseCase(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProjectResponse>> ExecuteAsync()
    {
        var projects = await _repository.ListAsync();
        return projects.Select(p => new ProjectResponse(p.Id, p.Name, p.Location, p.BasePrice));
    }
}