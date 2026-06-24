var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseHttpsRedirection();

// REST ==> GET, POST, PUT, DELETE

app.MapGet("/", () =>
{
    return "API is working fine.";
});

app.MapGet("/hello", () =>
{
    return "Get method: Hello";
});


app.MapPost("/hello", () =>
{
    return "POST method: Hello";
});

app.MapPut("/hello", () =>
{
    return "PUT method: Hello";
});

app.MapDelete("/hello", () =>
{
    return "DELETE method: Hello";
});

app.Run();