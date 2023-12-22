using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

using GSendService.Api;

using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedPluginFeatures;

using static GSendShared.Constants;

namespace GSendTests.GSendApi
{
    [TestClass]
    public class JobProfileApiTests
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
        public void JobProfilesGet_RetrievesAllJobProfiles_Success()
        {
            MockGSendDataProvider gSendDataProvider = new(["ProverXL", "3018"]);
            gSendDataProvider.JobProfiles.Add(new JobProfileModel(1) { Name = "Profile 1", Description = "test 1" });
            gSendDataProvider.JobProfiles.Add(new JobProfileModel(325) { Name = "Profile 2", Description = "test 2" });
            gSendDataProvider.JobProfiles.Add(new JobProfileModel(9865433) { Name = "Profile 3", Description = "test 3" });
            JobProfileApi sut = new(gSendDataProvider, new MockNotification());

            IActionResult result = sut.JobProfilesGet();
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);

            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);
            Assert.IsNotNull(jsonResult.Value);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);

            JobProfileModel[] profiles = JsonSerializer.Deserialize<JobProfileModel[]>(jsonResponseModel.ResponseData, DefaultJsonSerializerOptions);
            Assert.IsNotNull(profiles);
            Assert.AreEqual(3, profiles.Length);

            Assert.AreEqual(1, profiles[0].Id);
            Assert.AreEqual("Profile 1", profiles[0].Name);
            Assert.AreEqual("test 1", profiles[0].Description);

            Assert.AreEqual(325, profiles[1].Id);
            Assert.AreEqual("Profile 2", profiles[1].Name);
            Assert.AreEqual("test 2", profiles[1].Description);

            Assert.AreEqual(9865433, profiles[2].Id);
            Assert.AreEqual("Profile 3", profiles[2].Name);
            Assert.AreEqual("test 3", profiles[2].Description);
        }

        [TestMethod]
        public void JobProfileAdd_NullParameter_Returns_JsonErrorResponse()
        {
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, new MockNotification());
            ActionResult Result = sut.JobProfileAdd(null) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid job profile model", jsonValue.ResponseData);
        }

        [TestMethod]
        public void JobProfileAdd_NullName_Returns_JsonErrorResponse()
        {
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, new MockNotification());
            ActionResult Result = sut.JobProfileAdd(new JobProfileModel(1) { Description = "a desc" }) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid job profile name", jsonValue.ResponseData);
        }

        [TestMethod]
        public void JobProfileAdd_NullDescription_Returns_JsonErrorResponse()
        {
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, new MockNotification());
            ActionResult Result = sut.JobProfileAdd(new JobProfileModel(1) { Name = "a test" }) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid job profile description", jsonValue.ResponseData);
        }

        [TestMethod]
        public void JobProfileAdd_ValidModel_Returns_JsonResponse()
        {
            MockNotification notification = new();
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, notification);
            ActionResult Result = sut.JobProfileAdd(new JobProfileModel(1) { Name = "a test", Description = "a test" }) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsTrue(jsonValue.Success);
            Assert.AreEqual("", jsonValue.ResponseData);

            Assert.AreEqual(1, notification.Events.Count);
            Assert.IsTrue(notification.Events.ContainsKey("JobProfileAdd"));
        }

        [TestMethod]
        public void JobProfileAdd_InvalidModel_DuplicateName_Returns_JsonResponse()
        {
            MockNotification notification = new();
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, notification);
            ActionResult Result = sut.JobProfileAdd(new JobProfileModel(0) { Name = "a test", Description = "a test" }) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsTrue(jsonValue.Success);
            Assert.AreEqual("", jsonValue.ResponseData);

            Assert.AreEqual(1, notification.Events.Count);
            Assert.IsTrue(notification.Events.ContainsKey("JobProfileAdd"));

            Result = sut.JobProfileAdd(new JobProfileModel(-1) { Name = "a test", Description = "a test" }) as ActionResult;
            Assert.IsNotNull(Result);

            jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("duplicate name", jsonValue.ResponseData);
        }

        [TestMethod]
        public void JobProfileDelete_InvalidItemNotFound_ReturnsJsonErrorResponse()
        {
            MockNotification notification = new();
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, notification);
            ActionResult Result = sut.JobProfileDelete(111) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Profile not found", jsonValue.ResponseData);

            Assert.AreEqual(0, notification.Events.Count);
        }

        [TestMethod]
        public void JobProfileDelete_ValidModel_LastRemainingJobProfile_Returns_JsonErrorResponse()
        {
            MockNotification notification = new();
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, notification);
            ActionResult Result = sut.JobProfileAdd(new JobProfileModel(1) { Name = "a test", Description = "a test" }) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsTrue(jsonValue.Success);
            Assert.AreEqual("", jsonValue.ResponseData);

            Assert.AreEqual(1, notification.Events.Count);
            Assert.IsTrue(notification.Events.ContainsKey("JobProfileAdd"));

            jsonResult = sut.JobProfileDelete(1) as JsonResult;
            Assert.IsNotNull(Result);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Profile not removed", jsonValue.ResponseData);
        }

        [TestMethod]
        public void JobProfileDelete_ValidModel_ItemCanBeDeleted_Returns_JsonSuccessResponse()
        {
            MockNotification notification = new();
            MockGSendDataProvider gSendDataProvider = new(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, notification);
            ActionResult Result = sut.JobProfileAdd(new JobProfileModel(-1) { Name = "a test", Description = "a test" }) as ActionResult;
            Assert.IsNotNull(Result);

            Result = sut.JobProfileAdd(new JobProfileModel(-1) { Name = "a test 2", Description = "a test" }) as ActionResult;
            Assert.IsNotNull(Result);
            Assert.AreEqual(2, gSendDataProvider.JobProfiles.Count);

            Assert.AreEqual(1, notification.Events.Count);
            Assert.IsTrue(notification.Events.ContainsKey("JobProfileAdd"));
            Assert.AreEqual(2, notification.Events["JobProfileAdd"].Count);

            JsonResult jsonResult = sut.JobProfileDelete(1) as JsonResult;
            Assert.IsNotNull(Result);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsTrue(jsonValue.Success);
            Assert.AreEqual("", jsonValue.ResponseData);

            Assert.AreEqual(2, notification.Events.Count);
            Assert.IsTrue(notification.Events.ContainsKey("JobProfileRemove"));
            Assert.AreEqual(1, notification.Events["JobProfileRemove"].Count);
        }

        [TestMethod]
        public void JobProfileUpdate_InvalidModel_Null_Returns_JsonErrorResponse()
        {
            MockNotification notification = new();
            MockGSendDataProvider gSendDataProvider = new(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, notification);
            ActionResult Result = sut.JobProfileAdd(new JobProfileModel(-1) { Name = "a test", Description = "a test" }) as ActionResult;
            Assert.IsNotNull(Result);

            IActionResult result = sut.JobProfileUpdate(null);
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);

            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);
            Assert.IsNotNull(jsonResult.Value);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid job profile model", jsonValue.ResponseData);
        }

        [TestMethod]
        public void JobProfileUpdate_InvalidModel_NameNull_Returns_JsonErrorResponse()
        {
            MockNotification notification = new();
            MockGSendDataProvider gSendDataProvider = new(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, notification);

            IActionResult result = sut.JobProfileUpdate(new JobProfileModel(0));
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);

            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);
            Assert.IsNotNull(jsonResult.Value);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid job profile name", jsonValue.ResponseData);
        }

        [TestMethod]
        public void JobProfileUpdate_InvalidModel_DescriptionNull_Returns_JsonErrorResponse()
        {
            MockNotification notification = new();
            MockGSendDataProvider gSendDataProvider = new(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, notification);

            IActionResult result = sut.JobProfileUpdate(new JobProfileModel(0) { Name = "name" });
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);

            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);
            Assert.IsNotNull(jsonResult.Value);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid job profile description", jsonValue.ResponseData);
        }

        [TestMethod]
        public void JobProfileUpdate_InvalidModel_NotFound_Returns_JsonErrorResponse()
        {
            MockNotification notification = new();
            MockGSendDataProvider gSendDataProvider = new(["ProverXL", "3018"]);
            JobProfileApi sut = new(gSendDataProvider, notification);

            IActionResult result = sut.JobProfileUpdate(new JobProfileModel(245) { Name = "name", Description = "desc" });
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);

            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);
            Assert.IsNotNull(jsonResult.Value);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Profile not found", jsonValue.ResponseData);
        }

        [TestMethod]
        public void JobProfileUpdate_ValidModel_ItemCanBeUpdated_Returns_JsonSuccessResponse()
        {
            MockGSendDataProvider gSendDataProvider = new(["ProverXL", "3018"]);
            gSendDataProvider.JobProfiles.Add(new JobProfileModel(1) { Name = "Profile 1", Description = "test 1" });
            gSendDataProvider.JobProfiles.Add(new JobProfileModel(325) { Name = "Profile 2", Description = "test 2" });
            gSendDataProvider.JobProfiles.Add(new JobProfileModel(9865433) { Name = "Profile 3", Description = "test 3" });
            JobProfileApi sut = new(gSendDataProvider, new MockNotification());

            IActionResult result = sut.JobProfilesGet();
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);

            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);
            Assert.IsNotNull(jsonResult.Value);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);

            JobProfileModel[] profiles = JsonSerializer.Deserialize<JobProfileModel[]>(jsonResponseModel.ResponseData, DefaultJsonSerializerOptions);
            Assert.IsNotNull(profiles);
            Assert.AreEqual(3, profiles.Length);

            JobProfileModel profile = profiles[1];
            Assert.IsNotNull(profile);
            profile.Name = "Updated name";

            result = sut.JobProfileUpdate(profile);
            Assert.IsNotNull(result);
            jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);

            result = sut.JobProfilesGet();
            Assert.IsNotNull(result);

            jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);

            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);
            Assert.IsNotNull(jsonResult.Value);

            jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);

            profiles = JsonSerializer.Deserialize<JobProfileModel[]>(jsonResponseModel.ResponseData, DefaultJsonSerializerOptions);
            Assert.IsNotNull(profiles);
            Assert.AreEqual(3, profiles.Length);

            Assert.AreEqual("Updated name", profiles[1].Name);
        }
    }
}
