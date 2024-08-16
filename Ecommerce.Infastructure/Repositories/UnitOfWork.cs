using Ecommerce.Core.IRepositories;
using Ecommerce.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infastructure.Repositories
{
    public class UnitOfWork<T> : IUnitOfWorks<T> where T : class
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            ProductsRepository = new ProductRepositories(dbContext);
         }
        public IProductRepositories ProductsRepository { get; set; }
        public ICategoryRepositories CategoryRepositories { get; set ; }

        public async Task<int> save() => await dbContext.SaveChangesAsync();
        
    }
}
