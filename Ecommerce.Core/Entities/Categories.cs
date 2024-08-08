using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public ICollection<Products> Products { get; set; } = new HashSet<Products>();  

    }
}
