namespace GSendApi
{
    public sealed class ApiSettings
    {
        public ApiSettings(Uri rootAddress)
        {
            if (rootAddress == null)
                throw new ArgumentNullException(nameof(rootAddress));

            if (!rootAddress.IsAbsoluteUri)
                throw new ArgumentOutOfRangeException(nameof(rootAddress));

            RootAddress = rootAddress;
        }

        public Uri RootAddress { get; set; }

        public string ApiVersion { get; set; } = "1.0.0";

        public int Timeout { get; set; } = 1000;
    }
}
