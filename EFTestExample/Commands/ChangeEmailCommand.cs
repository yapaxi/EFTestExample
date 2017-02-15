using EFTestExample.DAL;
using EFTestExample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTestExample.Commands
{
    public class ChangeEmailCommand
    {
        private readonly ISomeBoundedRepository _repository;

        public ChangeEmailCommand(ISomeBoundedRepository repository)
        {
            _repository = repository;
        }

        public void Change(int customerId, string email)
        {
            var customers = _repository.Entity<Customer>();

            var customer = customers.Get(customerId);

            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            var sameEmailCustomer = customers.Get(e => e.Email == email);

            if (sameEmailCustomer != null && sameEmailCustomer.Id != customerId)
            {
                throw new Exception("Email cannot be used");
            }
            
            customer.Email = email;

            _repository.SaveChanges();
        }
    }
}
