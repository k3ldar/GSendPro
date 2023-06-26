using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using GSendShared;
using GSendShared.Models;
using GSendShared.Providers.Internal.Enc;

using SharedPluginFeatures;

namespace GSendApi
{
    public class GSendApiWrapper : IGSendApiWrapper
    {
        private static readonly byte[] key = new byte[] { 239, 191, 189, 86, 239, 191, 107, 33, 239, 191, 189, 239, 189, 92, 8, 35, 93, 107, 50, 239, 19, 239, 189, 239, 191, 189, 239, 189, 239, 34, 239, 189 };

        #region Private Members

        private class JsonResponseModel
        {
            public bool success { get; set; }
            public string responseData { get; set; }
        }

        private readonly ApiSettings _apiSettings;
        private readonly string _merchantId;
        private readonly string _apiKey;
        private readonly string _secret;

        #endregion Private Members

        #region Constructors

        public GSendApiWrapper()
        {
            string apiFile = Path.Combine(Environment.GetEnvironmentVariable("GSendProRootPath"), "api.dat");

            if (!File.Exists(apiFile))
                throw new InvalidOperationException("Unable to find Api Data");

            string decrypted = AesImpl.Decrypt(File.ReadAllText(apiFile), key);
            string[] parts = decrypted.Split("#", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
                throw new InvalidOperationException("Invalid Api data");

            _merchantId = parts[0];
            _apiKey = parts[1];
            _secret = parts[2];
        }

        public GSendApiWrapper(ApiSettings apiSettings)
            : this()
        {
            _apiSettings = apiSettings ?? throw new ArgumentNullException(nameof(apiSettings));
        }

        #endregion Constructors

        public Uri ServerAddress => _apiSettings.RootAddress;

        #region Machines

        public List<IMachine> MachinesGet()
        {
            return CallGetApi<List<IMachine>>("MachineApi/MachinesGet");
        }

        public void MachineAdd(IMachine machine)
        {
            CallPostApi("MachineApi/MachineAdd", machine);
        }

        public void MachineUpdate(IMachine machine)
        {
            CallPutApi("MachineApi/MachineUpdate", machine);
        }

        public bool MachineNameExists(string name)
        {
            return CallGetApi<bool>($"MachineApi/MachineExists/{name}");
        }

        #endregion Machines

        #region Services

        public List<MachineServiceModel> MachineServices(long machineId)
        {
            return CallGetApi<List<MachineServiceModel>>($"ServiceApi/ServicesGet/{machineId}");
        }

        public void MachineServiceAdd(long machineId, DateTime serviceDate, ServiceType serviceType, long spindleHours)
        {
            MachineServiceModel machineServiceModel = new()
            {
                MachineId = machineId,
                ServiceDate = serviceDate,
                ServiceType = serviceType,
                SpindleHours = spindleHours
            };

            CallPostApi("ServiceApi/ServiceAdd", machineServiceModel);
        }

        #endregion Services

        #region Spindle Time

        public List<SpindleHoursModel> GetSpindleTime(long machineId, DateTime fromDate)
        {
            return CallGetApi<List<SpindleHoursModel>>($"SpindleHoursApi/SpindleHoursGet/{machineId}/{fromDate.Ticks}/");
        }

        #endregion Spindle Time

        #region Job Profiles

        public List<IJobProfile> JobProfilesGet()
        {
            return CallGetApi<List<IJobProfile>>($"JobProfileApi/JobProfilesGet/");
        }

        #endregion Job Profiles

        #region Tool Profiles

        public List<IToolProfile> ToolProfilesGet()
        {
            return CallGetApi<List<IToolProfile>>($"ToolProfileApi/ToolsGet/");
        }

        #endregion Tool Profiles

        #region ILicense

        public bool IsLicenseValid()
        {
            return CallGetApi<bool>($"LicenseApi/IsLicensed/");
        }

        #endregion ILicense

        #region Subprograms

        public List<ISubprogram> SubprogramGet()
        {
            return CallGetApi<List<ISubprogram>>("SubprogramApi/GetAllSubprograms/");
        }

        public ISubprogram SubprogramGet(string name)
        {
            return CallGetApi<ISubprogram>($"SubprogramApi/SubprogramGet/{name}");
        }

        public bool SubprogramExists(string name)
        {
            return CallGetApi<bool>($"SubprogramApi/SubprogramExists/{name}");
        }

        public bool SubprogramDelete(string name)
        {
            return CallDeleteApi($"SubprogramApi/SubprogramDelete/{name}");
        }

        public bool SubprogramUpdate(ISubprogram subProgram)
        {
            CallPutApi<ISubprogram>($"SubprogramApi/SubprogramUpdate/", subProgram);
            return true;
        }

        #endregion Subprograms

        #region Private Methods

        private HttpClient CreateApiClient()
        {
            HttpClient httpClient = new();

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("GSend", _apiSettings.ApiVersion));

#if DEBUG
            httpClient.Timeout = TimeSpan.FromMinutes(5);
#else
     
            httpClient.Timeout = TimeSpan.FromMilliseconds(_apiSettings.Timeout);
#endif

            ulong nonce = (ulong)DateTime.UtcNow.Ticks;
            long timestamp = HmacGenerator.EpochDateTime();
            string auth = HmacGenerator.GenerateHmac(_apiKey, _secret, timestamp, nonce, _merchantId, String.Empty);

            httpClient.DefaultRequestHeaders.Add("apikey", _apiKey);
            httpClient.DefaultRequestHeaders.Add("merchantId", _merchantId);
            httpClient.DefaultRequestHeaders.Add("nonce", nonce.ToString());
            httpClient.DefaultRequestHeaders.Add("timestamp", timestamp.ToString());
            httpClient.DefaultRequestHeaders.Add("authcode", auth);
            httpClient.DefaultRequestHeaders.Add("payloadLength", "0");


            return httpClient;
        }

