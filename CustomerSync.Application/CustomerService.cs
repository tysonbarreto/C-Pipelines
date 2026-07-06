using CustomerSync.Domain.Entities;

namespace CustomerSync.Application
{
    public class CustomerService
    {
        public static IEnumerable<Customer> GetCustomers()
        {
            return
            [
                new() {

                Email = "john@test.com",
                Region = "UK"

            },
            new() {
                Email = "mary@test.com",
                Region = "US"
            }

            ];
        }
    }
}