using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

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

        public MachinesController(IGSendDataProvider gSendDataProvider, IComPortProvider comPortProvider, INotificationService notificationService)
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
                RedirectToAction(nameof(Index));

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
                RedirectToAction(nameof(Index));

            EditMachineModel machineEditModel = CreateMachineEditModel(machine, IsMachineConnected(machineId));

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

            bool isConnected = IsMachineConnected(model.Id);

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
        //[Breadcrumb()]
        public IActionResult Add()
        {
            EditMachineModel machineEditModel = CreateMachineAddModel();

            //machineEditModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.View} {machine.Name}", $"/{Name}/{nameof(View)}/{machineId}/", false));
            //machineEditModel.Breadcrumbs.Add(new BreadcrumbItem($"{Languages.LanguageStrings.Edit}", $"/{Name}/{nameof(Edit)}/{machineId}", false));

            return View(machineEditModel);
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
            };
            
            _gSendDataProvider.MachineAdd(machine);
            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineAdd, machine.Id);
            return RedirectToAction(nameof(View), new { machineId = machine.Id });
        }


        private bool IsMachineConnected(long machineId)
        {
            object connectedState = false;
            bool isConnected = true;

            if (_notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineConnected, machineId, null, ref connectedState))
            {
                isConnected = Convert.ToBoolean(connectedState);
            }

            if (isConnected)
            {
                ModelState.AddModelError(String.Empty, GSend.Language.Resources.UpdateConnectedMachine);
            }

            return isConnected;
        }

        private ViewMachineModel CreateMachineViewModel(IMachine machine)
        {
            return new ViewMachineModel(GetModelData(), machine.Id, machine.Name, machine.MachineType, machine.ComPort)
            {

            };
        }

        private EditMachineModel CreateMachineEditModel(IMachine machine, bool isConnected)
        {
            return new EditMachineModel(GetModelData(), machine.Id, machine.Name, machine.MachineType, machine.MachineFirmware, 
                machine.ComPort, _comPortProvider.AvailablePorts(), isConnected)
            {

            };
        }
        private EditMachineModel CreateMachineAddModel()
        {
            return new EditMachineModel(GetModelData(), _comPortProvider.AvailablePorts())
            {

            };
        }
    }
}
