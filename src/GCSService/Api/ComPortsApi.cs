using System;

using GSendShared.Abstractions;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

namespace GSendService.Api
{
    public sealed class ComPortsApi : BaseController
    {
        private readonly IComPortProvider _comPortProvider;
        internal readonly static Timings _comPortTimings = new();

        public ComPortsApi(IComPortProvider comPortProvider)
        {
            _comPortProvider = comPortProvider ?? throw new ArgumentNullException(nameof(comPortProvider));
        }

        [HttpGet]
        [ApiAuthorization]
        public IActionResult GetAllPorts()
        {
            using (StopWatchTimer swt = StopWatchTimer.Initialise(_comPortTimings))
            {
                return GenerateJsonSuccessResponse(_comPortProvider.AvailablePorts());
            }
        }
    }
}
