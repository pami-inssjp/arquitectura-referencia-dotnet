using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Pami.DotNet.ReferenceArchitecture.Servicios.Common.Security.Utils
{
    public class CustomOAuthBearerProvider : IOAuthBearerAuthenticationProvider
    {
        public Task ApplyChallenge(OAuthChallengeContext context)
        {
            return Task.FromResult<object>(null);
        }

        public Task RequestToken(OAuthRequestTokenContext context)
        {
            string token = context.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.FromResult<object>(null);
        }
        public Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            return Task.FromResult<object>(null);
        }
    }
}