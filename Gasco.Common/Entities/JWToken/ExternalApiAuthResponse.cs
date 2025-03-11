using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasco.Common.Entities.JWToken
{
    public class ExternalApiAuthResponse
    {
        public string? Token { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
