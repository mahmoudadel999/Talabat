﻿namespace Talabat.Shared.Models
{
    public class StripeSettings
    {
        public required string SecretKey { get; set; }
        public required string WebHookSecret { get; set; }
    }
}
