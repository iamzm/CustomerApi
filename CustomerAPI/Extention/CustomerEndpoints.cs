using CustomerAPI.Application.Commands;
using CustomerAPI.Application.Queries;
using CustomerAPI.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Extention
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/customers", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCustomersQuery());
                return Results.Ok(result);
            });

            // GET customer by ID
            app.MapGet("/api/customers/{id}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetCustomerByIdQuery(id));
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            // POST create customer
            app.MapPost("/api/customers", async (Customer customer, IMediator mediator) =>
            {
                try
                {
                    customer.Id = Guid.NewGuid();
                    var result = await mediator.Send(new CreateCustomerCommand(customer));
                    return Results.Created($"/api/customers/{result.Id}", result);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            // PUT update customer
            app.MapPut("/api/customers/{id}", async (Guid id, Customer updatedCustomer, IMediator mediator) =>
            {
                try
                {
                    updatedCustomer.Id = id;
                    var result = await mediator.Send(new UpdateCustomerCommand(updatedCustomer));
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            // DELETE customer
            app.MapDelete("/api/customers/{id}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteCustomerCommand(id));
                return result ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
