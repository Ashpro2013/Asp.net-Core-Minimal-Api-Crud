using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseInMemoryDatabase("DBProduct"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.MapGet("/", async (ProductDbContext db) =>
{
    return await db.Products.ToListAsync();
});

app.MapGet("/products/{id}", async (ProductDbContext db, int id) =>
{
    return await db.Products.FindAsync(id) is Product product ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/products", async (ProductDbContext db, Product product) =>
{
    await db.Products.AddAsync(product);
    await db.SaveChangesAsync();

    return Results.Ok(product);
});

app.MapPut("/products/{id}", async (ProductDbContext db, int id, Product inputproduct) =>
{
    if (await db.Products.FindAsync(id) is Product product)
    {
        product.Name = inputproduct.Name;
        product.Remarks = inputproduct.Remarks;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
});
app.MapDelete("/products/{id}", async (ProductDbContext db, int id) =>
{
    if (await db.Products.FindAsync(id) is Product product)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});
app.Run();

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Remarks { get; set; }
}
