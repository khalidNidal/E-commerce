using Ecommerce.Core.Entities;
using Ecommerce.Infastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.IRepositories;

namespace Ecommerce.Infastructure.Repositories
{
    public class ProductRepositories : GenericRepositories<Products>,IProductRepositories 
    {
        private readonly AppDbContext dbContext;

        public ProductRepositories(AppDbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Products>> GetAllProductsByCategoryId(int Cat_Id)
        {
            var products = await dbContext.Products.Include(x=>x.Categories)
                .Where(c=>c.Category_Id == Cat_Id).ToListAsync();
            return products;
        }
    }
}
