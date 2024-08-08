using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infastructure.Dbcontext;

namespace Ecommerce.Infastructure.Repositories
{
    public class GenericRepositories<T> : IGenericRepositories<T> where T : class
    {
        private readonly AppDbContext dbcontext;

        public GenericRepositories(AppDbContext dbcontext)  {
            this.dbcontext = dbcontext;
        }

      
        public void Create(T model)
        {
            dbcontext.Set<T>().Add(model);

        }

        public void Delete(int id)
        {
          dbcontext.Remove(id);

        }

        public IEnumerable<T> GetAll()
        {
            return dbcontext.Set<T>().ToList();
           
        }

        public T GetById(int id)
        {
            return dbcontext.Set<T>().Find(id);
        }

        public void Update(T model)
        {

            dbcontext.Set<T>().Update(model);


        }
    }
}
