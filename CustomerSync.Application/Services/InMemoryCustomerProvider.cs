


using CustomerSync.Application.Interfaces;
using CustomerSync.Domain.Entities;

namespace CustomerSync.Application.Services;
public sealed class InMemoryCustomerProvider:ICustomerProvider
{
    /// <inheritdoc />
    public Task<IEnumerable<Customer>> GetCustomersAsync()
    {

        IEnumerable<Customer> customers =
               [
                   new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Smith",
                Email = "john@test.com",
                Region = "UK"
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Mary",
                LastName = "Jones",
                Email = "mary@test.com",
                Region = "US"
            },
            new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Brown",
                Email = "bob@test.com",
                Region = "UK"
            }
               ];
            return Task.FromResult(customers);
    }
}