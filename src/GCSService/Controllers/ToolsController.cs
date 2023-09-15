using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public ToolsController(IGSendDataProvider gSendDataProvider)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
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
            return View(new ToolModel(GetModelData(), TimeSpan.Zero, new List<ToolUsageHistoryModel>()) { ExpectedLifeMinutes = 60 * 30 });
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

                ToolModel resultModel = new ToolModel(modelData, TimeSpan.Zero, model.History)
                {
                    Name = model.Name,
                    Description = model.Description,
                    ExpectedLifeMinutes = model.ExpectedLifeMinutes,
                    LengthInMillimetres = model.LengthInMillimetres,
                };

                return View(resultModel);
            }

            IToolProfile toolProfile = new ToolProfileModel()
            {
                Name = model.Name,
                Description = model.Description,
                ExpectedLifeMinutes = model.ExpectedLifeMinutes,
                LengthInMillimetres = model.LengthInMillimetres,
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

                ToolModel resultModel = new ToolModel(modelData, TimeSpan.Zero, new List<ToolUsageHistoryModel>())
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    ExpectedLifeMinutes = model.ExpectedLifeMinutes,
                };

                return View(resultModel);
            }

            IToolProfile toolProfile = new ToolProfileModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ExpectedLifeMinutes = model.ExpectedLifeMinutes,
                LengthInMillimetres = model.LengthInMillimetres,
            };

            _gSendDataProvider.ToolUpdate(toolProfile);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("/Tools/ViewUsage/{id}/")]
        public IActionResult ViewUsage(long id)
        {
            IToolProfile tool = _gSendDataProvider.ToolGet(id);

            if (tool == null)
                return RedirectToAction(nameof(Index));

            ToolUsageViewModel model = CreateToolUsageModel(tool, true, ChartViewPeriod.Daily, ChartViewTimePeriod.Minutes);

            return View(model);
        }

        [HttpPost]
        public IActionResult ViewUsage(ToolUsageViewModel model)
        {
            if (model == null)
                return RedirectToAction(nameof(Index));

            IToolProfile tool = _gSendDataProvider.ToolGet(model.ToolId);

            if (tool == null)
                return RedirectToAction(nameof(Index));

            return View(CreateToolUsageModel(tool, model.RecentData, model.ViewPeriod, model.TimePeriod));
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

        private ToolUsageViewModel CreateToolUsageModel(IToolProfile tool, bool isRecent, ChartViewPeriod viewPeriod, ChartViewTimePeriod viewTimePeriod)
        {
            IEnumerable<JobExecutionStatistics> toolData = _gSendDataProvider.JobExecutionModelsGetByTool(tool, isRecent)
                .Where(td => td.TotalTime.TotalSeconds > 1);

            List<JobExecutionStatistics> machines = toolData.DistinctBy(td => td.MachineName).ToList();

            ChartModel chartModel = GetToolUsage(toolData, machines, viewPeriod, viewTimePeriod);

            ToolUsageViewModel model = new ToolUsageViewModel(GetModelData(), tool, chartModel, viewPeriod, viewTimePeriod, isRecent, toolData, tool.History);

            model.Breadcrumbs.Add(new BreadcrumbItem(GSend.Language.Resources.ToolDatabase, "/Tools/Index", false));
            model.Breadcrumbs.Add(new BreadcrumbItem(tool.Name, $"/Tools/Edit/{tool.Id}/", false));
            model.Breadcrumbs.Add(new BreadcrumbItem(GSend.Language.Resources.ViewUsage, $"/Tools/ViewUsage/{tool.Id}/", false));
            return model;
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

            ToolModel Result;

            if (includeModelData)
                Result = new ToolModel(GetModelData(), totalTimeUsed, toolProfile.History);
            else
                Result = new ToolModel(totalTimeUsed, toolProfile.History);


            Result.Id = toolProfile.Id;
            Result.Name = toolProfile.Name;
            Result.Description = toolProfile.Description;
            Result.UsageLastReset = toolProfile.UsageLastReset;
            Result.ExpectedLifeMinutes = toolProfile.ExpectedLifeMinutes;
            Result.LengthInMillimetres = toolProfile.LengthInMillimetres;

            return Result;
        }

        private static ChartModel GetToolUsage(IEnumerable<JobExecutionStatistics> toolData, List<JobExecutionStatistics> machines, ChartViewPeriod viewPeriod, ChartViewTimePeriod timePeriod)
        {
            switch (viewPeriod)
            {
                case ChartViewPeriod.Daily:
                    return ToolUsageByDay(toolData, machines, timePeriod);
                case ChartViewPeriod.Weekly:
                    return ToolUsageByWeek(toolData, machines, timePeriod);
                case ChartViewPeriod.Monthly:
                    return ToolUsageByMonth(toolData, machines, timePeriod);
                default:
                    throw new NotSupportedException();
            }
        }

        private static ChartModel ToolUsageByDay(IEnumerable<JobExecutionStatistics> toolData, List<JobExecutionStatistics> machines, ChartViewTimePeriod timePeriod)
        {
            ChartModel chartModel = new();
            chartModel.DataNames.Add(new KeyValuePair<ChartDataType, string>(ChartDataType.String, GSend.Language.Resources.Day));
            machines.ForEach(m => chartModel.DataNames.Add(new KeyValuePair<ChartDataType, string>(ChartDataType.Number, m.MachineName)));

            Dictionary<DateTime, List<JobExecutionStatistics>> toolDataGrouped = toolData.GroupBy(td => td.Date.Date).ToDictionary(d => d.Key, d => d.ToList());

            foreach (var toolStat in toolDataGrouped)
            {
                List<decimal> datavalues = new List<decimal>();
                chartModel.DataValues[toolStat.Key.Date.ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern)] = datavalues;

                foreach (var machine in machines)
                {
                    if (timePeriod == ChartViewTimePeriod.Minutes)
                        datavalues.Add((decimal)toolStat.Value.Sum(ts => ts.TotalTime.TotalMinutes));
                    else
                        datavalues.Add((decimal)toolStat.Value.Sum(ts => ts.TotalTime.TotalHours));
                }
            }

            return chartModel;
        }

        private static ChartModel ToolUsageByWeek(IEnumerable<JobExecutionStatistics> toolData, List<JobExecutionStatistics> machines, ChartViewTimePeriod timePeriod)
        {
            ChartModel chartModel = new();
            chartModel.DataNames.Add(new KeyValuePair<ChartDataType, string>(ChartDataType.String, GSend.Language.Resources.Week));
            machines.ForEach(m => chartModel.DataNames.Add(new KeyValuePair<ChartDataType, string>(ChartDataType.Number, m.MachineName)));

            Dictionary<int, List<JobExecutionStatistics>> toolDataGrouped = toolData.GroupBy(td => ISOWeek.GetWeekOfYear(td.Date.Date)).ToDictionary(d => d.Key, d => d.ToList());

            foreach (var toolStat in toolDataGrouped)
            {
                List<decimal> datavalues = new List<decimal>();
                chartModel.DataValues[toolStat.Key.ToString()] = datavalues;

                foreach (var machine in machines)
                {
                    if (timePeriod == ChartViewTimePeriod.Minutes)
                        datavalues.Add((decimal)toolStat.Value.Sum(ts => ts.TotalTime.TotalMinutes));
                    else
                        datavalues.Add((decimal)toolStat.Value.Sum(ts => ts.TotalTime.TotalHours));
                }
            }

            return chartModel;
        }

        private static ChartModel ToolUsageByMonth(IEnumerable<JobExecutionStatistics> toolData, List<JobExecutionStatistics> machines, ChartViewTimePeriod timePeriod)
        {
            ChartModel chartModel = new();
            chartModel.DataNames.Add(new KeyValuePair<ChartDataType, string>(ChartDataType.String, GSend.Language.Resources.Month));
            machines.ForEach(m => chartModel.DataNames.Add(new KeyValuePair<ChartDataType, string>(ChartDataType.Number, m.MachineName)));

            Dictionary<int, List<JobExecutionStatistics>> toolDataGrouped = toolData.GroupBy(td => td.Date.Month).ToDictionary(d => d.Key, d => d.ToList());

            foreach (var toolStat in toolDataGrouped)
            {
                List<decimal> datavalues = new List<decimal>();
                chartModel.DataValues[toolStat.Key.ToString()] = datavalues;

                foreach (var machine in machines)
                {
                    if (timePeriod == ChartViewTimePeriod.Minutes)
                        datavalues.Add((decimal)toolStat.Value.Sum(ts => ts.TotalTime.TotalMinutes));
                    else
                        datavalues.Add((decimal)toolStat.Value.Sum(ts => ts.TotalTime.TotalHours));
                }
            }

            return chartModel;
        }
    }
}
