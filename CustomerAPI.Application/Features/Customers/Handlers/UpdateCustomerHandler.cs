﻿using CustomerAPI.Application.Features.Customers.Commands;
using CustomerAPI.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Application.Features.Customers.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly CustomerContext _context;

        public UpdateCustomerHandler(CustomerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _context.Customers.FindAsync(new object[] { request.Customer.Id }, cancellationToken);

            if (existingCustomer == null)
                return false;

            // Update fields
            existingCustomer.FirstName = request.Customer.FirstName;
            existingCustomer.MiddleName = request.Customer.MiddleName;
            existingCustomer.LastName = request.Customer.LastName;
            existingCustomer.Email = request.Customer.Email;
            existingCustomer.PhoneNumber = request.Customer.PhoneNumber;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
