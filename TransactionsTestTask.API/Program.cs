using Microsoft.OpenApi.Models;
using TransactionsTestTask.API.Extensions;
using TransactionsTestTask.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureCors();
builder.Services.ConfigureIdentity();
builder.Services.RegisterDependencies();
builder.Services.ConfigureAuthentication(builder);

builder.ConfigureDbContext();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AnyOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
