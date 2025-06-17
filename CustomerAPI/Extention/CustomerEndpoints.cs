using CustomerAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Extention
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", () => "Customer API is running");

            app.MapGet("/api/customers", async (CustomerContext db) =>
                await db.Customers.ToListAsync());

            app.MapGet("/api/customers/{id}", async (Guid id, CustomerContext db) =>
                await db.Customers.FindAsync(id) is Customer customer
                    ? Results.Ok(customer)
                    : Results.NotFound());

            app.MapPost("/api/customers", async (Customer customer, CustomerContext db) =>
            {
                if (await db.Customers.AnyAsync(c => c.Email == customer.Email))
                    return Results.BadRequest("Email already exists.");

                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return Results.Created($"/api/customers/{customer.Id}", customer);
            });

            app.MapPut("/api/customers/{id}", async (Guid id, Customer input, CustomerContext db) =>
            {
                var customer = await db.Customers.FindAsync(id);
                if (customer is null)
                    return Results.NotFound();

                customer.FirstName = input.FirstName;
                customer.MiddleName = input.MiddleName;
                customer.LastName = input.LastName;
                customer.Email = input.Email;
                customer.PhoneNumber = input.PhoneNumber;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/api/customers/{id}", async (Guid id, CustomerContext db) =>
            {
                var customer = await db.Customers.FindAsync(id);
                if (customer is null)
                    return Results.NotFound();

                db.Customers.Remove(customer);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
