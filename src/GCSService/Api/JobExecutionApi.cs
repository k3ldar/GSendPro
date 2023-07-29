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

        public JobExecutionApi(IGSendDataProvider gSendDataProvider, INotificationService notificationService) 
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider)); ;
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

        //private static bool ValidateJobExecution(long machineId, long toolId, long jobProfileId, out string errorData)
        //{
        //    if (model == null)
        //    {
        //        errorData = "Invalid job execute model";
        //        return false;
        //    }

        //    if (model.JobProfile == null)
        //    {
        //        errorData = "Invalid job profile";
        //        return false;
        //    }

        //    if (model.Machine == null)
        //    {
        //        errorData = "Invalid machine";
        //        return false;
        //    }

        //    if (model.ToolProfile == null)
        //    {
        //        errorData = "Invalid tool profile";
        //        return false;
        //    }

        //    errorData = String.Empty;
        //    return true;
        //}
    }
}
