using AutoMapper;
using Ecommerce.API.Controllers;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Entities.DTO;

namespace Ecommerce.API.mapping_profiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Products, ProductDTO>()
                .ForMember(To => To.Category_Name, from => from.MapFrom(x => x.Categories != null ? x.Categories.Name : null));
            CreateMap<LocalUser , LocalUserDTO>().ReverseMap();
        }


    }
}
