using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities.DTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public LocalUserDTO Localuser { get; set; }
        public string Role { get; set; }
    }
}
