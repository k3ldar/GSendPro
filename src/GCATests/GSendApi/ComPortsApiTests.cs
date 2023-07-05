using System.Diagnostics.CodeAnalysis;

using GSendService.Api;

using GSendTests.Mocks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GSendTests.GSendApi
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ComPortsApiTests
    {
        [TestMethod]
        public void GetAllPorts_ReturnsListOfValidPorts_Success()
        {
            ComPortsApi sut = new(new MockComPortProvider());
            Assert.IsNotNull(sut);

            ActionResult Result = sut.GetAllPorts() as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsTrue(jsonValue.Success);
            Assert.AreEqual("[\"COM5\",\"COM6\",\"COM7\"]", jsonValue.ResponseData);
        }
    }
}
