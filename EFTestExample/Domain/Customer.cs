using EFTestExample.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTestExample.Domain
{
    public class Customer : IEntityWithKey
    {
        public int Id { get; set; }

        public string Email { get; set; }
    }
}
