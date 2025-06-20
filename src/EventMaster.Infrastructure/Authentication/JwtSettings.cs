﻿namespace EventMaster.Infrastructure.Authentication;

internal class JwtSettings
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public int AccessTokenExpirationInMinutes { get; set; }
    public int RefreshTokenExpirationInDays { get; set; }
}