using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using GSendService.Api;
using GSendShared;
using GSendShared.Models;

using GSendTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendApi
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class JobExecutionTests
    {
        [TestMethod]
        public void CreateJobExecution_FailToCreateDBRecord_Returns_JsonErrorResponse()
        {
            MockGSendDataProvider gSendDataProvider = new MockGSendDataProvider(new string[] { "ProverXL", "3018" });
            gSendDataProvider.CreateFalseResponseWhenCalled = true;
            JobExecutionApi sut = new(gSendDataProvider, new MockNotification());

            ActionResult Result = sut.CreateJobExecution(1, 2, 3) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Unable to create job execution", jsonValue.ResponseData);
        }

        [TestMethod]
        public void CreateJobExecution_DBRecordCreated_Returns_JsonSuccess()
        {
            MockGSendDataProvider gSendDataProvider = new MockGSendDataProvider(new string[] { "ProverXL", "3018" });
            JobExecutionApi sut = new(gSendDataProvider, new MockNotification());

            ActionResult Result = sut.CreateJobExecution(1, 2, 3) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsTrue(jsonValue.Success);
            Assert.IsFalse(String.IsNullOrEmpty(jsonValue.ResponseData));

            IJobExecution jsonResponse = JsonSerializer.Deserialize<IJobExecution>(jsonValue.ResponseData);
            Assert.IsNotNull(jsonResponse);
            Assert.AreEqual(JobExecutionStatus.None, jsonResponse.Status);
        }
    }
}
