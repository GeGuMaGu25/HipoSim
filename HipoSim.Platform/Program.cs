using HipoSim.Platform.Simulation.Application.UseCases;
using HipoSim.Platform.Simulation.Domain.Services;
using Microsoft.EntityFrameworkCore;
using HipoSim.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using HipoSim.Platform.LeadManagement.Domain.Model.Repositories;
using HipoSim.Platform.LeadManagement.Infrastructure.Persistence.EFC.Repositories;
using HipoSim.Platform.LeadManagement.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISimulationCalculator, FrenchAmortizationCalculator>();
builder.Services.AddScoped<ISimulateCreditUseCase, SimulateCreditUseCase>();

builder.Services.AddScoped<ICreditLeadRepository, CreditLeadRepository>();
builder.Services.AddScoped<ISaveCreditLeadUseCase, SaveCreditLeadUseCase>();

builder.Services.AddScoped<IGetAllCreditLeadsUseCase, GetAllCreditLeadsUseCase>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();