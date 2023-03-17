﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using GSendShared;
using GSendService.Api;
using GSendTests.Mocks;
using SharedPluginFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using static GSendShared.Constants;
using GSendShared.Models;

namespace GSendTests.GCService
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MachineApiTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_Invalid_ParamNull_Throws_ArgumentNullException()
        {
            IMachineProvider machineProvider = null;
            new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), new MockNotification());
        }

        [TestMethod]
        public void Construct_ValidInstance_Success()
        {
            IMachineProvider machineProvider = new MockMachineProvider();
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), new MockNotification());
            Assert.IsNotNull(sut);
            Assert.IsInstanceOfType(sut, typeof(BaseController));
        }

        [TestMethod]
        public void MachinesGet_RetrievesAllListedMachines_Success()
        {
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), new MockNotification());

            IActionResult result = sut.MachinesGet();
            Assert.IsNotNull(result);

            JsonResult jsonResult = result as JsonResult;
            Assert.IsNotNull(jsonResult);

            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);
            Assert.IsNotNull(jsonResult.Value);

            JsonResponseModel jsonResponseModel = jsonResult.Value as JsonResponseModel;
            Assert.IsNotNull(jsonResponseModel);

            IMachine[] machines = JsonSerializer.Deserialize<IMachine[]>(jsonResponseModel.ResponseData, DefaultJsonSerializerOptions);
            Assert.IsNotNull(machines);
            Assert.AreEqual(2, machines.Length);

            Assert.AreEqual(0, machines[0].Id);
            Assert.AreEqual("ProverXL", machines[0].Name);
            Assert.AreEqual("COM2", machines[0].ComPort);
            Assert.AreEqual(MachineType.CNC, machines[0].MachineType);

            Assert.AreEqual(1, machines[1].Id);
            Assert.AreEqual("3018", machines[1].Name);
            Assert.AreEqual("COM3", machines[1].ComPort);
            Assert.AreEqual(MachineType.Laser, machines[1].MachineType);
        }

        [TestMethod]
        public void MachinesAdd_NullParameter_Returns_JsonErrorResponse()
        {
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), new MockNotification());
            ActionResult Result = sut.MachineAdd(null) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid machine model", jsonValue.ResponseData);
        }

        [TestMethod]
        public void MachinesAdd_NameMissing_Returns_JsonErrorResponse()
        {
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), new MockNotification());

            MachineModel model = new MachineModel();

            ActionResult Result = sut.MachineAdd(model) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid name", jsonValue.ResponseData);
        }

        [TestMethod]
        public void MachinesAdd_NameIsDuplicate_Returns_JsonErrorResponse()
        {
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), new MockNotification());

            MachineModel model = new MachineModel()
            {
                Name = "ProverXL",
                MachineType = MachineType.CNC,
                ComPort = "COM6"
            };

            ActionResult Result = sut.MachineAdd(model) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Name already exists", jsonValue.ResponseData);
        }

        [TestMethod]
        public void MachinesAdd_InvalidMachineType_Returns_JsonErrorResponse()
        {
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), new MockNotification());

            MachineModel model = new MachineModel()
            {
                Name = "ProverXL 2",
                MachineType = MachineType.Unspecified,
            };

            ActionResult Result = sut.MachineAdd(model) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid machine type", jsonValue.ResponseData);
        }

        [TestMethod]
        public void MachinesAdd_DuplicateComPort_Returns_JsonErrorResponse()
        {
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(new byte[] { 2 }), new MockSettingsProvider(), new MockNotification());

            MachineModel model = new MachineModel()
            {
                Name = "ProverXL 2",
                MachineType = MachineType.Printer,
                ComPort = "COM2"
            };

            ActionResult Result = sut.MachineAdd(model) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid com port duplicate", jsonValue.ResponseData);
        }

        [TestMethod]
        public void MachinesAdd_ComPortNotFound_Returns_JsonErrorResponse()
        {
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), new MockNotification());

            MachineModel model = new MachineModel()
            {
                Name = "ProverXL 2",
                MachineType = MachineType.Printer,
                ComPort = "COM2"
            };

            ActionResult Result = sut.MachineAdd(model) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Invalid com port not found", jsonValue.ResponseData);
        }

        [TestMethod]
        public void MachinesAdd_ValidNewDetails_Returns_JsonSuccessResponse()
        {
            MockNotification notification = new MockNotification();
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), notification);

            MachineModel model = new MachineModel()
            {
                Name = "ProverXL 2",
                MachineType = MachineType.Printer,
                ComPort = "COM7"
            };

            ActionResult Result = sut.MachineAdd(model) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsTrue(jsonValue.Success);
            Assert.AreEqual("", jsonValue.ResponseData);

            Assert.IsTrue(notification.Events.ContainsKey("MachineAdd"));
        }

        [TestMethod]
        public void MachinesDelete_InvalidMachineNotFound_Returns_JsonErrorResponse()
        {
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), new MockNotification());

            ActionResult Result = sut.MachineDelete(245) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(400, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsFalse(jsonValue.Success);
            Assert.AreEqual("Machine not found", jsonValue.ResponseData);
        }

        [TestMethod]
        public void MachinesDelete_ValidRequest_Returns_JsonSuccessResponse()
        {
            MockNotification notification = new MockNotification();
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(), new MockSettingsProvider(), notification);

            ActionResult Result = sut.MachineDelete(1) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsTrue(jsonValue.Success);
            Assert.AreEqual("", jsonValue.ResponseData);

            Assert.IsTrue(notification.Events.ContainsKey("MachineRemove"));
        }

        [TestMethod]
        public void MachinesUpdate_ValidRequest_Returns_JsonSuccessResponse()
        {
            MockNotification notification = new MockNotification();
            IMachineProvider machineProvider = new MockMachineProvider(new string[] { "ProverXL", "3018" });
            MachineApi sut = new MachineApi(machineProvider, new MockComPortProvider(new byte[] { 3 }), new MockSettingsProvider(), notification);

            MachineModel machineToUpdate = JsonSerializer.Deserialize<MachineModel>(JsonSerializer.Serialize(machineProvider.MachineGet(1)));
            machineToUpdate.Name = "Updated";
            machineToUpdate.MachineType = MachineType.Printer;
            ActionResult Result = sut.MachineUpdate(machineToUpdate) as ActionResult;
            Assert.IsNotNull(Result);

            JsonResult jsonResult = Result as JsonResult;
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(200, jsonResult.StatusCode);
            Assert.AreEqual("application/json", jsonResult.ContentType);

            dynamic jsonValue = jsonResult.Value;
            Assert.IsTrue(jsonValue.Success);
            Assert.AreEqual("", jsonValue.ResponseData);

            Assert.IsTrue(notification.Events.ContainsKey("MachineUpdate"));
        }
    }
}