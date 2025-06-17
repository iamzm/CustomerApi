using CustomerAPI.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Application.Features.Customers.Queries
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<Customer?>;
}
