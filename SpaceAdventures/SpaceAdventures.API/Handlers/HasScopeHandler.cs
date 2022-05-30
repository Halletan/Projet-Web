using Microsoft.AspNetCore.Authorization;

namespace SpaceAdventures.API.Handlers;

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
    {
        var issuer = context.User;
        var scope = context.User.Claims;

        // If user current user does not have a scope claim => so no permission
        if (!context.User
                .HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
            return Task.CompletedTask;

        // Otherwise we split the scopes string into an array
        var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer).Value.Split(' ');
        var permissions = context.User.FindAll(c => c.Type == "permissions");
        var result = string.Join(",", permissions);


        // If the scope array contains the required scope => access is granted
        if (result.Contains(requirement.Scope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}