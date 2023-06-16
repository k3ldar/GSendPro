using GSendCommon.Settings;
using GSendShared;
using GSendShared.Abstractions;

using Microsoft.AspNetCore.Mvc;

using PluginManager.Abstractions;

using SharedPluginFeatures;

using static SharedPluginFeatures.Constants;

using MachineModel = GSendShared.Models.MachineModel;

namespace GSendService.Api
{
    public class MachineApi : BaseController
    {
        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly IComPortProvider _comPortProvider;
        private readonly INotificationService _notificationService;
        private readonly GSendSettings _settings;

        public MachineApi(IGSendDataProvider gSendDataProvider, IComPortProvider comPortProvider, ISettingsProvider settingsProvider, INotificationService notificationService)
        {
            _settings = settingsProvider.GetSettings<GSendSettings>(GSendShared.Constants.SettingsName);
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _comPortProvider = comPortProvider ?? throw new ArgumentNullException(nameof(comPortProvider));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        [HttpGet]
        public IActionResult MachinesGet()
        {
            return GenerateJsonSuccessResponse(_gSendDataProvider.MachinesGet());
        }

        [HttpGet]
        [Route("/MachineApi/MachineExists/{name}/")]
        public IActionResult MachineExists(string name)
        {
            bool exists = _gSendDataProvider.MachinesGet().Any(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return GenerateJsonSuccessResponse(exists);
        }

        [HttpPost]
        public IActionResult MachineAdd([FromBody] IMachine model)
        {
            if (!ValidateMachineModel(model, true, out string errorData))
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, errorData);

            _gSendDataProvider.MachineAdd(model);

            if (_gSendDataProvider.MachinesGet().FirstOrDefault(m => m.Name.Equals(model.Name, StringComparison.InvariantCultureIgnoreCase)) == null)
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Error adding machine");

            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineAdd, model.Id);

            return GenerateJsonSuccessResponse();
        }

        [HttpDelete]
        public IActionResult MachineDelete(long machineId)
        {
            IMachine deleteMachine = _gSendDataProvider.MachineGet(machineId);

            if (deleteMachine == null)
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Machine not found");

            _gSendDataProvider.MachineRemove(deleteMachine.Id);

            if (_gSendDataProvider.MachinesGet().FirstOrDefault(m => m.Id.Equals(machineId)) != null)
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, "Error removing machine");

            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineRemove, deleteMachine.Id);

            return GenerateJsonSuccessResponse();
        }

        [HttpPut]
        public IActionResult MachineUpdate([FromBody] IMachine model)
        {
            if (!ValidateMachineModel(model, false, out string errorData))
                return GenerateJsonErrorResponse(HtmlResponseBadRequest, errorData);

            _gSendDataProvider.MachineUpdate(model);

            _notificationService.RaiseEvent(GSendShared.Constants.NotificationMachineUpdated, model.Id);

            return GenerateJsonSuccessResponse();
        }

        private bool ValidateMachineModel(IMachine model, bool validateComPort, out string errorMessage)
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

            if (validateComPort && !_comPortProvider.AvailablePorts().Any(ap => ap.Equals(model.ComPort)))
            {
                errorMessage = "Invalid com port not found";
                return false;
            }

            IReadOnlyList<IMachine> machines = _gSendDataProvider.MachinesGet();

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
