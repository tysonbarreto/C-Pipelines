





using System.Text.Json;
using CustomerSync.Application.Configuration;
using CustomerSync.Application.Interfaces;
using CustomerSync.Application.Services;
using CustomerSync.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(
                    "log/customersync.log",
                    rollingInterval: RollingInterval.Day
                ).CreateLogger();


// var customers = CustomerService.GetCustomers();

// foreach(var customer in customers)
// {
//     System.Console.WriteLine($"Customer : {customer}");
// }

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddLogging();
builder.Services.AddSingleton<ICustomerProvider, InMemoryCustomerProvider>();
builder.Services.AddSingleton<CustomerService>();
builder.Services.AddSingleton<CustomerGroupingService>();
builder.Services.AddSingleton<ICustomerProvider, FakeApiCustomerProvider>();
builder.Services.AddSingleton<CustomerReportWriter>();
builder.Logging.ClearProviders();
builder.Services.AddSerilog();

builder.Services.Configure<ReportSettings>(builder.Configuration.GetSection("ReportSettings"));


var host = builder.Build();

var customerService = host.Services.GetRequiredService<CustomerService>();
var groupingService = host.Services.GetRequiredService<CustomerGroupingService>();
var customers = await customerService.GetCustomersAsync();

var summaries = groupingService.GroupByRegion(customers);

foreach (var customer in customers.Select((user, index) => new { user, index }))
{
    System.Console.WriteLine($"Customer_{customer.index} : {JsonSerializer.Serialize(customer.user, new JsonSerializerOptions { WriteIndented = true })}");
}


using var reportWriter = host.Services.GetRequiredService<CustomerReportWriter>();

var staticMethod = reportWriter.GetType().GetMethod("CreateDirectoryIfRequired", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
staticMethod!.Invoke(null, parameters: ["reports/customer-summary.csv"]);

await reportWriter.WriteCsvAsync(summaries, "reports/customer-summary.csv");
await reportWriter.WritJsonAsync(summaries, "reports/customer-summary.json");
