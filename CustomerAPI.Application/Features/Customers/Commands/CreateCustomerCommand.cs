using CustomerAPI.Domain;
using MediatR;

namespace CustomerAPI.Application.Features.Customers.Commands
{
    public record CreateCustomerCommand(Customer Customer) : IRequest<Customer>;
}
