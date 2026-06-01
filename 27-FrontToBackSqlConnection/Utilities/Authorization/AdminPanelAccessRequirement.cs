using _27_FrontToBackSqlConnection.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _27_FrontToBackSqlConnection.Utilities.Authorization;

public class AdminPanelAccessRequirement : IAuthorizationRequirement
{
}

public class AdminPanelAccessHandler : AuthorizationHandler<AdminPanelAccessRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdminPanelAccessRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated != true)
        {
            return Task.CompletedTask;
        }

        if (context.User.IsInRole(AppRoles.Admin))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        string? actionName = GetActionName(context.Resource);

        if (string.Equals(actionName, "Index", StringComparison.OrdinalIgnoreCase)
            && (context.User.IsInRole(AppRoles.Moderator) || context.User.IsInRole(AppRoles.Member)))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (actionName is not null && !IsDeleteAction(actionName) && context.User.IsInRole(AppRoles.Moderator))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }

    private static string? GetActionName(object? resource)
    {
        if (resource is AuthorizationFilterContext authorizationContext
            && authorizationContext.RouteData.Values.TryGetValue("action", out object? action))
        {
            return Convert.ToString(action);
        }

        return null;
    }

    private static bool IsDeleteAction(string? actionName)
    {
        return actionName?.Contains("Delete", StringComparison.OrdinalIgnoreCase) == true;
    }
}
