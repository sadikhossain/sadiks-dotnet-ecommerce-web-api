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
                  .Select(e => new
                  {
                      Feild = e.Key,
                      Errors = e.Value != null ? e.Value.Errors.Select(x => x.ErrorMessage).ToArray() : new string[0]
                  }).ToList();

        // var errorString = string.Join("; ", errors.Select(e => $"{e.Feild} : {string.Join(", ", e.Errors)}"));
        return new BadRequestObjectResult(new
        {
            Message = "Validation failed",
            Errors = errors
        });
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