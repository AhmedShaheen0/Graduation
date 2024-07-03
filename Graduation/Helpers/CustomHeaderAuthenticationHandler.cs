    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

namespace Graduation.Helpers
{
    public class CustomHeaderAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public CustomHeaderAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("X-Custom-Token"))
            {
                return AuthenticateResult.Fail("Custom token not found in the request header.");
            }

            var customToken = Request.Headers["X-Custom-Token"].ToString();

            // Validate the token (e.g., verify signature, check expiration, etc.)
            // You can use your existing token validation logic here.

            
            // For now, let's assume the token is valid.
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "username"),
        };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
        }
    }
}
