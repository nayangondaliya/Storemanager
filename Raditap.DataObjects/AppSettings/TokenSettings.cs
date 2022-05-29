﻿namespace Raditap.DataObjects.AppSettings
{
    public class TokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
