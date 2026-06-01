using _27_FrontToBackSqlConnection.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace _27_FrontToBackSqlConnection.Utilities.Conventions;

public class AdminPanelAuthorizationConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        bool isAdminPanelController = controller.Attributes
            .OfType<AreaAttribute>()
            .Any(area => string.Equals(area.RouteValue, "AdminPanel", StringComparison.OrdinalIgnoreCase));

        if (isAdminPanelController)
        {
            controller.Filters.Add(new AuthorizeFilter(PolicyNames.AdminPanelAccess));
        }
    }
}
