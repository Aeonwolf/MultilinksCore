﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Multilinks.TokenService
{
   public class Config
   {
      // scopes define the resources in your system
      public static IEnumerable<IdentityResource> GetIdentityResources()
      {
         return new List<IdentityResource>
         {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
         };
      }

      public static IEnumerable<ApiResource> GetApiResources()
      {
         return new List<ApiResource>
         {
            new ApiResource("api1", "My API")
         };
      }

      // clients want to access resources (aka scopes)
      public static IEnumerable<Client> GetClients()
      {
         // client credentials client
         return new List<Client>
         {
         };
      }
   }
}