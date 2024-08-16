using AutoMapper;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Core.IRepositories.IServices;
using Ecommerce.Infastructure.Data;
using Ecommerce.Services;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infastructure.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly ITokenServices tokenServices;
        private readonly SignInManager<LocalUser> signInManager;
        private readonly AppDbContext context;
        private readonly UserManager<LocalUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public UserRepository(ITokenServices tokenServices ,SignInManager<LocalUser> signInManager ,AppDbContext context , UserManager<LocalUser> userManager , RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.tokenServices = tokenServices;
            this.signInManager = signInManager;
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }
        public bool IsUniqueUser(string Email)
        {
            var result = context.localUsers.FirstOrDefault(x=> x.Email == Email);
            return result == null;
                
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
        {

            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            var checkPassword = await signInManager.CheckPasswordSignInAsync(user,loginRequest.Password,false);
            if(!checkPassword.Succeeded)
            {
                return new LoginResponseDTO()
                {
                    Localuser = null,
                    Token = ""
                    
                };


            }

            var role = await userManager.GetRolesAsync(user);

            return new LoginResponseDTO()
            {
                Localuser = mapper.Map<LocalUserDTO>(user),
                Token = await tokenServices.CreateTokenAsync(user),
                Role = role.FirstOrDefault()


            };


        }

        public async Task<LocalUserDTO> Register(RegisterrationRequestDTO registerrationRequest)
        {

            var user = new LocalUser
            {
                UserName = registerrationRequest.Email.Split('@')[0],
                Email = registerrationRequest.Email,
                NormalizedEmail = registerrationRequest.Email,
                FirstName = registerrationRequest.Fname,
                LastName = registerrationRequest.Lname,
                Addreess = registerrationRequest.Address,



            };

            
            


            using(var transaction = await context.Database.BeginTransactionAsync()) 
            {

                try
                {
                    var result = await userManager.CreateAsync(user, registerrationRequest.Password);
                    if (result.Succeeded)
                    {

                        var role = await roleManager.RoleExistsAsync(registerrationRequest.Role);
                        if (!role)
                        {
                            throw new Exception($" the role {registerrationRequest.Role} is not exist. ! ");

                        }
                        /* var roles = await roleManager.Roles.ToListAsync();
                         foreach (var role in roles)
                          {
                              if (await roleManager.RoleExistsAsync(role.Name))
                              {
                                  await roleManager.CreateAsync(new IdentityRole(role.Name));
                              }

                          }*/

                        var userRoleResult = await userManager.AddToRoleAsync(user, registerrationRequest.Role);
                        if (userRoleResult.Succeeded)
                        {
                            await transaction.CommitAsync();
                            var userReturn = context.localUsers.FirstOrDefault(u => u.Email == registerrationRequest.Email);
                            return mapper.Map<LocalUserDTO>(userRoleResult);

                        }
                        else
                        {
                            await transaction.RollbackAsync( );
                            throw new Exception(" Failed to add user to UserRole ");

                        }


                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        throw new Exception(" user Registeration faild ");
                    }
                }

                catch (Exception ex)
                {
                    throw;


                }

            }
            



        }
    }
}
