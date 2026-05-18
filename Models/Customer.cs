

namespace ETL.Models;

public class Customer : IComparable<Customer>
{
    public int Id {get; set;} = default;
    public string Name {get; set;} = "";

    public string Email {get; set;} = "";

    public int Age {get; set;} = default;

    public int CompareTo(Customer? other)
    {
        if (other is null) return 1;
        return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        //StringComparison.OrdinalIgnoreCase ignore the case difference
    }
    public override string ToString()
    {
        return $"{Id} | {Name} | {Email} | Age: {Age}";
    }
}