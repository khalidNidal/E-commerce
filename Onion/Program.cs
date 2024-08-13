using Ecommerce.API.mapping_profiles;
using Ecommerce.Core.Entities;
using Ecommerce.Core.IRepositories;
using Ecommerce.Infastructure.Dbcontext;
using Ecommerce.Infastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Onion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(op=>
            {

                op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddScoped(typeof(IProductRepositories),typeof(ProductRepositories));
                builder.Services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepositories<>));
                builder.Services.AddScoped(typeof(IUnitOfWorks<>),typeof(UnitOfWork<>));
                builder.Services.AddAutoMapper(typeof(MappingProfile));
                builder.Services.Configure<ApiBehaviorOptions>(op =>
                    op.InvalidModelStateResponseFactory=(actionContext)=>
                    {
                        var errors = actionContext.ModelState.Where(x=>x.Value.Errors.Count()>0)
                                                             .SelectMany(x=>x.Value.Errors)
                                                             .Select(x=>x.ErrorMessage)
                                                             .ToList();
                        var validation = new ApiValidationResponse(stausCode:400) { Errors = errors};
                        return new BadRequestObjectResult(validation);

                    }


                );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}