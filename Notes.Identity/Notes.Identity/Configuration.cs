using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Identity
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes()
        {
            yield return new ApiScope("NotesWebAPI", "Web API");
        }

        public static IEnumerable<IdentityResource> IdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
        }

        public static IEnumerable<ApiResource> ApiResources()
        {
            yield return new ApiResource("NotesWebAPI");
        }

        public static IEnumerable<Client> Clients()=>
            new List<Client>
            {
                new Client
            {
                ClientId = "client_id",
                ClientSecrets = { new Secret("client_secret".ToSha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                //AlwaysSendClientClaims = true, 
                //AlwaysIncludeUserClaimsInIdToken = true, 
                AllowedScopes =
                {
                    "NotesWebAPI",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            },
            };
    }
}
