using System;

namespace yukumyolda.Infrastructure.Tokens;

public class TokenSettings
{
    public string Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Secret { get; set; } = default!;
    public int TokenValidityInMinutes { get; set; }
}
