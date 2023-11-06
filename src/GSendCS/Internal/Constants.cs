namespace GSendCS.Internal
{
    internal static class Constants
    {
        public const string PluginFileName = "Plugins.json";
        public const string ServerAddressFileName = "Servers.json";

        public const int ResponseSuccess = 0;
        public const int ResponseExclusiveAccessDenied = -100;
        public const int ResponseExists = -101;
        public const int ResponseDoesNotExist = -102;
        public const int ResponseInvalidAddress = -103;
        public const int ResponseNoAddressesFound = -104;
    }
}
