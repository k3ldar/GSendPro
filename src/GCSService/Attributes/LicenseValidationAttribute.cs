using System;
using System.Threading.Tasks;

using GSendShared.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GSendService.Attributes
{
    public class LicenseValidationAttribute : ActionFilterAttribute
    {
        public async override void OnActionExecuting(ActionExecutingContext context)
        {
#if __LICENSED__
            ILicenseFactory licenseFactory = context.HttpContext.RequestServices.GetRequiredService<ILicenseFactory>();
            ILicense license = licenseFactory.GetActiveLicense();

            if (license == null || !license.IsValid)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary
                {
                    { "action", "ViewLicense" },
                    { "controller", "Home" },
                    { "area", "" }
                };

                context.Result = new RedirectToRouteResult(redirectTargetDictionary);
                await context.Result.ExecuteResultAsync(context);
                return;
            }
#else
            await Task.Delay(0);
#endif

            base.OnActionExecuting(context);
        }
    }
}
