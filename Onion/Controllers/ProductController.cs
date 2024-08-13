using AutoMapper;
using Ecommerce.API.mapping_profiles;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infastructure.Dbcontext;
using Ecommerce.Infastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWorks<Products> unitOfWorks;
        public ApiResponse ApiResponse { get; set; }
        public IMapper mapper { get; set; }

        private readonly IProductRepositories productRepositories;
        /*        private readonly IGenericRepositories<Products> genericRepositories;
        */
        public ProductController(IMapper mapper,/*IGenericRepositories<Products> genericRepositories*/ /*IProductRepositories productRepositories*/  IUnitOfWorks<Products> unitOfWorks)
        {
            this.mapper = mapper;
            this.unitOfWorks = unitOfWorks;
            this.ApiResponse = new ApiResponse();
            
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllProducts([FromQuery] string ? catName ,int pageSize = 2, int pageNumber= 1) {
            Expression<Func<Products, bool>> Filter = null;
            if(!string.IsNullOrWhiteSpace(catName))
            {
                Filter = x=>x.Categories.Name.Contains(catName);
            }
            var model = await unitOfWorks.ProductsRepository.GetAll(page_size : pageSize , page_number:pageNumber
                , includeProperty:"Categories",filter:Filter);
            var check = model.Any();
           
                if (check)
                {
                    ApiResponse.StatusCode = 200;
                    ApiResponse.IsSuccess = check;
                    var mappedproducts = mapper.Map<IEnumerable<Products>, IEnumerable<ProductDTO>>(model);
                    ApiResponse.Result = mappedproducts;
                    return ApiResponse;



                }
                else
                {
                    ApiResponse.Message = "not product found";
                    ApiResponse.StatusCode = 200;
                    ApiResponse.IsSuccess = false;
                    return ApiResponse;

                }
            

            

            return Ok(model);        
        
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get([FromQuery]int id)
        {
            try
            {

                if(id <= 0)
                {
                    return BadRequest(new ApiValidationResponse(new List<string> {"try positive int, grater than zero" ,"Invalid number" } , 400));
                }
                var model = await unitOfWorks.ProductsRepository.GetById(id);
                if(model == null)
                {
                    var x = model.ToString();
                    return NotFound(new ApiValidationResponse(new List<string> { "Product not found" }, 404) );
                }
                return Ok(new ApiResponse(200, result: model));


            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiValidationResponse(new List<string> { "internal server error ", ex.Message }, StatusCodes.Status500InternalServerError));
            }


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


        [HttpGet ("Product/{Cat_Id}") ]
        public async Task<ActionResult> GetProductByCatId(int Cat_Id)
        {
            var product = await unitOfWorks.ProductsRepository.GetAllProductsByCategoryId(Cat_Id);
            var mapperproduct = mapper.Map<IEnumerable<Products>, IEnumerable<ProductDTO>>(product);
            return Ok(mapperproduct);

        }

    }
}
