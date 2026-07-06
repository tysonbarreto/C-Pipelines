using CustomerSync.Application.Interfaces;
using CustomerSync.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CustomerSync.Application.Services;

/// <summary>
/// Provides customer-related business operations.
/// </summary>
/// <remarks>
/// Initializes a new instance of the
/// <see cref="CustomerService"/> class.
/// </remarks>
public sealed class CustomerService
{
    private readonly ICustomerProvider _customerProvider;
    private readonly ILogger<CustomerService> _logger;

    /// <param name="customerProvider">
    /// Source used to retrieve customer data.
    /// </param>
    public CustomerService(ICustomerProvider customerProvider, ILogger<CustomerService> logger)
    {
        _customerProvider = customerProvider;
        _logger = logger;
    }

    /// <summary>
    /// Retrieves all customers.
    /// </summary>
    /// <returns>
    /// Collection of customer records.
    /// </returns>
    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        _logger.LogInformation("Loading customers");
        var customers = await _customerProvider.GetCustomersAsync();
        _logger.LogInformation("Retrieved {Count} customer(s)", customers.Count());
        return customers;

    }
}