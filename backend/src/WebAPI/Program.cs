using Application;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.ExampleItems.Commands;
using Application.Features.ExampleItems.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS for frontend development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // Or your frontend dev URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); // Use the CORS policy

app.UseAuthorization();

// Minimal API for MediatR (can be replaced with controllers)
app.MapGet("/api/ExampleItems", async (IMediator mediator) => 
{
    return await mediator.Send(new GetExampleItemsQuery());
})
.WithName("GetExampleItems")
.WithOpenApi();

app.MapPost("/api/ExampleItems", async ([FromBody] CreateExampleItemCommand command, IMediator mediator) => 
{
    var id = await mediator.Send(command);
    return Results.Created($"/api/ExampleItems/{id}", id);
})
.WithName("CreateExampleItem")
.WithOpenApi();

app.MapControllers(); // Enable attribute-routed controllers

app.Run();