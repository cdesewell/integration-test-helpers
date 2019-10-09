using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using IntegrationTestHelpers.Models;

namespace IntegrationTestHelpers.Clients
{
    public class HttpIdentityClient
    {
        public static async Task<TokenResponse> GetToken(Credentials credentials, Uri identityUri)
        {
            // Create auth credentials
            var tokenClient = new TokenClient(
                identityUri.ToString(),
                credentials.ClientId,
                credentials.ClientSecret);

            // Get token
            var tokenResponse = await tokenClient
                .RequestResourceOwnerPasswordAsync(credentials.Username,
                    credentials.Password,
                    credentials.Scope,
                    new Dictionary<string, string> { { credentials.Key, credentials.Name } });

            return tokenResponse;
        }
    }
}
}
