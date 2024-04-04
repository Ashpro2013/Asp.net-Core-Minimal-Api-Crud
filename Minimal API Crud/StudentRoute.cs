using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Minimal_API_Crud
{
    public static class StudentRoute
    {
        public static void StudentMethod(this WebApplication app)
        {
            app.MapGet("/students", async (ProductDbContext db) =>
            {
                return await db.Students.ToListAsync();
            });

            app.MapGet("/students/{id}", async (ProductDbContext db, int id) =>
            {
                return await db.Students.FindAsync(id) is Student student ? Results.Ok(student) : Results.NotFound();
            });

            app.MapPost("/students", async (ProductDbContext db, Student student) =>
            {
                await db.Students.AddAsync(student);
                await db.SaveChangesAsync();

                return Results.Ok(student);
            });

            app.MapPut("/students/{id}", async (ProductDbContext db, int id, Student inputStudent) =>
            {
                try
                {
                    if (await db.Students.AsNoTrackingWithIdentityResolution<Student>().FirstOrDefaultAsync(x => x.Id == id) is Student student)
                    {
                        student = new Student(inputStudent.Id, inputStudent.Name, inputStudent.Age);
                        db.Update(student);
                        await db.SaveChangesAsync();
                        return Results.Ok(student);
                    }
                    else
                    {
                        return Results.NotFound();
                    }
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            });
            app.MapDelete("/students/{id}", async (ProductDbContext db, int id) =>
            {
                if (await db.Students.FindAsync(id) is Student student)
                {
                    db.Students.Remove(student);
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
