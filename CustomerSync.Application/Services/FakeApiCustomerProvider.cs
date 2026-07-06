using CustomerSync.Application.Interfaces;
using CustomerSync.Domain.Entities;

namespace CustomerSync.Application.Services;



public sealed class FakeApiCustomerProvider : ICustomerProvider
{
    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        await Task.Delay(500);
        return
        [
            new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Sarah",
                LastName = "Wilson",
                Email = "sarah@test.com",
                Region = "EU"
            }
        ];

    }
}