﻿using System.Security.Claims;

 namespace Playground.Nl.OrderManagementAPI.Nl.Helpers
{
    public interface IUserPrincipalAccessor
    {
        ClaimsPrincipal? User { get; }
    }
}
