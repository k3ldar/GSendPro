using System;

using GSendShared;
using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

using static SharedPluginFeatures.Constants;

namespace GSendService.Api
{
    public class JobExecutionApi : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly INotificationService _notificationService;
        internal readonly static Timings _jobExecutionCreate = new();
        internal readonly static Timings _jobExecutionToolTime = new();

        public JobExecutionApi(IGSendDataProvider gSendDataProvider, INotificationService notificationService) 
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        [HttpPost]
        [ApiAuthorization]
        [Route("/JobExecuteApi/Create/{machineId}/{toolId}/{jobProfileId}/")]
        public IActionResult CreateJobExecution(long machineId, long toolId, long jobProfileId)
        {
            using (StopWatchTimer swt = StopWatchTimer.Initialise(_jobExecutionCreate))
            {
                IJobExecution model = _gSendDataProvider.JobExecutionCreate(machineId, toolId, jobProfileId);

                if (model == null)
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Unable to create job execution");

                _notificationService.RaiseEvent(GSendShared.Constants.NotificationJobExecutionAdd, model.Id);

                return GenerateJsonSuccessResponse(model);
            }
        }

        [HttpGet]
        [ApiAuthorization]
        [Route("/JobExecuteApi/ToolHours/{toolId}/")]
        public IActionResult JobExecutionTooltime(long toolId)
        {
            using (StopWatchTimer swt = StopWatchTimer.Initialise(_jobExecutionToolTime))
            {
                IToolProfile toolProfile = _gSendDataProvider.ToolGet(toolId);

                if (toolProfile == null)
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Unable to evaulutate job execution time");

                TimeSpan totalTime = _gSendDataProvider.JobExecutionByTool(toolProfile);

                return GenerateJsonSuccessResponse(totalTime);
            }
        }
    }
}
