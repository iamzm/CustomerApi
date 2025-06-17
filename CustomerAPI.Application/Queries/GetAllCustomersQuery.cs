using CustomerAPI.Domain;
using MediatR;
using System.Collections.Generic;

namespace CustomerAPI.Application.Queries
{
    public record GetAllCustomersQuery() : IRequest<IEnumerable<Customer>>;
}
