using Weex.Net;
using Weex.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Weex services
builder.Services.AddWeex();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddWeex(options =>
{
    options.ApiCredentials = new WeexCredentials("API_KEY", "API_SECRET", "API_PASSPHRASE");
    options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoint and inject the rest client
app.MapGet("/{Symbol}", async ([FromServices] IWeexRestClient client, string symbol) =>
{
    var result = await client.SpotApi.ExchangeData.GetTickersAsync([symbol]);
    return result.Success
        ? Results.Ok(result.Data.SingleOrDefault()?.LastPrice)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] IWeexRestClient client) =>
{
    var result = await client.SpotApi.Account.GetAccountInfoAsync();
    return result.Success
        ? Results.Ok(result.Data)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();

app.Run();
