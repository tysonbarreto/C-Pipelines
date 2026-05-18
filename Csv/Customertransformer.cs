

using ETL.Models;

namespace ETL.Csv;

public class CustomerTransformer : ITransformer<Customer, Customer>
{
    public IEnumerable<Customer> Transform(IEnumerable<Customer> input)
    {
        Logger.Info("Transforming customers...");
        Func<Customer,bool> isValidAge = (c) => c.Age>= 18;

        var query = input
            .Where(isValidAge)
            .Select(c =>
            {
                c.Name = c.Name.Trim();
                c.Email = c.Email.Trim().ToLowerInvariant();
                return c;
            })
            .OrderBy(c=>c);

        return query.ToList();

    }
}