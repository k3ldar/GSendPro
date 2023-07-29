using System;
using System.IO;

using AppSettings;

using GSendShared;

using Microsoft.Extensions.Configuration;

using PluginManager.Abstractions;

namespace GSendService.Internal
{
    public class DefaultSettingProvider : ISettingsProvider
    {
        #region Private Members

        private readonly string _rootPath;
        private readonly ISettingOverride _settingOverride;
        private readonly IApplicationOverride _appOverride;
        private readonly ISettingError _settingsError;

        #endregion Private Members

        #region Constructors

        public DefaultSettingProvider()
            : this(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Constants.GSendProAppFolder), new SettingOverride(), new ApplicationOverride(), new SettingsError())
        {

        }

        public DefaultSettingProvider(in string rootPath, ISettingOverride settingOverride, IApplicationOverride appOverride, ISettingError settingsError)
        {
            if (String.IsNullOrEmpty(rootPath))
                throw new ArgumentNullException(nameof(rootPath));

            _settingOverride = settingOverride;
            _appOverride = appOverride;
            _settingsError = settingsError;
            _rootPath = rootPath;
        }

        #endregion Constructors

        #region ISettingsProvider Methods

        public T GetSettings<T>(in string storage, in string sectionName)
        {
            if (String.IsNullOrEmpty(storage))
                throw new ArgumentNullException(nameof(storage));

            if (String.IsNullOrEmpty(sectionName))
                throw new ArgumentNullException(nameof(sectionName));

            ConfigurationBuilder builder = new();
            IConfigurationBuilder configBuilder = builder.SetBasePath(_rootPath);
            configBuilder.AddJsonFile(storage);
            IConfigurationRoot config = builder.Build();
            T Result = (T)Activator.CreateInstance(typeof(T));
            config.GetSection(sectionName).Bind(Result);

            return ValidateSettings<T>.Validate(Result, _settingOverride, _settingsError, _appOverride);
        }

        public T GetSettings<T>(in string sectionName)
        {
            return GetSettings<T>(GSendShared.Constants.AppSettings, sectionName);
        }

        #endregion ISettingsProvider Methods
    }
}
