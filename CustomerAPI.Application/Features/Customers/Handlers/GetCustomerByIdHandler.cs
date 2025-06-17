using CustomerAPI.Application.Features.Customers.Queries;
using CustomerAPI.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Application.Features.Customers.Handlers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
    {
        private readonly CustomerContext _context;

        public GetCustomerByIdHandler(CustomerContext context)
        {
            _context = context;
        }

        public async Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
