using Microsoft.AspNetCore.Mvc;

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
app.MapGet("/api/categories", ([FromQuery] string searchValue = "") =>
{
     
    // search categories using this value
    if(!string.IsNullOrEmpty(searchValue))
    {
        Console.WriteLine($"{searchValue}");
        var searchCategories = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        return Results.Ok(searchCategories);
    }
    return Results.Ok(categories); // 200
});

// POST  /api/categories ==> Create Categories
app.MapPost("/api/categories", ([FromBody] Category categoryData) =>
{
    Console.WriteLine($"{categoryData}");
    var newCategory = new Category
    {
        CategoryId = Guid.NewGuid(),
        Name = categoryData.Name,
        Description = categoryData.Description,
        CreatedAt = DateTime.UtcNow,
    };
    categories.Add(newCategory);
    return Results.Created($"/api/categories/{newCategory.CategoryId}", newCategory); // 200
});

// DELETE  /api/categories/{categoryId} ==> Delete a Categories
app.MapDelete("/api/categories/{categoryId:guid}", (Guid categoryId) =>
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
    if(foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exist");
    }
    categories.Remove(foundCategory);
    return Results.NoContent();
});

// PUT  /api/categories/{categoryId} ==> Update a Categories
app.MapPut("/api/categories/{categoryId:guid}", (Guid categoryId, [FromBody] Category categoryData) =>
{
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
    if(foundCategory == null)
    {
        return Results.NotFound("Category with this id does not exist");
    }
    foundCategory.Name = categoryData.Name;
    foundCategory.Description = categoryData.Description;
    // categories.Remove(foundCategory);
    return Results.NoContent();
});

app.Run();

public record Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

};


// CRUD
// Create ==> Create a category ==> POST: /api/category
// Read ==> Read a category ==> GET: /api/category
// Update ==> Update a category ==> PUT: /api/category
// Delete ==> Delete a category ==> DELETE: /api/category
