using CustomerSync.Domain.Entities;

namespace CustomerSync.Application.Interfaces;

/// <summary>
/// Defines a contract for retrieving customer data
/// from an external source.
/// </summary>
public interface ICustomerProvider
{
    /// <summary>
    /// Retrieves customer records asynchronously.
    /// </summary>
    /// <returns>
    /// Collection of customers.
    /// </returns>
    Task<IEnumerable<Customer>> GetCustomersAsync();
}