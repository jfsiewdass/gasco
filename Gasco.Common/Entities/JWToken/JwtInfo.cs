using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gasco.Common.Entities.JWToken
{
    public class JwtInfo
    {
        public string? ValidAudience { get; set; }
        public string? ValidIssuer { get; set; }
        public string? SecretKey { get; set; }
        public int Duration { get; set; }
        public int Refresh { get; set; }
        public int DurationExternalApi { get; set; }
        public int RefreshExternalApi { get; set; }
    }
}
