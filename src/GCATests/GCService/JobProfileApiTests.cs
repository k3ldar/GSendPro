using System.Text.Json;

using GSendService.Api;

using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedPluginFeatures;

using static GSendShared.Constants;

namespace GSendTests.GCService
{
    [TestClass]
    public class JobProfileApiTests
    {
        [TestMethod]
        public void JobProfilesGet_RetrievesAllJobProfiles_Success()
        {
            MockGSendDataProvider gSendDataProvider = new MockGSendDataProvider(new string[] { "ProverXL", "3018" });
            gSendDataProvider.JobProfiles.Add(new JobProfileModel(1) { Name = "Profile 1", Description = "test 1" });
            gSendDataProvider.JobProfiles.Add(new JobProfileModel(325) { Name = "Profile 2", Description = "test 2" });
            gSendDataProvider.JobProfiles.Add(new JobProfileModel(9865433) { Name = "Profile 3", Description = "test 3" });
            JobProfileApi sut = new JobProfileApi(gSendDataProvider, new MockNotification());

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
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(new string[] { "ProverXL", "3018" });
            JobProfileApi sut = new JobProfileApi(gSendDataProvider, new MockNotification());
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
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(new string[] { "ProverXL", "3018" });
            JobProfileApi sut = new JobProfileApi(gSendDataProvider, new MockNotification());
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
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(new string[] { "ProverXL", "3018" });
            JobProfileApi sut = new JobProfileApi(gSendDataProvider, new MockNotification());
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
            MockNotification notification = new MockNotification();
            IGSendDataProvider gSendDataProvider = new MockGSendDataProvider(new string[] { "ProverXL", "3018" });
            JobProfileApi sut = new JobProfileApi(gSendDataProvider, notification);
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
        public void JobProfileDelete_InvalidItemNotFound_ReturnsJsonErrorResponse()
        {
            asdf
        }
    }
}
