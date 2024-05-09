using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.Authentication
{
    public record JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string Issuer { get; init; } = null;
        public string Audience {  get; init; } = null;
        public int ExpiryMinutes {  get; init; }
        public string Secret {  get; init; } = "super-secret-key";
    }
}
