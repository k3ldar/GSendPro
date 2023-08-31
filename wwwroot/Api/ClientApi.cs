using System;

using gsend.pro.Internal;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace gsend.pro.Api
{
    public class ClientApi : BaseController
    {
        private readonly ISettingsProvider _settingsProvider;

        public ClientApi(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
        }

        [HttpGet]
        [Route("/ClientApi/ClientMenuItems/{version}/")]
        public IActionResult ClientMenuItems(string version)
        {
            return GenerateJsonSuccessResponse(_settingsProvider.GetSettings<ClientMenuItems>(nameof(ClientMenuItems)));
        }
    }
}
