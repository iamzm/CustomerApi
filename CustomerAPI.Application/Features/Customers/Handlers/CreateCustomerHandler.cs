using CustomerAPI.Application.Features.Customers.Commands;
using CustomerAPI.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Application.Features.Customers.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly CustomerContext _context;

        public CreateCustomerHandler(CustomerContext context)
        {
            _context = context;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Customers.AnyAsync(c => c.Email == request.Customer.Email))
                throw new Exception("Email already exists.");

            _context.Customers.Add(request.Customer);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Customer;
        }
    }
}
