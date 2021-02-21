using System;

namespace Questionnaires.Api.AuthenticationMock
{
    public class TokenResponse
    {
        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}