﻿using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using GSendShared.Providers.Internal.Enc;

using SharedPluginFeatures;

namespace GSendApi
{
    public class BaseApiWrapper
    {
        #region Private Members

        private Uri _serverAddress;
        private readonly ApiSettings _apiSettings;
        private readonly string _merchantId;
        private readonly string _apiKey;
        private readonly string _secret;
        private readonly TimeSpan _timeout;

        #endregion Private Members

        #region Constructors

        private BaseApiWrapper()
        {
#if DEBUG
            _timeout = TimeSpan.FromMinutes(5);
#else
            _timeout = TimeSpan.FromMilliseconds(_apiSettings.Timeout);
#endif
        }

        protected BaseApiWrapper(ApiSettings apiSettings)
            : this()
        {
            _apiSettings = apiSettings ?? throw new ArgumentNullException(nameof(apiSettings));
            ServerAddress = _apiSettings.RootAddress;
            string apiFile = Path.Combine(Environment.GetEnvironmentVariable(GSendShared.Constants.GSendPathEnvVar), "api.dat");

            if (!File.Exists(apiFile))
                throw new InvalidOperationException("Unable to find Api Data");

            string decrypted = AesImpl.Decrypt(File.ReadAllText(apiFile), Convert.FromBase64String(Environment.GetEnvironmentVariable("gsp")));
            string[] parts = decrypted.Split("#", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
                throw new InvalidOperationException("Invalid Api data");

            _merchantId = parts[0];
            _apiKey = parts[1];
            _secret = parts[2];
        }

        #endregion Constructors

        #region Properties

        private sealed class JsonResponseModel
        {
            public bool success { get; set; }

            public string responseData { get; set; }
        }

        public Uri ServerAddress
        {
            get => _serverAddress;

            set
            {
                _serverAddress = value;
                ServerUriChanged?.Invoke();
            }
        }


        #endregion Properties

        #region Events

        public event Action ServerUriChanged;

        #endregion Events

        #region Protected Members

        protected HttpClient CreateApiClient(TimeSpan? timeout = null)
        {
            HttpClient httpClient = new();

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("GSend", _apiSettings.ApiVersion));
            httpClient.Timeout = timeout ?? _timeout;

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

        protected static HttpContent CreateContent<T>(T data)
        {
            byte[] content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, GSendShared.Constants.DefaultJsonSerializerOptions));
            HttpContent Result = new ByteArrayContent(content);
            Result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return Result;
        }

        protected void CallPostApi<T>(string endPoint, T data)
        {
            HttpContent content = CreateContent(data);
            using HttpClient httpClient = CreateApiClient();
            string address = $"{ServerAddress}{endPoint}";

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

        protected T CallPostApi<T>(string endPoint)
        {
            HttpContent content = CreateContent(String.Empty);
            using HttpClient httpClient = CreateApiClient();
            string address = $"{ServerAddress}{endPoint}";

            using HttpResponseMessage response = httpClient.PostAsync(address, content).Result;

            if (!response.IsSuccessStatusCode)
                throw new GSendApiException(String.Format(GSend.Language.Resources.InvalidApiResponse, endPoint, nameof(CallPostApi)));

            string jsonData = response.Content.ReadAsStringAsync().Result;
            JsonResponseModel responseModel = (JsonResponseModel)JsonSerializer.Deserialize(jsonData, typeof(JsonResponseModel), GSendShared.Constants.DefaultJsonSerializerOptions);

            if (responseModel.success)
            {
                return JsonSerializer.Deserialize<T>(responseModel.responseData, GSendShared.Constants.DefaultJsonSerializerOptions);
            }

            throw new GSendApiException(responseModel.responseData);
        }

        protected T CallGetApi<T>(Uri server, string endPoint, TimeSpan? timeout = null)
        {
            try
            {
                using HttpClient httpClient = CreateApiClient(timeout);
                string address = $"{server}{endPoint}";

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

        protected T CallGetApi<T>(string endPoint, TimeSpan? timeout = null)
        {
            try
            {
                using HttpClient httpClient = CreateApiClient(timeout);
                string address = $"{ServerAddress}{endPoint}";

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

        protected void CallPutApi<T>(string endPoint, T data)
        {
            using HttpClient httpClient = CreateApiClient();
            string address = $"{ServerAddress}{endPoint}";

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

        protected bool CallDeleteApi(string endPoint)
        {
            using HttpClient httpClient = CreateApiClient();
            string address = $"{ServerAddress}{endPoint}";

            using HttpResponseMessage response = httpClient.DeleteAsync(address).Result;

            if (!response.IsSuccessStatusCode)
                throw new GSendApiException(String.Format(GSend.Language.Resources.InvalidApiResponse, endPoint, nameof(CallDeleteApi)));

            string jsonData = response.Content.ReadAsStringAsync().Result;
            JsonResponseModel responseModel = (JsonResponseModel)JsonSerializer.Deserialize(jsonData, typeof(JsonResponseModel), GSendShared.Constants.DefaultJsonSerializerOptions);

            return responseModel.success;
        }

        #endregion Protected Members
    }
}
