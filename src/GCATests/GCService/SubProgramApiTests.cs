using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

using GSendCommon;

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
    [ExcludeFromCodeCoverage]
    public class SubProgramApiTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_Invalid_ParamNull_Throws_ArgumentNullException()
        {
            new SubprogramApi(null);
        }

        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            new SubprogramApi(new MockSubPrograms());
        }

        [TestMethod]
        public void GetAllSubprogramNames_Success()
        {
            MockSubPrograms subPrograms = new();
            subPrograms.SubPrograms.Add(new SubProgramModel("test1", "desc 1", "asfdasdf"));
            subPrograms.SubPrograms.Add(new SubProgramModel("test2", "desc 2", "asfdasdf"));
            subPrograms.SubPrograms.Add(new SubProgramModel("test3", "desc 3", "asfdasdf"));
            subPrograms.SubPrograms.Add(new SubProgramModel("test4", "desc 4", "asfdasdf"));
            subPrograms.SubPrograms.Add(new SubProgramModel("test5", "desc 5", "asfdasdf"));
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.GetAllSubprograms() as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            List<ISubProgram> names = JsonSerializer.Deserialize<List<ISubProgram>>(jsonResponseModel.ResponseData, GSendShared.Constants.DefaultJsonSerializerOptions);

            Assert.AreEqual(5, names.Count);
            Assert.AreEqual("test1", names[0].Name);
            Assert.AreEqual("test2", names[1].Name);
            Assert.AreEqual("test3", names[2].Name);
            Assert.AreEqual("test4", names[3].Name);
            Assert.AreEqual("test5", names[4].Name);
            Assert.AreEqual("desc 1", names[0].Description);
            Assert.AreEqual("desc 2", names[1].Description);
            Assert.AreEqual("desc 3", names[2].Description);
            Assert.AreEqual("desc 4", names[3].Description);
            Assert.AreEqual("desc 5", names[4].Description);
        }

        [TestMethod]
        public void SubprogramExists_NullName_DoesNotExist_ReturnsFalse()
        {
            MockSubPrograms subPrograms = new();
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.SubprogramExists(null) as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            bool exists = JsonSerializer.Deserialize<bool>(jsonResponseModel.ResponseData, GSendShared.Constants.DefaultJsonSerializerOptions);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void SubprogramExists_ValidName_DoesNotExist_ReturnsFalse()
        {
            MockSubPrograms subPrograms = new();
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.SubprogramExists("O5000") as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            bool exists = JsonSerializer.Deserialize<bool>(jsonResponseModel.ResponseData, GSendShared.Constants.DefaultJsonSerializerOptions);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void SubprogramExists_ValidName_Exists_ReturnsTrue()
        {
            MockSubPrograms subPrograms = new();
            subPrograms.SubPrograms.Add(new SubProgramModel("O5000", "desc 1", "asfdasdf"));
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.SubprogramExists("O5000") as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            bool exists = JsonSerializer.Deserialize<bool>(jsonResponseModel.ResponseData, GSendShared.Constants.DefaultJsonSerializerOptions);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void SubSubprogramDelete_SubProgramNotfound_Returns_False()
        {
            MockSubPrograms subPrograms = new();
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.SubprogramDelete("O500") as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            bool deleted = JsonSerializer.Deserialize<bool>(jsonResponseModel.ResponseData, GSendShared.Constants.DefaultJsonSerializerOptions);
            Assert.IsFalse(deleted);
        }

        [TestMethod]
        public void SubSubprogramDelete_SubProgramFound_Returns_True()
        {
            MockSubPrograms subPrograms = new();
            subPrograms.SubPrograms.Add(new SubProgramModel("O5000", "desc 1", "asfdasdf"));
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.SubprogramDelete("O5000") as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            bool deleted = JsonSerializer.Deserialize<bool>(jsonResponseModel.ResponseData, GSendShared.Constants.DefaultJsonSerializerOptions);
            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public void SubprogramGet_SubProgramNotFound_Returns_False()
        {
            MockSubPrograms subPrograms = new();
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.SubprogramGet("O500") as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);
            Assert.IsFalse(jsonResponseModel.Success);
        }

        [TestMethod]
        public void SubprogramGet_SubProgramNotFound_Returns_True()
        {
            MockSubPrograms subPrograms = new();
            subPrograms.SubPrograms.Add(new SubProgramModel("O5000", "desc 1", "asfdasdf"));
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.SubprogramGet("O5000") as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);
            Assert.IsTrue(jsonResponseModel.Success);
            ISubProgram subprogram = JsonSerializer.Deserialize<ISubProgram>(jsonResponseModel.ResponseData, GSendShared.Constants.DefaultJsonSerializerOptions);
            Assert.IsNotNull(subprogram);
            Assert.AreEqual("O5000", subprogram.Name);
            Assert.AreEqual("desc 1", subprogram.Description);
            Assert.AreEqual("asfdasdf", subprogram.Contents);
        }

        [TestMethod]
        public void SubprogramUpdate_InvalidModel_Returns_False()
        {
            MockSubPrograms subPrograms = new();
            subPrograms.SubPrograms.Add(new SubProgramModel("O5000", "desc 1", "asfdasdf"));
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.SubprogramUpdate(null) as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);
            Assert.IsFalse(jsonResponseModel.Success);
        }

        [TestMethod]
        public void SubprogramUpdate_UpdatesFromModel_Returns_False()
        {
            SubProgramModel subprogram = new SubProgramModel("O5000", "desc 1", "asfdasdf");
            MockSubPrograms subPrograms = new();
            subPrograms.SubPrograms.Add(subprogram);
            SubprogramApi sut = new SubprogramApi(subPrograms);

            ActionResult result = sut.SubprogramUpdate(new SubProgramModel("O5000", "new desc", "new content")) as ActionResult;
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);
            Assert.IsTrue(jsonResponseModel.Success);

            Assert.AreEqual("O5000", subprogram.Name);
            Assert.AreEqual("new desc", subprogram.Description);
            Assert.AreEqual("new content", subprogram.Contents);
        }
    }
}
