using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GSendShared
{
    public sealed class Constants
    {
        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new();

        public const string SettingsName = "GSend";

        public const string NotificationMachineAdd = "MachineAdd";

        public const string NotificationMachineRemove = "MachineRemove";

        public const string NotificationMachineUpdated = "MachineUpdate";
    }
}
