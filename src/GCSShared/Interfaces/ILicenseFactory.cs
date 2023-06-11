namespace GSendShared.Abstractions
{
    public interface ILicenseFactory
    {
        /// <summary>
        /// Loads license details in form of a byte array
        /// </summary>
        /// <param name="license"></param>
        /// <returns><see cref="ILicense"/> instance</returns>
        ILicense LoadLicense(in string license);

        /// <summary>
        /// Retrieves license details in the form of a string
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        string SaveLicense(in ILicense license);

        /// <summary>
        /// Retrieves the currently active license for the application
        /// </summary>
        /// <returns><see cref="ILicense"/>ILicense instance</returns>
        ILicense GetActiveLicense();

        /// <summary>
        /// Sets the currently active license
        /// </summary>
        /// <param name="license"></param>
        void SetActiveLicense(in ILicense license);

        /// <summary>
        /// Determines whether the license is valid or not
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        bool LicenseIsValid(in ILicense license);
    }
}