        private HttpContent CreateContent<T>(T data)
        {
            byte[] content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, GSendShared.Constants.DefaultJsonSerializerOptions));
            HttpContent Result = new ByteArrayContent(content);
            Result.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //ulong nonce = (ulong)DateTime.UtcNow.Ticks;
            //long timestamp = HmacGenerator.EpochDateTime();
            //string auth = HmacGenerator.GenerateHmac(_apiKey, _secret, timestamp, nonce, _merchantId, String.Empty);

            //Result.Headers.Add("apikey", _apiKey);
            //Result.Headers.Add("merchantId", _merchantId);
            //Result.Headers.Add("nonce", nonce.ToString());
            //Result.Headers.Add("timestamp", timestamp.ToString());
            //Result.Headers.Add("authcode", auth);
            //Result.Headers.Add("payloadLength", "0");

            return Result;
        }

        private void CallPostApi<T>(string endPoint, T data)
        {
            HttpContent content = CreateContent(data);
            using HttpClient httpClient = CreateApiClient();
            string address = $"{_apiSettings.RootAddress}{endPoint}";

            using HttpResponseMessage response = httpClient.PostAsync(address, content).Result;

            if (!response.IsSuccessStatusCode)
                throw new GSendApiException(String.Format(GSend.Language.Resources.InvalidApiResponse, endPoint, nameof(CallPostApi)));

            string jsonData = response.Content.ReadAsStringAsync().Result;
            JsonResponseModel responseModel = (JsonResponseModel)JsonSerializer.Deserialize(jsonData, typeof(JsonResponseModel), GSendShared.Constants.DefaultJsonSerializerOptions);

            if (responseModel.success)
            {
                return;
            }

            throw new GSendApiException(responseModel.responseData);
        }

        private T CallGetApi<T>(string endPoint)
        {
            try
            {
                using HttpClient httpClient = CreateApiClient();
                string address = $"{_apiSettings.RootAddress}{endPoint}";

                using HttpResponseMessage response = httpClient.GetAsync(address).Result;

                if (!response.IsSuccessStatusCode)
                    throw new GSendApiException(String.Format(GSend.Language.Resources.InvalidApiResponse, endPoint, nameof(CallGetApi)));

                string jsonData = response.Content.ReadAsStringAsync().Result;

                if (String.IsNullOrWhiteSpace(jsonData))
                    return default;

                JsonResponseModel responseModel = (JsonResponseModel)JsonSerializer.Deserialize(jsonData, typeof(JsonResponseModel), GSendShared.Constants.DefaultJsonSerializerOptions);

                if (responseModel.success)
                {
                    return JsonSerializer.Deserialize<T>(responseModel.responseData, GSendShared.Constants.DefaultJsonSerializerOptions);
                }

                throw new GSendApiException(responseModel.responseData);
            }
            catch (Exception ex) when
                (ex is AggregateException)
            {
                throw new GSendApiException(GSend.Language.Resources.UnableToContactServer, ex);
            }
        }

        private void CallPutApi<T>(string endPoint, T data)
        {
            using HttpClient httpClient = CreateApiClient();
            string address = $"{_apiSettings.RootAddress}{endPoint}";

            HttpContent content = CreateContent(data);

            using HttpResponseMessage response = httpClient.PutAsync(address, content).Result;

            if (!response.IsSuccessStatusCode)
                throw new GSendApiException(String.Format(GSend.Language.Resources.InvalidApiResponse, endPoint, nameof(CallPutApi)));

            string jsonData = response.Content.ReadAsStringAsync().Result;


            JsonResponseModel responseModel = (JsonResponseModel)JsonSerializer.Deserialize(jsonData, typeof(JsonResponseModel), GSendShared.Constants.DefaultJsonSerializerOptions);

            if (responseModel.success)
            {
                return;
            }

            throw new GSendApiException(responseModel.responseData);
        }

        private bool CallDeleteApi(string endPoint)
        {
            using HttpClient httpClient = CreateApiClient();
            string address = $"{_apiSettings.RootAddress}{endPoint}";

            using HttpResponseMessage response = httpClient.DeleteAsync(address).Result;

            if (!response.IsSuccessStatusCode)
                throw new GSendApiException(String.Format(GSend.Language.Resources.InvalidApiResponse, endPoint, nameof(CallDeleteApi)));

            string jsonData = response.Content.ReadAsStringAsync().Result;
            JsonResponseModel responseModel = (JsonResponseModel)JsonSerializer.Deserialize(jsonData, typeof(JsonResponseModel), GSendShared.Constants.DefaultJsonSerializerOptions);

            return responseModel.success;
        }

        #endregion Private Methods
    }
}