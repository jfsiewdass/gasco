using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sare.Common.Entities.JWToken
{
    public record RefreshToken (string Token, DateTime ExpirationDate);
}
