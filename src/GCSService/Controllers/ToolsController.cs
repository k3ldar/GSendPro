using System;
using System.Collections.Generic;

using GSendService.Attributes;
using GSendService.Models;

using GSendShared;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace GSendService.Controllers
{
    [LicenseValidation]
    public class ToolsController : BaseController
    {
        private const string Name = "Tools";

        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly INotificationService _notificationService;

        public ToolsController(IGSendDataProvider gSendDataProvider, INotificationService notificationService)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        [HttpGet]
        [Breadcrumb(nameof(Index))]
        public IActionResult Index()
        {
            return View(CreateToolsModel());
        }

        [HttpGet]
        [Breadcrumb(nameof(Add), nameof(Index))]
        public IActionResult Add()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Add(ToolModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Breadcrumb(nameof(Edit), nameof(Index))]
        [Route("/Tools/Edit/{id}")]
        public IActionResult Edit(long id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Edit(ToolModel model)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Breadcrumb(nameof(Delete), nameof(Index))]
        [Route("/Tools/Delete/{id}")]
        public IActionResult Delete(long id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Delete(ToolModel model)
        {
            throw new NotImplementedException();
        }

        private ToolsModel CreateToolsModel()
        {
            IReadOnlyList<IToolProfile> dbTools = _gSendDataProvider.ToolsGet();

            List<ToolModel> tools = new();

            foreach (IToolProfile toolProfile in dbTools)
            {
                TimeSpan totalTimeUsed = _gSendDataProvider.JobExecutionByTool(toolProfile);

                ToolModel toolModel = new ToolModel(totalTimeUsed)
                {
                    Id = toolProfile.Id,
                    Name = toolProfile.Name,
                    Description = toolProfile.Description,
                    UsageLastReset = toolProfile.UsageLastReset,
                };

                tools.Add(toolModel);
            }

            return new ToolsModel(GetModelData(), tools);
        }
    }
}
