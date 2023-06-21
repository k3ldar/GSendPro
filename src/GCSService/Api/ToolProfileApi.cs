using GSendShared;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace GSendService.Api
{
    public class ToolProfileApi : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly INotificationService _notificationService;

        public ToolProfileApi(IGSendDataProvider gSendDataProvider, INotificationService notificationService)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        [HttpGet]
        [ApiAuthorization]
        public IActionResult ToolsGet()
        {
            return GenerateJsonSuccessResponse(_gSendDataProvider.ToolsGet());
        }
    }
}
