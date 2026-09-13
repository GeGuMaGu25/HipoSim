using HipoSim.Platform.Simulation.Application.UseCases;
using HipoSim.Platform.Simulation.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISimulationCalculator, FrenchAmortizationCalculator>();
builder.Services.AddScoped<ISimulateCreditUseCase, SimulateCreditUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();