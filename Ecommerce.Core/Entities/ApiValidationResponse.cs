using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class ApiValidationResponse : ApiResponse
    {
        public IEnumerable<string> Errors{ get; set; }
        public ApiValidationResponse(IEnumerable<string> errors = null , int? stausCode = null):base(stausCode)
        {
            Errors = errors ?? new List<string>();
        }
    }

    
}

