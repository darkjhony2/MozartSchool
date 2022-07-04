﻿using ColegioMozart.Application.Common.Models;

namespace ColegioMozart.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password, string role);

    Task<Result> DeleteUserAsync(string userId);

    Task<IList<string>> GetUserRoles(string userId);

    Task<(bool, string)> VerifyCredentialsAsync(string userName, string password);
}
