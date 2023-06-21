using GSendShared.Abstractions;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Api
{
    public sealed class ComPortsApi : BaseController
    {
        private readonly IComPortProvider _comPortProvider;

        public ComPortsApi(IComPortProvider comPortProvider)
        {
            _comPortProvider = comPortProvider ?? throw new ArgumentNullException(nameof(comPortProvider));
        }

        [HttpGet]
        [ApiAuthorization]
        public IActionResult GetAllPorts()
        {
            return GenerateJsonSuccessResponse(_comPortProvider.AvailablePorts());
        }
    }
}
