﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Multilinks.TokenService
{
   public class Config
   {
      public static IEnumerable<IdentityResource> GetIdentityResources()
      {
         return new List<IdentityResource>
         {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "User role(s)", new List<string> { JwtClaimTypes.Role })
         };
      }

      public static IEnumerable<ApiResource> GetApiResources()
      {
         return new List<ApiResource>
         {
            new ApiResource("ApiService", "Multilinks API Service", new List<string> { JwtClaimTypes.Role })
         };
      }

      // clients want to access resources (aka scopes)
      public static IEnumerable<Client> GetClients()
      {
         return new List<Client>
         {
            new Client
            {
               ClientId = "WebConsole",
               ClientName = "Multilinks Web Console",
               AllowAccessTokensViaBrowser = true,
               AllowedGrantTypes = GrantTypes.Implicit,
               RequireConsent = false,
               AccessTokenLifetime = 1800,
               AllowedCorsOrigins = { "https://localhost:44302" },

               RedirectUris =
               {
                  "https://localhost:44302/signin-oidc",
                  "https://localhost:44302/redirect-silent-renew"
               },

               PostLogoutRedirectUris =
               {
                  "https://localhost:44302/signout-oidc"
               },
 
               // scopes that client has access to
               AllowedScopes =
               {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  "roles",
                  "ApiService"
               },

               /* Custom claims to include with access token for this client. */
               Claims = new List<Claim>
               {
                  new Claim("Type", "SPA_CLIENT")
               },
               AlwaysSendClientClaims = true
            }
         };
      }
   }
}
