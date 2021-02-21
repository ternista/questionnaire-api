namespace Questionnaires.Api.AuthenticationMock
{
    public interface IAuthenticationService
    {
        TokenResponse Authenticate(GetTokenRequest request);
    }
}