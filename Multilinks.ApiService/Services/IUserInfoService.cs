﻿using System;

namespace Multilinks.ApiService.Services
{
   public interface IUserInfoService
   {
      Guid UserId { get; }
      string FirstName { get; }
      string LastName { get; }
      string Role { get; }

      /* The following are client details on behalf of the user */
      string ClientId { get; }
      string ClientType { get; }
   }
}
