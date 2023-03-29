using System.Reflection.PortableExecutable;

using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Models;

using Microsoft.AspNetCore.Mvc;

using SharedPluginFeatures;

using MachineModel = GSendShared.Models.MachineModel;
using static SharedPluginFeatures.Constants;
using System.Reflection;
using PluginManager.Abstractions;
using GSendCommon;

namespace GSendService.Api
{
    public class MachineApi : BaseController
    {
        private readonly IMachineProvider _machineProvider;
        private readonly IComPortProvider _comPortProvider;
        private readonly INotificationService _notificationService;
        private readonly GSendSettings _settings;

        public MachineApi(IMachineProvider machineProvider, IComPortProvider comPortProvider, ISettingsProvider settingsProvider, INotificationService notificationService)
        {
            _settings = settingsProvider.GetSettings<GSendSettings>(GSendShared.Constants.SettingsName);
            _machineProvider = machineProvider ?? throw new ArgumentNullException(nameof(machineProvider));
            _comPortProvider = comPortProvider ?? throw new ArgumentNullException(nameof(comPortProvider));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        [HttpGet]
        public IActionResult MachinesGet()
        {
            return GenerateJsonSuccessResponse(_machineProvider.MachinesGet());
        }

        [HttpPost]
        public IActionResult MachineAdd([FromBody]MachineModel model)
        {
            if (!ValidateMachineModel(model, out string errorData))
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, errorData);

            _machineProvider.MachineAdd(model);

            if (_machineProvider.MachinesGet().FirstOrDefault(m => m.Name.Equals(model.Name)) == null)
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Error adding machine");

            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineAdd, model.Id);

            return GenerateJsonSuccessResponse();
        }

        [HttpDelete]
        public IActionResult MachineDelete(long machineId)
        {
            IMachine deleteMachine = _machineProvider.MachineGet(machineId);

            if (deleteMachine == null)
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Machine not found");

            _machineProvider.MachineRemove(deleteMachine.Id);

            if (_machineProvider.MachinesGet().FirstOrDefault(m => m.Id.Equals(machineId)) != null)
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Error removing machine");

            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineRemove, deleteMachine.Id);

            return GenerateJsonSuccessResponse();
        }

        [HttpPut]
        public IActionResult MachineUpdate([FromBody]MachineModel model)
        {
            if (!ValidateMachineModel(model, out string errorData))
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, errorData);

            _machineProvider.MachineUpdate(model);

            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineUpdated, model.Id);
            
            return GenerateJsonSuccessResponse();
        }

        private bool ValidateMachineModel(IMachine model, out string errorMessage)
        {
            if (model == null)
            {
                errorMessage = "Invalid machine model";
                return false;
            }

            if (String.IsNullOrWhiteSpace(model.Name))
            {
                errorMessage = "Invalid name";
                return false;
            }

            if (model.MachineType.Equals(MachineType.Unspecified))
            {
                errorMessage = "Invalid machine type";
                return false;
            }

            if (!_comPortProvider.AvailablePorts().Any(ap => ap.Equals(model.ComPort)))
            {
                errorMessage = "Invalid com port not found";
                return false;
            }

            IReadOnlyList<IMachine> machines = _machineProvider.MachinesGet();

            if (machines.Any(m => !m.Id.Equals(model.Id) && m.Name.Equals(model.Name)))
            {
                errorMessage = "Name already exists";
                return false;
            }

            if (!_settings.AllowDuplicateComPorts && machines.Any(m => m.ComPort.Equals(model.ComPort) && !m.Id.Equals(model.Id)))
            {
                errorMessage = "Invalid com port duplicate";
                return false;
            }

            errorMessage = String.Empty;
            return true;
        }
    }
}
