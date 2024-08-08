using Ecommerce.Core.Entities;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infastructure.Dbcontext;
using Ecommerce.Infastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWorks<Products> unitOfWorks;

        /*        private readonly IProductRepositories productRepositories;
*//*        private readonly IGenericRepositories<Products> genericRepositories;
*/
        public ProductController( /*IGenericRepositories<Products> genericRepositories*/ /*IProductRepositories productRepositories*/ IUnitOfWorks<Products> unitOfWorks)
        {
            this.unitOfWorks = unitOfWorks;
            this.unitOfWorks = unitOfWorks;
        }

        [HttpGet]
        public ActionResult GetAll() {
            var model = unitOfWorks.ProductsRepository.GetAll();

            return Ok(model);        
        
        }
        [HttpGet]
        public ActionResult Get(int id)
        {
            var model = unitOfWorks.ProductsRepository.GetById(id);
            return Ok(model);

        }
        [HttpPut]
        public ActionResult UpdateProduct (Products products)
        {
            unitOfWorks.ProductsRepository.Update(products);
            unitOfWorks.save();

            return Ok(products);
        }

        [HttpPost]
        public ActionResult CreateProduct(Products products)
        {
            unitOfWorks.ProductsRepository.Create(products);
            unitOfWorks.save();

            return Ok(products);
        }
        [HttpDelete]
        public ActionResult DeleteProduct(int id)
        {
            unitOfWorks.ProductsRepository.Delete(id);
            unitOfWorks.save();

            return Ok();
        }




    }
}
