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

List<Category> categories = new List<Category>();

// REST ==> GET, POST, PUT, DELETE
app.MapGet("/", () => "API is working fine.");

// GET  /api/categories ==> Read Categories
app.MapGet("/api/categories", () =>
{
    return Results.Ok(categories); // 200
});

// POST  /api/categories ==> Create Categories
app.MapPost("/api/categories", () =>
{
    var newCategory = new Category
    {
        CategoryId = Guid.Parse("d24cdbed-dea9-4416-bdf7-183816a43e63"),
        Name = "Electronics",
        Description = "Devices an gadgets including phone, laptop and other accesories",
        CreatedAt = DateTime.UtcNow,
    };
    categories.Add(newCategory);
    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory); // 200
});

// DELETE  /api/categories ==> Delete a Categories
app.MapDelete("/api/categories", () =>
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == Guid.Parse("4681c82f-eb5e-435f-9638-b6d2e1d6efcd"));
    if(foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exist");
    }
    categories.Remove(foundCategory);
    return Results.NoContent();
});

// PUT  /api/categories ==> Update a Categories
app.MapPut("/api/categories", () =>
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == Guid.Parse("d24cdbed-dea9-4416-bdf7-183816a43e63"));
    if(foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exist");
    }
    foundCategory.Name = "Smart Phone";
    foundCategory.Description = "Smart Phone is a nice category";
    // categories.Remove(foundCategory);
    return Results.NoContent();
});

app.Run();

public record Category
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

};


// CRUD
// Create ==> Create a category ==> POST: /api/category
// Read ==> Read a category ==> GET: /api/category
// Update ==> Update a category ==> PUT: /api/category
// Delete ==> Delete a category ==> DELETE: /api/category
