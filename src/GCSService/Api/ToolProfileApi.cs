using System;

using GSendShared;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace GSendService.Api
{
    public class ToolProfileApi : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;

        public ToolProfileApi(IGSendDataProvider gSendDataProvider)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
        }

        [HttpGet]
        [ApiAuthorization]
        public IActionResult ToolsGet()
        {
            return GenerateJsonSuccessResponse(_gSendDataProvider.ToolsGet());
        }
    }
}
