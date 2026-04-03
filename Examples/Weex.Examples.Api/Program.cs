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
    options.ApiCredentials = new ApiCredentials("<APIKEY>", "<APISECRET>", "<PASS>");
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
    return result.Data.SingleOrDefault()?.LastPrice;
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] IWeexRestClient client) =>
{
    var result = await client.SpotApi.Account.GetAccountInfoAsync();
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.Run();