using CustomerAPI.Domain;
using MediatR;

namespace CustomerAPI.Application.Commands
{
    public record CreateCustomerCommand(Customer Customer) : IRequest<Customer>;
}
