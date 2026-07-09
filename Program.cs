using asp_net_ecommerce_web_api.Controllers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


// Add services to the controller
// builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
//     options.SuppressModelStateInvalidFilter = true // Disable automatic model validation response
// );
builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
          .Where(e => e.Value != null && e.Value.Errors.Count > 0)
          .SelectMany(e => e.Value?.Errors != null ? e.Value.Errors.Select(x => x.ErrorMessage) : new List<string>()).ToList();

        return new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(errors, 400, "Validation failed"));
    };
});

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

app.MapControllers();

app.Run();


// CRUD
// Create ==> Create a category ==> POST: /api/category
// Read ==> Read a category ==> GET: /api/category
// Update ==> Update a category ==> PUT: /api/category
// Delete ==> Delete a category ==> DELETE: /api/category

// MVC = Model, View, Controllers