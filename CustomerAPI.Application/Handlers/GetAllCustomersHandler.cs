using CustomerAPI.Application.Queries;
using CustomerAPI.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace CustomerAPI.Application.Handlers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
    {
        private readonly CustomerContext _context;

        public GetAllCustomersHandler(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.ToListAsync(cancellationToken);
        }
    }
}
