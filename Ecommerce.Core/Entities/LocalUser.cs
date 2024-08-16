using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ecommerce.Core.Entities
{
    public class LocalUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Addreess { get; set; }
        public ICollection<Orders> ? Orders { get; set; } = new HashSet<Orders>();
        
    }
}
