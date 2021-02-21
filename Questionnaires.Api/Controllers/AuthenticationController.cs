using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Questionnaires.Api.AuthenticationMock;

namespace Questionnaires.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authMock;

        public AuthenticationController(IAuthenticationService authMock)
        {
            _authMock = authMock;
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public TokenResponse RequestToken([FromBody] GetTokenRequest request)
        {
            return _authMock.Authenticate(request);
        }
    }
}