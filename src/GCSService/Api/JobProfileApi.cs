using System;
using System.Linq;

using GSendShared;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

using static SharedPluginFeatures.Constants;

namespace GSendService.Api
{
    public sealed class JobProfileApi : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly INotificationService _notificationService;
        internal readonly static Timings _jobProfilesGet = new();
        internal readonly static Timings _jobProfileAdd = new();
        internal readonly static Timings _jobProfileDelete = new();
        internal readonly static Timings _jobProfileUpdate = new();

        public JobProfileApi(IGSendDataProvider gSendDataProvider, INotificationService notificationService)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        [HttpGet]
        [ApiAuthorization]
        public IActionResult JobProfilesGet()
        {
            using (StopWatchTimer swt = StopWatchTimer.Initialise(_jobProfilesGet))
            {
                return GenerateJsonSuccessResponse(_gSendDataProvider.JobProfilesGet());
            }
        }

        [HttpPost]
        [ApiAuthorization]
        public IActionResult JobProfileAdd([FromBody] IJobProfile model)
        {
            using (StopWatchTimer swt = StopWatchTimer.Initialise(_jobProfileAdd))
            {
                if (!ValidateJobProfile(model, out string errorData))
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, errorData);

                if (_gSendDataProvider.JobProfilesGet().Any(jp => jp.Name.Equals(model.Name)))
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, "duplicate name");

                _gSendDataProvider.JobProfileAdd(model.Name, model.Description);

                if (_gSendDataProvider.JobProfilesGet().FirstOrDefault(j => j.Name.Equals(model.Name)) == null)
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Error adding job profile");

                _notificationService.RaiseEvent(GSendShared.Constants.NotificationJobProfileAdd, model.Id);

                return GenerateJsonSuccessResponse();
            }
        }

        [HttpDelete]
        [ApiAuthorization]
        public IActionResult JobProfileDelete(long jobProfileId)
        {
            using (StopWatchTimer swt = StopWatchTimer.Initialise(_jobProfileDelete))
            {
                IJobProfile deleteJobProfile = _gSendDataProvider.JobProfileGet(jobProfileId);

                if (deleteJobProfile == null)
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Profile not found");

                if (_gSendDataProvider.JobProfilesGet().Count == 1)
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Profile not removed");

                _gSendDataProvider.JobProfileRemove(deleteJobProfile.Id);

                if (_gSendDataProvider.JobProfilesGet().FirstOrDefault(m => m.Id.Equals(jobProfileId)) != null)
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Error removing job profile");

                _notificationService.RaiseEvent(GSendShared.Constants.NotificationJobProfileRemove, deleteJobProfile.Id);

                return GenerateJsonSuccessResponse();
            }
        }

        [HttpPut]
        [ApiAuthorization]
        public IActionResult JobProfileUpdate([FromBody] IJobProfile model)
        {
            using (StopWatchTimer swt = StopWatchTimer.Initialise(_jobProfileUpdate))
            {
                if (!ValidateJobProfile(model, out string errorData))
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, errorData);

                IJobProfile updateJobProfile = _gSendDataProvider.JobProfileGet(model.Id);

                if (updateJobProfile == null)
                    return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Profile not found");

                updateJobProfile.Name = model.Name;
                updateJobProfile.Description = model.Description;

                _gSendDataProvider.JobProfileUpdate(updateJobProfile);

                _notificationService.RaiseEvent(GSendShared.Constants.NotificationJobProfileUpdated, updateJobProfile.Id);

                return GenerateJsonSuccessResponse();
            }
        }

        private static bool ValidateJobProfile(IJobProfile jobProfileModel, out string errorData)
        {
            if (jobProfileModel == null)
            {
                errorData = "Invalid job profile model";
                return false;
            }

            if (String.IsNullOrEmpty(jobProfileModel.Name))
            {
                errorData = "Invalid job profile name";
                return false;
            }

            if (String.IsNullOrEmpty(jobProfileModel.Description))
            {
                errorData = "Invalid job profile description";
                return false;
            }

            errorData = String.Empty;
            return true;
        }
    }
}
