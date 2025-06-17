using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Application.Features.Customers.Commands
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;
}
