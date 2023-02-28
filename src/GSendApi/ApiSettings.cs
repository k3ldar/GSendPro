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

        public Uri RootAddress { get; }

        public string ApiVersion { get; } = "1.0.0";

        public int Timeout { get; } = 1000;
    }
}
