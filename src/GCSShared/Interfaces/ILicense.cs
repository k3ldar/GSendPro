namespace GSendShared.Abstractions
{
    public interface ILicense
    {
        /// <summary>
        /// Determines whether a license option is available or not
        /// </summary>
        /// <param name="licenseOption">License option being queried</param>
        /// <returns>bool</returns>
        bool OptionAvailable(in string licenseOption);

        string OptionValue(in string licenseOption);

        /// <summary>
        /// Date/time license expires
        /// </summary>
        DateTime Expires { get; }

        /// <summary>
        /// Registered user or organisation
        /// </summary>
        string RegisteredUser { get; }

        string ClientId { get; }

        bool IsValid { get; }
    }
}
