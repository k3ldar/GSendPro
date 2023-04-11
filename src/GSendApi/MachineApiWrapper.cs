using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using GSendShared;
using GSendShared.Models;

namespace GSendApi
{
    public sealed class MachineApiWrapper
    {
        #region Private Members

        private class JsonResponseModel
        {
            public bool success { get; set; }
            public string responseData { get; set; }
        }

        private readonly ApiSettings _apiSettings;

        #endregion Private Members

        #region Constructors

        public MachineApiWrapper(ApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
        }

        #endregion Constructors

        #region Machines

        public List<IMachine> MachinesGet()
        {
            return CallGetApi<List<IMachine>>("MachineApi/MachinesGet");
        }

        public void MachineAdd(IMachine machine)
        {
            CallPostApi("MachineApi/MachineAdd", machine);
        }

        //public void MachineRemove(IMachine machine)
        //{

        //}

        public void MachineUpdate(IMachine machine)
        {
            CallPutApi("MachineApi/MachineUpdate", machine);
        }

        #endregion Machines

        #region Services

        public List<DateTime> MachineServices(long machineId)
        {
            return CallGetApi<List<DateTime>>($"ServiceApi/ServicesGet/{machineId}");
        }

        public void MachineServiceAdd(long machineId, DateTime serviceDate)
        {
            MachineServiceModel machineServiceModel = new()
            {
                MachineId = machineId,
                ServiceDate = serviceDate
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

        #region Private Methods

        private HttpClient CreateApiClient()
        {
            HttpClient httpClient = new();
            //httpClient.DefaultRequestHeaders.Accept.Clear();
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
            return httpClient;
        }

        private HttpContent CreateContent<T>(T data)
        {
            byte[] content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
            HttpContent Result = new ByteArrayContent(content);
            Result.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return Result;
        }

        private void CallPostApi<T>(string endPoint, T data)
        {
            try
            {
                using HttpClient httpClient = CreateApiClient();
                string address = $"{_apiSettings.RootAddress}{endPoint}";

                HttpContent content = CreateContent(data);
                using HttpResponseMessage response = httpClient.PostAsync(address, content).Result;

                string jsonData = response.Content.ReadAsStringAsync().Result;
                JsonResponseModel responseModel = (JsonResponseModel)JsonSerializer.Deserialize(jsonData, typeof(JsonResponseModel), GSendShared.Constants.DefaultJsonSerializerOptions);

                if (responseModel.success)
                {
                    return;
                }

                throw new GSendApiException(responseModel.responseData);
            }
            catch (Exception)
            {
                //log?
                throw;
            }
        }

        private T CallGetApi<T>(string endPoint)
        {
            try
            {
                using HttpClient httpClient = CreateApiClient();
                string address = $"{_apiSettings.RootAddress}{endPoint}";

                using HttpResponseMessage response = httpClient.GetAsync(address).Result;

                string jsonData = response.Content.ReadAsStringAsync().Result;
                JsonResponseModel responseModel = (JsonResponseModel)JsonSerializer.Deserialize(jsonData, typeof(JsonResponseModel), GSendShared.Constants.DefaultJsonSerializerOptions);

                if (responseModel.success)
                {
                    return JsonSerializer.Deserialize<T>(responseModel.responseData);
                }

                throw new GSendApiException(responseModel.responseData);
            }
            catch (Exception)
            {
                //log?
                throw;
            }
        }

        private void CallPutApi<T>(string endPoint, T data)
        {
            try
            {
                using HttpClient httpClient = CreateApiClient();
                string address = $"{_apiSettings.RootAddress}{endPoint}";

                HttpContent content = CreateContent(data);
                using HttpResponseMessage response = httpClient.PutAsync(address, content).Result;

                string jsonData = response.Content.ReadAsStringAsync().Result;
                JsonResponseModel responseModel = (JsonResponseModel)JsonSerializer.Deserialize(jsonData, typeof(JsonResponseModel), GSendShared.Constants.DefaultJsonSerializerOptions);

                if (responseModel.success)
                {
                    return;
                }

                throw new GSendApiException(responseModel.responseData);
            }
            catch (Exception)
            {
                //log?
                throw;
            }
        }

        #endregion Private Methods
    }
}