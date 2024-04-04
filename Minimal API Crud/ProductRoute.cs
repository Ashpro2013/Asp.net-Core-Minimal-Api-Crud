using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Minimal_API_Crud
{
    public static class ProductRoute
    {
        public static void ProductsMethod(this WebApplication app)
        {
            app.MapGet("/products", async (ProductDbContext db) =>
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
                    product = new Product(product.Id, inputproduct.Name, inputproduct.Remarks);
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
        }

    }
}
