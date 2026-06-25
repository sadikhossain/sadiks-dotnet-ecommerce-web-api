var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// REST ==> GET, POST, PUT, DELETE

app.MapGet("/", () => "API is working fine.");

var products = new List<Product>()
{
    new Product("Samsung s20", 1250),
    new Product("Apple iphone", 1367),
};

app.MapGet("/products", () =>
{

    return Results.Ok(products); // 200
});


app.Run();

public record Product(string Name, decimal Price);
