
using CustomerSync.Application.Models;
using CustomerSync.Domain.Entities;

namespace CustomerSync.Application.Services;

public sealed class CustomerGroupingService
{
    public IEnumerable<CustomerRegionSummary> GroupByRegion(IEnumerable<Customer> customers)
    {
        return customers
                .GroupBy(c => c.Region)
                .Select(g => new CustomerRegionSummary
                {
                    Region = g.Key,
                    CustomerCount = g.Count()
                })
                .OrderBy(x => x.Region);
    }
}