using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Entities;
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

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T,bool>>filter=null,int page_size = 2, int page_number = 1, string? includeProperty = null)
        {
           /* if(typeof(T) == typeof(Products))
            {
                var model = await dbcontext.Products.Include(x=>x.Categories).ToListAsync();
                return(IEnumerable<T>)model;
            }


*/
            IQueryable<T> query = dbcontext.Set<T>();
            if(filter!= null)
            {
                query = query.Where(filter);
            }
            if(includeProperty != null)
            {
                foreach(var property in includeProperty.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(includeProperty);
                }
            }
            if(page_size> 0)
            {
                if (page_size > 4)
                {
                    page_size = 4;

                }
                query = query.Skip(page_size * (page_number-1)).Take(page_size);
            }
            return await query.ToListAsync();
           
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
