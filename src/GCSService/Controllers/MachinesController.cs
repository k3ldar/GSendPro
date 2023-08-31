using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;

using GSendService.Attributes;
using GSendService.Models;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

namespace GSendService.Controllers
{
    [LicenseValidation]
    public class MachinesController : BaseController
    {
        public const string Name = "Machines";
        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly IComPortProvider _comPortProvider;
        private readonly INotificationService _notificationService;

        public MachinesController(IGSendDataProvider gSendDataProvider, IComPortProvider comPortProvider,
            INotificationService notificationService)
        {
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _comPortProvider = comPortProvider ?? throw new ArgumentNullException(nameof(comPortProvider));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(HomeController.Index), HomeController.Name);
        }

        [HttpGet]
        [Route("/Machines/View/{machineId}/")]
        public IActionResult View(long machineId)
        {
            IMachine machine = _gSendDataProvider.MachineGet(machineId);

            if (machine == null)
                return RedirectToAction(nameof(Index));

            ViewMachineModel viewMachineModel = CreateMachineViewModel(machine);
            viewMachineModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.View} {machine.Name}", $"/{Name}/{nameof(View)}/{machineId}/", false));

            return View(viewMachineModel);
        }

        [HttpGet]
        [Route("/Machines/Edit/{machineId}/")]
        public IActionResult Edit(long machineId)
        {
            IMachine machine = _gSendDataProvider.MachineGet(machineId);

            if (machine == null)
                return RedirectToAction(nameof(Index));

            EditMachineModel machineEditModel = CreateMachineEditModel(machine, IsMachineConnected(machineId, true));

            machineEditModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.View} {machine.Name}", $"/{Name}/{nameof(View)}/{machineId}/", false));
            machineEditModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.Edit}", $"/{Name}/{nameof(Edit)}/{machineId}", false));

            return View(machineEditModel);
        }

        [HttpPost]
        public IActionResult Edit(EditMachineModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            IMachine machine = _gSendDataProvider.MachineGet(model.Id);

            if (machine == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                if (_gSendDataProvider.MachinesGet().Any(m => m.Name == model.Name && m.Id != model.Id))
                    ModelState.AddModelError(String.Empty, GSend.Language.Resources.InvalidMachineNameUnique);
            }

            bool isConnected = IsMachineConnected(model.Id, true);

            if (String.IsNullOrEmpty(model.ComPort))
                ModelState.AddModelError(String.Empty, GSend.Language.Resources.ServerErrorInvalidComPort);

            if (!ModelState.IsValid)
            {
                EditMachineModel machineEditModel = CreateMachineEditModel(_gSendDataProvider.MachineGet(model.Id), isConnected);
                machineEditModel.Name = model.Name;
                machineEditModel.MachineFirmware = model.MachineFirmware;
                machineEditModel.MachineType = model.MachineType;
                machineEditModel.ComPort = model.ComPort;

                return View(machineEditModel);
            }

            machine.Name = model.Name;
            machine.ComPort = model.ComPort;
            machine.MachineFirmware = model.MachineFirmware;
            machine.MachineType = model.MachineType;

            _gSendDataProvider.MachineUpdate(machine);
            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineUpdated, machine.Id);

            return RedirectToAction(nameof(View), new { machineId = model.Id });
        }

        [HttpGet]
        [Breadcrumb(nameof(Add))]
        public IActionResult Add()
        {
            return View(CreateMachineAddModel());
        }

        [HttpPost]
        public IActionResult Add(EditMachineModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                if (_gSendDataProvider.MachinesGet().Any(m => m.Name == model.Name))
                    ModelState.AddModelError(String.Empty, GSend.Language.Resources.InvalidMachineNameUnique);
            }

            if (String.IsNullOrEmpty(model.ComPort))
                ModelState.AddModelError(String.Empty, GSend.Language.Resources.ServerErrorInvalidComPort);

            if (!ModelState.IsValid)
            {
                EditMachineModel machineEditModel = CreateMachineAddModel();
                machineEditModel.Name = model.Name;
                machineEditModel.MachineFirmware = model.MachineFirmware;
                machineEditModel.MachineType = model.MachineType;
                machineEditModel.ComPort = model.ComPort;

                return View(machineEditModel);
            }

            IMachine machine = new MachineModel()
            {
                Name = model.Name,
                ComPort = model.ComPort,
                MachineType = model.MachineType,
                MachineFirmware = model.MachineFirmware,
                OverrideSpeed = 40,
                OverrideSpindle = 5000,
                OverrideZDownSpeed = 300,
                OverrideZUpSpeed = 300,
                SoftStartSeconds = 30,
                ServiceSpindleHours = 200,
                ServiceWeeks = 20,
            };

            _gSendDataProvider.MachineAdd(machine);
            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineAdd, machine.Id);
            return RedirectToAction(nameof(View), new { machineId = machine.Id });
        }

        [HttpGet]
        [Route("/Machines/Delete/{machineId}/")]
        public IActionResult Delete(long machineId)
        {
            IMachine machine = _gSendDataProvider.MachineGet(machineId);

            if (machine == null)
                return RedirectToAction(nameof(Index));

            DeleteMachineModel machineDeleteModel = CreateMachineDeleteModel(machine, IsMachineConnected(machineId, false));

            machineDeleteModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.View} {machine.Name}", $"/{Name}/{nameof(View)}/{machineId}/", false));
            machineDeleteModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.Delete}", $"/{Name}/{nameof(Edit)}/{machineId}", false));

            return View(machineDeleteModel);
        }

        [HttpPost]
        public IActionResult Delete(DeleteMachineModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            IMachine machine = _gSendDataProvider.MachineGet(model.Id);

            if (machine == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!model.ConfirmDelete)
                ModelState.AddModelError(String.Empty, GSend.Language.Resources.MachineDeleteConfirm);

            if (ModelState.IsValid)
            {
                if (_gSendDataProvider.MachinesGet().Any(m => m.Name == model.Name && m.Id != model.Id))
                    ModelState.AddModelError(String.Empty, GSend.Language.Resources.InvalidMachineNameUnique);
            }

            bool isConnected = IsMachineConnected(model.Id, false);

            if (!ModelState.IsValid)
            {
                DeleteMachineModel machineDeleteModel = CreateMachineDeleteModel(_gSendDataProvider.MachineGet(model.Id), isConnected);
                machineDeleteModel.Name = model.Name;
                machineDeleteModel.MachineFirmware = model.MachineFirmware;
                machineDeleteModel.MachineType = model.MachineType;
                machineDeleteModel.ComPort = model.ComPort;

                machineDeleteModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.View} {machine.Name}", $"/{Name}/{nameof(View)}/{model.Id}/", false));
                machineDeleteModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.Delete}", $"/{Name}/{nameof(Edit)}/{model.Id}", false));

                return View(machineDeleteModel);
            }

            _gSendDataProvider.MachineRemove(model.Id);
            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineUpdated, machine.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("/Machines/ServiceScheduleAdd/{machineId}/")]
        public IActionResult ServiceScheduleAdd(long machineId)
        {
            IMachine machine = _gSendDataProvider.MachineGet(machineId);

            if (machine == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (machine is MachineModel machineModel)
            {
                machineModel.AddOptions(MachineOptions.ServiceSchedule);
                _gSendDataProvider.MachineUpdate(machineModel);
            }

            return RedirectToAction(nameof(ConfigureService), machineId);
        }

        [HttpGet]
        [Route("/Machines/ConfigureService/{machineId}/")]
        public IActionResult ConfigureService(long machineId)
        {
            IMachine machine = _gSendDataProvider.MachineGet(machineId);

            if (machine == null || !machine.Options.HasFlag(MachineOptions.ServiceSchedule))
            {
                return RedirectToAction(nameof(Index));
            }

            ServiceConfigurationMachineModel model = new ServiceConfigurationMachineModel(GetModelData(), machine);
            model.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.View} {machine.Name}", $"/{Name}/{nameof(View)}/{machineId}/", false));
            model.Breadcrumbs.Add(new BreadcrumbItem(GSend.Language.Resources.ServiceSchedule, $"/{Name}/{nameof(ConfigureService)}/{machineId}/", false));

            return View(model);
        }

        [HttpPost]
        public IActionResult ConfigureService(ServiceConfigurationMachineModel model)
        {
            if (model == null)
                return RedirectToAction(nameof(Index));

            IMachine machine = _gSendDataProvider.MachineGet(model.MachineId);

            if (machine == null || !machine.Options.HasFlag(MachineOptions.ServiceSchedule))
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                machine.ServiceSpindleHours = model.SpindleHours;
                machine.ServiceWeeks = model.ServiceWeeks;
                _gSendDataProvider.MachineUpdate(machine);
                return RedirectToAction(nameof(View), new { machineId = model.MachineId });
            }

            return View(model);
        }

        [HttpGet]
        [Route("/Machines/Service/{machineId}/")]
        public IActionResult Service(long machineId)
        {
            IMachine machine = _gSendDataProvider.MachineGet(machineId);

            if (machine == null || !machine.Options.HasFlag(MachineOptions.ServiceSchedule))
            {
                return RedirectToAction(nameof(Index));
            }

            List<ServiceItemModel> serviceItems = _gSendDataProvider.ServiceItemsGet(machine.MachineType);

            ServiceMachineModel returnModel = new ServiceMachineModel(GetModelData(), machine, GSendShared.ServiceType.Daily, serviceItems);
            returnModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.View} {machine.Name}", $"/{Name}/{nameof(View)}/{machineId}/", false));
            returnModel.Breadcrumbs.Add(new BreadcrumbItem(GSend.Language.Resources.ServiceNow, $"/{Name}/{nameof(Service)}/{machineId}/", false));
            return View(returnModel);
        }

        [HttpPost]
        [Route("/Machines/Service/{machineId}/")]
        public IActionResult Service(ServiceMachineModel model)
        {
            IMachine machine = _gSendDataProvider.MachineGet(model.MachineId);

            if (machine == null || !machine.Options.HasFlag(MachineOptions.ServiceSchedule))
            {
                return RedirectToAction(nameof(Index));
            }

            List<ServiceItemModel> allServiceItems = _gSendDataProvider.ServiceItemsGet(machine.MachineType);

            Dictionary<long, string> serviceItems = new();

            foreach (var serviceItem in allServiceItems)
            {
                if (model.ServiceType == GSendShared.ServiceType.Daily && serviceItem.IsDaily ||
                    model.ServiceType == GSendShared.ServiceType.Minor && serviceItem.IsMinor ||
                    model.ServiceType == GSendShared.ServiceType.Major && serviceItem.IsMajor)
                {
                    serviceItems.Add(serviceItem.Id, serviceItem.Name);
                }
            }

            //machine.ServiceSpindleHours * TimeSpan.TicksPerHour) - ticks

            RetreiveServiceData(machine, out DateTime _, out TimeSpan _, out long spindleTicks);

            _gSendDataProvider.ServiceAdd(new MachineServiceModel(-1, model.MachineId, DateTime.UtcNow,
                model.ServiceType, spindleTicks / TimeSpan.TicksPerHour, serviceItems));
            
            return RedirectToAction(nameof(Index));
        }

        #region Private Methods

        private bool IsMachineConnected(long machineId, bool isUpdate)
        {
            object connectedState = false;
            bool isConnected = true;

            if (_notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineConnected, machineId, null, ref connectedState))
            {
                isConnected = Convert.ToBoolean(connectedState);
            }

            if (isConnected)
            {
                if (isUpdate)
                    ModelState.AddModelError(String.Empty, GSend.Language.Resources.UpdateConnectedMachine);
                else
                    ModelState.AddModelError(String.Empty, GSend.Language.Resources.DeleteConnectedMachine);
            }

            return isConnected;
        }

        private ViewMachineModel CreateMachineViewModel(IMachine machine)
        {
            DateTime nextService;
            TimeSpan remainingSpindle;
            RetreiveServiceData(machine, out nextService, out remainingSpindle, out long _);

            MachineServiceViewModel serviceModel = new MachineServiceViewModel(machine.Id,
                machine.Options.HasFlag(MachineOptions.ServiceSchedule),
                machine.ServiceWeeks, machine.ServiceSpindleHours, nextService, remainingSpindle);

            return new ViewMachineModel(GetModelData(), machine.Id, machine.Name, machine.MachineType, machine.ComPort, serviceModel);
        }

        private void RetreiveServiceData(IMachine machine, out DateTime nextService, out TimeSpan remainingSpindle, out long spindleTicks)
        {
            IReadOnlyList<MachineServiceModel> services = _gSendDataProvider.ServicesGet(machine.Id);
            nextService = DateTime.MinValue;
            remainingSpindle = TimeSpan.Zero;
            spindleTicks = 0;

            if (services.Count > 0)
            {
                DateTime latestService = services.Max(s => s.ServiceDate);
                nextService = latestService.AddDays(machine.ServiceWeeks * 7);

                IReadOnlyList<ISpindleTime> spindleHours = _gSendDataProvider.SpindleTimeGet(machine.Id).Where(m =>
                    m.StartTime >= latestService && m.FinishTime > DateTime.MinValue).ToList();

                spindleTicks = 0;

                foreach (ISpindleTime spindleTime in spindleHours)
                {
                    if (spindleTime.FinishTime > spindleTime.StartTime)
                        spindleTicks += ((TimeSpan)(spindleTime.FinishTime - spindleTime.StartTime)).Ticks;
                }

                remainingSpindle = new((machine.ServiceSpindleHours * TimeSpan.TicksPerHour) - spindleTicks);
            }
        }

        private EditMachineModel CreateMachineEditModel(IMachine machine, bool isConnected)
        {
            return new EditMachineModel(GetModelData(), machine.Id, machine.Name, machine.MachineType, machine.MachineFirmware,
                machine.ComPort, _comPortProvider.AvailablePorts(), isConnected);
        }

        private DeleteMachineModel CreateMachineDeleteModel(IMachine machine, bool isConnected)
        {
            return new DeleteMachineModel(GetModelData(), machine.Id, machine.Name, machine.MachineType, machine.MachineFirmware,
                machine.ComPort, isConnected);
        }

        private EditMachineModel CreateMachineAddModel()
        {
            return new EditMachineModel(GetModelData(), _comPortProvider.AvailablePorts());
        }

        #endregion Private Methods
    }
}
