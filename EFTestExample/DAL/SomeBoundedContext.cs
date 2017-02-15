using EFTestExample.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTestExample.DAL
{
    public class SomeBoundedContext : DbContext
    {
        public IDbSet<Customer> Customers { get; set; }
    }

    public class SomeBoundedRepository : BasicRepository<SomeBoundedContext>, ISomeBoundedRepository
    {
        public SomeBoundedRepository(SomeBoundedContext context) : base(context)
        {

        }
    }

    public interface ISomeBoundedRepository : IRepository
    {

    }
}
