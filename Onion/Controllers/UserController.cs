using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private List<string> errors = new List<string>() { "the email is already exist" };
        
        private readonly IUsersRepository usersRepository;

        public UserController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        [HttpPost ("login") ]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await usersRepository.Login(loginRequestDTO);
                if (user.Localuser == null)
                {
                    return Unauthorized(new ApiValidationResponse(new List <string> { "email or password is uncorrect" } , 401));


                }

                return Ok(user);

            }

            return BadRequest(new ApiValidationResponse(new List<string> { "please try to enter the email and password coorectly" }, 401));

        }




        [HttpPost ("register")]
        public async Task<IActionResult> Register([FromBody]RegisterrationRequestDTO model)
        {
            try
            {
                bool uniqueEmail = usersRepository.IsUniqueUser(model.Email);
                if (!uniqueEmail)
                {
                    return BadRequest(new ApiValidationResponse(errors, 400));

                }

                var user = usersRepository.Register(model);


                if (user == null)
                {
                    return BadRequest(new ApiValidationResponse(new List<String> { "error while reg" }, 400));
                }

                else
                {
                    return Ok(new ApiResponse(201, result:user));
                }
            }
            catch(Exception ex) 
            {

                return StatusCode(500, new ApiValidationResponse(new List <string>() { ex.Message , "an error while proccing  youre request"}));
            
            }


        }
    }
}
