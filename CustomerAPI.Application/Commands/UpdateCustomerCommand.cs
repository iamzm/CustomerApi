using CustomerAPI.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI.Application.Commands
{
    public record UpdateCustomerCommand(Customer Customer) : IRequest<bool>;
}
