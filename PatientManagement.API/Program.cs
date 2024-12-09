using Microsoft.EntityFrameworkCore;
using PatientManagement.Api.Infrastructure;
using PatientManagement.DataAccess.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiLayer(builder.Configuration.GetConnectionString("PatientManagementDB"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<PatientDbContext>();
    dbContext?.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
