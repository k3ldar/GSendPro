using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

using GSendService.Api;

using GSendShared.Models;

using GSendTests.Mocks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SharedPluginFeatures;

using static GSendShared.Constants;

namespace GSendTests.GCService
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ToolProfileApiTests
    {
        [TestMethod]
        public void ToolGet_ReturnsAllTools_Success()
        {
            MockGSendDataProvider gSendDataProvider = new(new string[] { "ProverXL", "3018" });
            gSendDataProvider.ToolProfiles.Add(new ToolProfileModel() { Id = 1, Name = "Tool 1", Description = "Tool 1 Desc" });
            gSendDataProvider.ToolProfiles.Add(new ToolProfileModel() { Id = 2, Name = "Tool 2", Description = "Tool 2 Desc" });
            gSendDataProvider.ToolProfiles.Add(new ToolProfileModel() { Id = 3, Name = "Tool 3", Description = "Tool 3 Desc" });

            ToolProfileApi sut = new(gSendDataProvider, new MockNotification());

            IActionResult result = sut.ToolsGet();
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);

            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);
            Assert.IsNotNull(jsonResult.Value);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);

            ToolProfileModel[] profiles = JsonSerializer.Deserialize<ToolProfileModel[]>(jsonResponseModel.ResponseData, DefaultJsonSerializerOptions);
            Assert.IsNotNull(profiles);
            Assert.AreEqual(3, profiles.Length);

            Assert.AreEqual(1, profiles[0].Id);
            Assert.AreEqual("Tool 1", profiles[0].Name);
            Assert.AreEqual("Tool 1 Desc", profiles[0].Description);

            Assert.AreEqual(2, profiles[1].Id);
            Assert.AreEqual("Tool 2", profiles[1].Name);
            Assert.AreEqual("Tool 2 Desc", profiles[1].Description);

            Assert.AreEqual(3, profiles[2].Id);
            Assert.AreEqual("Tool 3", profiles[2].Name);
            Assert.AreEqual("Tool 3 Desc", profiles[2].Description);
        }
    }
}
