using CustomerAPI.Domain;
using CustomerAPI.Extention;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using CustomerAPI.Application.Features.Customers.Queries;
using CustomerAPI.Application.Features.Customers.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CustomerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetCustomerByIdQuery>();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", " CustomerAPI v1"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapCustomerEndpoints();

app.Run();

public partial class Program { }
