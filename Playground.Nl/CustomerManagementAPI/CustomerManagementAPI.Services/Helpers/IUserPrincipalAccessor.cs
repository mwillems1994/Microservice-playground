﻿using System.Security.Claims;

 namespace Playground.Nl.CustomerManagementAPI.Services.Helpers
{
    public interface IUserPrincipalAccessor
    {
        ClaimsPrincipal? User { get; }
    }
}
