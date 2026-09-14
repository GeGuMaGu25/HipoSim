using HipoSim.Platform.Simulation.Application.UseCases;
using HipoSim.Platform.Simulation.Domain.Services;
using Microsoft.EntityFrameworkCore;
using HipoSim.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using HipoSim.Platform.LeadManagement.Domain.Model.Repositories;
using HipoSim.Platform.LeadManagement.Infrastructure.Persistence.EFC.Repositories;
using HipoSim.Platform.LeadManagement.Application.UseCases;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HipoSim.Platform.IAM.Infrastructure.Tokens;
using HipoSim.Platform.IAM.Domain.Model.Repositories;
using HipoSim.Platform.IAM.Infrastructure.Persistence.EFC.Repositories;
using HipoSim.Platform.IAM.Application.UseCases;

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

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<SignUpUseCase>();
builder.Services.AddScoped<SignInUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();