## Bonsai.Backend

ASP.NET Minimal API with rate limitting and [Serilog](https://github.com/serilog/serilog) logging made for the [Bonsai mobile app](https://github.com/Me-Wosh/Bonsai) to handle responses from an external weather API.

### The reasons for this API / why not just send HTTP requests directly through the mobile app:

* security - this way I can hide API key on the server as an environmental variable, otherwise everyone that downloaded the app could potentially get the key,
* stability and convenience - if the external API breaks I can just make a simple update to this codebase instead of making users update the entire app to the latest version just to get the optional feature working,
* cleaner code and future-proofing - there could be possible future features like this one that the mobile app doesn't really care about the exact implementation.
