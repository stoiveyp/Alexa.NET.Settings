using System;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Response;
using Xunit;

namespace Alexa.NET.Settings.Tests
{
    public class ClientTests
    {
        private const string DeviceId = "testDevice";

        [Fact]
        public void CreateFromSkillRequestSetupCorrectly()
        {
            const string endpoint = "https://testclient/";
            const string accesstoken = "accesstoken";

            var request = new SkillRequest
            {
                Context = new Context
                {
                    System = new AlexaSystem
                    {
                        ApiEndpoint = endpoint,
                        ApiAccessToken = accesstoken,
                        Device = new Device { DeviceID = DeviceId}
                    }
                }
            };

            var client = new SettingsClient(request);

            Assert.Equal(endpoint, client.Client.BaseAddress.ToString());
            Assert.Equal(accesstoken, client.Client.DefaultRequestHeaders.Authorization.Parameter);
            Assert.Equal(DeviceId,client.DeviceId);
        }

        [Fact]
        public void ClientSetDirectlySetCorrectly()
        {
            var lowClient = new HttpClient();

            var client = new SettingsClient(lowClient,DeviceId);

            Assert.Equal(lowClient, client.Client);
        }

        [Fact]
        public async Task GetTimeZoneGeneratesCorrectRequest()
        {
            const string expectedResult = "Africa/Abidjan";
            string fullNamePath = $"/v2/devices/{DeviceId}/settings/System.timeZone";
            var runHandler = false;
            var httpClient = new HttpClient(new ActionMessageHandler(req =>
            {
                runHandler = true;
                Assert.Equal(req.RequestUri.PathAndQuery, fullNamePath);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(Utility.GetExampleJson("TimeZone.json"))
                };
            }))
            { BaseAddress = new Uri("https://testclient.com", UriKind.Absolute) };

            var client = new SettingsClient(httpClient, DeviceId);
            var nameResult = await client.TimeZone();

            Assert.True(runHandler);
            Assert.Equal(expectedResult, nameResult);
        }
        [Fact]
        public async Task GetDistanceUnitGeneratesCorrectRequest()
        {
            const string expectedResult = "METRIC";
            string fullNamePath = $"/v2/devices/{DeviceId}/settings/System.distanceUnits";
            var runHandler = false;
            var httpClient = new HttpClient(new ActionMessageHandler(req =>
                {
                    runHandler = true;
                    Assert.Equal(req.RequestUri.PathAndQuery, fullNamePath);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(Utility.GetExampleJson("Distance.json"))
                    };
                }))
                { BaseAddress = new Uri("https://testclient.com", UriKind.Absolute) };

            var client = new SettingsClient(httpClient, DeviceId);
            var nameResult = await client.DistanceUnit();

            Assert.True(runHandler);
            Assert.Equal(expectedResult, nameResult);
        }

        [Fact]
        public async Task GetTemperatureUnitGeneratesCorrectRequest()
        {
            const string expectedResult = "CELCIUS";
            string fullNamePath = $"/v2/devices/{DeviceId}/settings/System.temperatureUnits";
            var runHandler = false;
            var httpClient = new HttpClient(new ActionMessageHandler(req =>
                {
                    runHandler = true;
                    Assert.Equal(req.RequestUri.PathAndQuery, fullNamePath);
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                    {
                        Content = new StringContent(Utility.GetExampleJson("Temp.json"))
                    };
                }))
                { BaseAddress = new Uri("https://testclient.com", UriKind.Absolute) };

            var client = new SettingsClient(httpClient, DeviceId);
            var nameResult = await client.TemperatureUnit();

            Assert.True(runHandler);
            Assert.Equal(expectedResult, nameResult);
        }
    }
}