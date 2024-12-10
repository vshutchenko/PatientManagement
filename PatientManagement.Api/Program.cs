using Microsoft.EntityFrameworkCore;
using PatientManagement.Api.Infrastructure;
using PatientManagement.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiLayer(builder.Configuration.GetConnectionString("PatientManagementDB"));

var app = builder.Build();

app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
