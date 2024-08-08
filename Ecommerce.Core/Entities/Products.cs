using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ? Image { get; set; }

        [ForeignKey(nameof(Categories))]
        public int Category_Id { get; set; }
        public Categories ? Categories { get; set; }
        public ICollection<OrderDetails> OrderDetailes { get; set; } = new HashSet<OrderDetails>();
    }
}
