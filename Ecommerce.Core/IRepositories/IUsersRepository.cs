using Ecommerce.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.IRepositories
{
    public interface IUsersRepository
    {
        Task<LoginResponseDTO> Login (LoginRequestDTO loginRequest);
        Task<LocalUserDTO> Register (RegisterrationRequestDTO registerrationRequest);
        bool IsUniqueUser (string Email);
    }
}
