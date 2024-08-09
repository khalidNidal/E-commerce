using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infastructure.Repositories
{
    public class GenericRepositories<T> : IGenericRepositories<T> where T : class
    {
        private readonly AppDbContext dbcontext;

        public GenericRepositories(AppDbContext dbcontext)  {
            this.dbcontext = dbcontext;
        }

      
        public async Task Create(T model)
        {
           await dbcontext.Set<T>().AddAsync(model);

        }

        public void Delete(int id)
        {
          dbcontext.Remove(id);

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbcontext.Set<T>().ToListAsync();
           
        }

        public async Task<T> GetById(int id)
        {
            return await dbcontext.Set<T>().FindAsync(id);
        }

        public void Update(T model)
        {

            dbcontext.Set<T>().Update(model);


        }
    }
}
