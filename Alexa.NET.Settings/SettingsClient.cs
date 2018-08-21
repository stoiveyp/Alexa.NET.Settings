using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Response
{
    public class SettingsClient
    {
        public HttpClient Client { get; set; }
        public string DeviceId { get; set; }

        public SettingsClient(SkillRequest request) : this(
            request.Context.System.ApiEndpoint,
            request.Context.System.ApiAccessToken,
            request.Context.System.Device.DeviceID)
        { }

        public SettingsClient(string endpointUrl, string accessToken, string deviceId)
        {
            var client = new HttpClient { BaseAddress = new Uri(endpointUrl, UriKind.Absolute) };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Client = client;
            DeviceId = deviceId;
        }

        public SettingsClient(HttpClient client, string deviceId)
        {
            Client = client;
            DeviceId = deviceId;
        }

        public async Task<string> TimeZone()
        {
            var nameResponse = await Client.GetStringAsync($"/v2/devices/{DeviceId}/settings/System.timeZone");
            var response = JToken.Parse(nameResponse);
            return response.Value<string>();
        }

        public async Task<string> DistanceUnit()
        {
            var givenNameResponse = await Client.GetStringAsync($"/v2/devices/{DeviceId}/settings/System.distanceUnits");
            var response = JToken.Parse(givenNameResponse);
            return response.Value<string>();
        }

        public async Task<string> TemperatureUnit()
        {
            var emailResponse = await Client.GetStringAsync($"/v2/devices/{DeviceId}/settings/System.temperatureUnits");
            var response = JToken.Parse(emailResponse);
            return response.Value<string>();
        }
    }
}