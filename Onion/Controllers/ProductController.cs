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
        public ApiResponse ApiResponse { get; set; }

        private readonly IProductRepositories productRepositories;
        /*        private readonly IGenericRepositories<Products> genericRepositories;
        */
        public ProductController( /*IGenericRepositories<Products> genericRepositories*/ /*IProductRepositories productRepositories*/ IUnitOfWorks<Products> unitOfWorks)
        {
            this.unitOfWorks = unitOfWorks;
            this.ApiResponse = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAll() {
            var model = await unitOfWorks.ProductsRepository.GetAll();
            var check = model.Any();
            if(check)
            {
                ApiResponse.StatusCode = System.Net.HttpStatusCode.OK;
                ApiResponse.IsSuccess = check;
                ApiResponse.Result = model;
                return ApiResponse;



            }
            else
            {
                ApiResponse.ErrorMessages = "not product found";
                ApiResponse.StatusCode=System.Net.HttpStatusCode.OK;
                return ApiResponse;

            }

            return Ok(model);        
        
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var model = await unitOfWorks.ProductsRepository.GetById(id);
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
        public async Task <ActionResult> CreateProduct(Products products)
        {
            await unitOfWorks.ProductsRepository.Create(products);
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
