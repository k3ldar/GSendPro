using System.Reflection;

using GSendShared;
using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

using static SharedPluginFeatures.Constants;

namespace GSendService.Api
{
    public class JobProfileApi : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly INotificationService _notificationService;

        public JobProfileApi(IGSendDataProvider gSendDataProvider, INotificationService notificationService)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        [HttpGet]
        public IActionResult JobProfilesGet()
        {
            return GenerateJsonSuccessResponse(_gSendDataProvider.JobProfilesGet());
        }

        [HttpPost]
        public IActionResult JobProfileAdd([FromBody] JobProfileModel model)
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

        [HttpDelete]
        public IActionResult JobProfileDelete(long jobProfileId)
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

        [HttpPut]
        public IActionResult JobProfileUpdate([FromBody] JobProfileModel model)
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

        private bool ValidateJobProfile(JobProfileModel jobProfileModel, out string errorData)
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
