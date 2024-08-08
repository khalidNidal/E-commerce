using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.IRepositories
{
    public interface IUnitOfWorks<T> where T : class
    {
        public IProductRepositories ProductsRepository { get; set; }
        public ICategoryRepositories CategoryRepositories { get; set; }
        public int save();

    }
}
