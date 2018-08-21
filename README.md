# Alexa.NET.Settings
Small helper library for Alexa.NET based skills to access the settings API

```
## Getting settings information
```csharp
using Alexa.NET.Response

var client = new SettingsClient(skillRequest);
var timezone = await client.TimeZone();
var distance = await client.DistanceUnit();
var temp = await client.TemperatureUnit();
```