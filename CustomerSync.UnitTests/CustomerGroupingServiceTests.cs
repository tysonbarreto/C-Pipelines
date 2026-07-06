


using CustomerSync.Application.Services;
using CustomerSync.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace CustomerSync.UnitTests;

public class CustomerGroupingServiceTests
{
    [Fact]
    public void GroupByRegion_Should_Return_Correct_Counts()
    {

        var customers = new List<Customer> { new () { Region = "UK" }, new () { Region = "UK" }, new () { Region = "US" } };

        var result = new CustomerGroupingService().GroupByRegion(customers);

        result.Should().HaveCount(2);

        result.Single(x=>x.Region == "UK").CustomerCount.Should().Be(2);
        result.Single(x=>x.Region == "US").CustomerCount.Should().Be(1);
        Assert.True(true);

    }
}