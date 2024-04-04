using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minimal_API_Crud;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseInMemoryDatabase("DBProduct"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.RouteMethod();
app.Run();


public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Student> Students { get; set; }
}
public record Product(int Id, string Name, string Remarks);
public record Student(int Id, string Name, int Age);
