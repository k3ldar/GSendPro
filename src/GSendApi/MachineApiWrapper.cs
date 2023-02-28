using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using GSendShared;

namespace GSendApi
{
    public sealed class MachineApiWrapper
    {
        private class JsonResponseModel
        {
            public bool success { get; set; }
            public string responseData { get; set; }
        }

        private readonly ApiSettings _apiSettings;

        public MachineApiWrapper(ApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
        }

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

        //public void MachineUpdate(IMachine machine)
        //{

        //}

        private HttpClient CreateApiClient()
        {
            HttpClient httpClient = new();
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("GSend", _apiSettings.ApiVersion));
            httpClient.Timeout = TimeSpan.FromMilliseconds(_apiSettings.Timeout);

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
    }
}