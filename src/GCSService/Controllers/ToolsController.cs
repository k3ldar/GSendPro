using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

using GSendService.Attributes;
using GSendService.Models;

using GSendShared;
using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace GSendService.Controllers
{
    [LicenseValidation]
    public class ToolsController : BaseController
    {
        public const string Name = "Tools";

        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly INotificationService _notificationService;

        public ToolsController(IGSendDataProvider gSendDataProvider, INotificationService notificationService)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        [HttpGet]
        [Breadcrumb(nameof(GSend.Language.Resources.ToolDatabase))]
        public IActionResult Index()
        {
            return View(CreateToolsModel());
        }

        [HttpGet]
        [Breadcrumb(nameof(Add), Name, nameof(Index))]
        public IActionResult Add()
        {
            return View(new ToolModel(GetModelData(), TimeSpan.Zero));
        }

        [HttpPost]
        public IActionResult Add(ToolModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (String.IsNullOrEmpty(model.Name))
                ModelState.AddModelError(String.Empty, GSend.Language.Resources.ToolErrorInvalidName);

            if (String.IsNullOrEmpty(model.Description))
                ModelState.AddModelError(String.Empty, GSend.Language.Resources.ToolErrorInvalidDescription);

            if (!ModelState.IsValid)
            {
                BaseModelData modelData = GetModelData();

                modelData.Breadcrumbs.Add(new BreadcrumbItem(nameof(GSend.Language.Resources.ToolDatabase), "/Tools/Index", false));
                modelData.Breadcrumbs.Add(new BreadcrumbItem(nameof(GSend.Language.Resources.ToolAdd), "/Tools/Add", false));

                ToolModel resultModel = new ToolModel(modelData, TimeSpan.Zero)
                {
                    Name = model.Name,
                    Description = model.Description,
                };

                return View(resultModel);
            }

            IToolProfile toolProfile = new ToolProfileModel()
            {
                Name = model.Name,
                Description = model.Description,
            };

            _gSendDataProvider.ToolAdd(toolProfile);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Breadcrumb(nameof(Edit), Name, nameof(Index))]
        [Route("/Tools/Edit/{id}")]
        public IActionResult Edit(long id)
        {
            IToolProfile tool = _gSendDataProvider.ToolGet(id);

            if (tool == null)
                return RedirectToAction(nameof(Index));

            return View(CreateToolModelFromToolProfile(tool, true));
        }

        [HttpPost]
        public IActionResult Edit(ToolModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (String.IsNullOrEmpty(model.Name))
                ModelState.AddModelError(String.Empty, GSend.Language.Resources.ToolErrorInvalidName);

            if (String.IsNullOrEmpty(model.Description))
                ModelState.AddModelError(String.Empty, GSend.Language.Resources.ToolErrorInvalidDescription);

            if (!ModelState.IsValid)
            {
                BaseModelData modelData = GetModelData();

                modelData.Breadcrumbs.Add(new BreadcrumbItem(nameof(GSend.Language.Resources.ToolDatabase), "/Tools/Index", false));
                modelData.Breadcrumbs.Add(new BreadcrumbItem(nameof(GSend.Language.Resources.Edit), "/Tools/Edit", false));

                ToolModel resultModel = new ToolModel(modelData, TimeSpan.Zero)
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                };

                return View(resultModel);
            }

            IToolProfile toolProfile = new ToolProfileModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };

            _gSendDataProvider.ToolUpdate(toolProfile);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Breadcrumb(nameof(Edit), Name, nameof(Index))]
        [Route("/Tools/ViewUsage/{id}/")]
        public IActionResult ViewUsage(long id)
        {
            IToolProfile tool = _gSendDataProvider.ToolGet(id);

            if (tool == null)
                return RedirectToAction(nameof(Index));

            IEnumerable<JobExecutionStatistics> toolData = _gSendDataProvider.JobExecutionModelsGetByTool(tool, false);

            List<JobExecutionStatistics> machines = toolData.DistinctBy(td => td.MachineName).ToList();

            ChartModel chartModel = new();
            chartModel.DataNames.Add(new KeyValuePair<ChartDataType, string>(ChartDataType.String, "Day"));
            machines.ForEach(m => chartModel.DataNames.Add(new KeyValuePair<ChartDataType, string>(ChartDataType.Number, m.MachineName)));

            Dictionary<DateTime, List<JobExecutionStatistics>> toolDataGrouped = toolData.GroupBy(td => td.Date.Date).ToDictionary(d => d.Key, d => d.ToList());

            foreach (var toolStat in toolDataGrouped)
            {
                List<Decimal> datavalues = new List<decimal>();
                chartModel.DataValues[toolStat.Key.Date.ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern)] = datavalues;

                foreach (var machine in machines)
                {
                    datavalues.Add((decimal)toolStat.Value.Sum(ts => ts.TotalTime.TotalMinutes));
                }
                //var machineGroup = toolStat.Value.Sum(ts => ts.MachineName);
                //machines.ForEach(m => m.MachineName.Equals(toolStat.MachineName) ? );
                //datavalues.Add(day.BotVisits);
                //datavalues.Add(day.Bounced);
            }

            ToolUsageViewModel model = new ToolUsageViewModel(GetModelData(), tool.Name, chartModel);

            return View(model);
        }

        [HttpGet]
        [Breadcrumb(nameof(Delete), nameof(Index))]
        [Route("/Tools/Delete/{id}")]
        public IActionResult Delete(long id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/Tools/ResetUsage/{id}")]
        public IActionResult ResetUsage(long id)
        {
            IToolProfile tool = _gSendDataProvider.ToolGet(id);

            if (tool == null)
                return RedirectToAction(nameof(Index));

            _gSendDataProvider.ToolResetUsage(tool);
            return RedirectToAction(nameof(Edit), new { id });
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
                tools.Add(CreateToolModelFromToolProfile(toolProfile, false));
            }

            return new ToolsModel(GetModelData(), tools);
        }

        private ToolModel CreateToolModelFromToolProfile(IToolProfile toolProfile, bool includeModelData)
        {
            TimeSpan totalTimeUsed = _gSendDataProvider.JobExecutionByTool(toolProfile);

            ToolModel Result = null;

            if (includeModelData)
                Result = new ToolModel(GetModelData(), totalTimeUsed);
            else
                Result = new ToolModel(totalTimeUsed);


            Result.Id = toolProfile.Id;
            Result.Name = toolProfile.Name;
            Result.Description = toolProfile.Description;
            Result.UsageLastReset = toolProfile.UsageLastReset;

            return Result;
        }
    }
}
