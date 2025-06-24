using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelContextProtocol.Server;
using LunchTimeMCP;

var builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<RestaurantTools>();

builder.Services.AddSingleton<RestaurantService>();

await builder.Build().RunAsync();
